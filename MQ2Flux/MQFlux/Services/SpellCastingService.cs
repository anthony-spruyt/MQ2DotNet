﻿using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface ISpellCastingService
    {
        /// <summary>
        /// Cast a spell. If it is not currently memorized it will be memorized in the last gem slot.
        /// </summary>
        /// <param name="spell"></param>
        /// <param name="waitForSpellReady">If the spell is already memorized wait for it to be ready.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CastAsync(SpellType spell, bool waitForSpellReady = false, CancellationToken cancellationToken = default);
        Task<bool> MemorizeSpellAsync(uint slot, SpellType spell, CancellationToken cancellationToken = default);
        Task<bool> WaitForSpellReadyAsync(SpellType spell, CancellationToken cancellationToken = default);
        Task<bool> WaitForSpellReadyAsync(int gem, CancellationToken cancellationToken = default);
    }

    public static class SpellCastingServiceExtensions
    {
        public static IServiceCollection AddSpellCastingService(this IServiceCollection services)
        {
            return services
                .AddSingleton<ISpellCastingService, SpellCastingService>();
        }
    }

    public class SpellCastingService : ISpellCastingService, IDisposable
    {
        private readonly IContext context;
        private readonly IMQLogger mqLogger;

        private SemaphoreSlim semaphore;
        private bool disposedValue;

        public SpellCastingService(IContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;

            semaphore = new SemaphoreSlim(1);
        }

        public async Task<bool> CastAsync(SpellType spell, bool waitForSpellReady = false, CancellationToken cancellationToken = default)
        {
            // TODO add support for splash spell targets.
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                var me = context.TLO.Me;

                if
                (
                    !me.Spawn.Class.CanCast || 
                    me.AmICasting() || 
                    (
                        me.Moving && 
                        spell.HasCastTime()
                    )
                )
                {
                    return false;
                }

                var spellBookSpell = me.GetSpellbookSpellBySpellID((int)spell.ID.Value);

                if (spellBookSpell == null)
                {
                    // You dont know this spell.
                    return false;
                }

                var gem = (int)me.GetGem(spellBookSpell.Name).GetValueOrDefault(0u);

                if
                (
                    gem == 0 &&
                    (
                        !await MemorizeSpellInternalAsync(me.NumGems.GetValueOrDefault(8), spellBookSpell) ||
                        !await WaitForSpellReadyAsync(gem, cancellationToken)
                    )
                )
                {
                    return false;
                }

                if
                (
                    waitForSpellReady && !await WaitForSpellReadyAsync(gem, cancellationToken) ||
                    !me.IsSpellReady(gem) ||
                    spellBookSpell.Mana > me.CurrentMana ||
                    spellBookSpell.EnduranceCost > me.CurrentEndurance
                )
                {
                    return false;
                }

                // TODO check for reagents.

                var castTime = spellBookSpell.CastTime ?? TimeSpan.Zero;
                var timeout = castTime + TimeSpan.FromMilliseconds(1000); // add some fat
                var castOnYou = spellBookSpell.CastOnYou;
                var castOnAnother = spellBookSpell.CastOnAnother;
                var wasCastOnYou = false;
                var wasCastOnAnother = false;
                var fizzled = false;
                var interrupted = false;

                Task<bool> waitForEQTask = Task.Run
                (
                    () => context.Chat.WaitForEQ
                    (
                        text =>
                        {
                            wasCastOnYou = string.Compare(text, castOnYou) == 0;
                            wasCastOnAnother = text.Contains(castOnAnother);
                            fizzled = text.Contains("Your") && text.Contains("spell fizzles");
                            interrupted = text.Contains("Your") && text.Contains("spell is interrupted");

                            return wasCastOnYou || wasCastOnAnother || fizzled || interrupted;
                        },
                        timeout,
                        cancellationToken
                    )
                );

                context.MQ.DoCommand($"/cast {gem}");

                mqLogger.Log($"Casting [\ay{spellBookSpell.Name}\aw]", TimeSpan.Zero);

                // Some spells dont write a message so assume it was successful if it timed out.
                _ = await waitForEQTask;

                if (fizzled || interrupted)
                {
                    var reason = fizzled ? "fizzled" : "was interrupted";
                    mqLogger.Log($"Casting [\ay{spell.Name}\aw] \ar{reason}", TimeSpan.Zero);
                    
                    return false;
                }
            }
            finally
            {
                semaphore.Release();
            }

            return true;
        }

        public async Task<bool> MemorizeSpellAsync(uint slot, SpellType spell, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                return await MemorizeSpellInternalAsync(slot, spell, cancellationToken);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<bool> WaitForSpellReadyAsync(SpellType spell, CancellationToken cancellationToken = default)
        {
            var me = context.TLO.Me;
            string name = spell.Name;

            return await WaitForSpellReadyInternalAsync(() => me.IsSpellReady(name), cancellationToken);
        }

        public async Task<bool> WaitForSpellReadyAsync(int gem, CancellationToken cancellationToken = default)
        {
            var me = context.TLO.Me;

            return await WaitForSpellReadyInternalAsync(() => me.IsSpellReady(gem), cancellationToken);
        }

        private async Task<bool> WaitForSpellReadyInternalAsync(Func<bool> isSpellReady, CancellationToken cancellationToken)
        {
            if (isSpellReady())
            {
                return true;
            }

            DateTime waitForSpellReadyUntil = DateTime.UtcNow + TimeSpan.FromSeconds(8);

            while (!isSpellReady())
            {
                await MQFlux.Yield(cancellationToken);

                if (waitForSpellReadyUntil < DateTime.UtcNow || cancellationToken.IsCancellationRequested)
                {
                    return false;
                }
            }

            // spell ready evaluates to true but there is some delay observed - guessing this is due to some server client latency
            await Task.Delay(500);

            return true;
        }

        private async Task<bool> MemorizeSpellInternalAsync(uint slot, SpellType spell, CancellationToken cancellationToken = default)
        {
            var me = context.TLO.Me;
            var spellName = spell.Name;

            if (me.GetGem(spellName) == slot)
            {
                return true;
            }

            DateTime waitUntil = DateTime.UtcNow +TimeSpan.FromSeconds(8);

            context.MQ.DoCommand($"/memspell {slot} \"{spellName}\"");
            mqLogger.Log($"Memorizing [\ay{spellName}\aw] in slot \ay{slot}", TimeSpan.Zero);

            try
            {
                while (me.GetGem(spellName) != slot)
                {
                    await MQFlux.Yield(cancellationToken);

                    if (waitUntil < DateTime.UtcNow || cancellationToken.IsCancellationRequested)
                    {
                        return false;
                    }
                }
            }
            finally
            {
                // Sometimes memming the spell fails for some reason/gets stuck so auto close the spellbook after.
                context.TLO.CloseSpellBook();
            }

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (semaphore != null)
                    {
                        semaphore.Dispose();
                        semaphore = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SpellCastingService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
