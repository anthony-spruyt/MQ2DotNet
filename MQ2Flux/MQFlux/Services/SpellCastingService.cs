using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Extensions;
using MQFlux.Utils;
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
        Task<bool> Cast(SpellType spell, bool waitForSpellReady = false, CancellationToken cancellationToken = default);
        Task<bool> MemorizeSpell(int slot, SpellType spell, CancellationToken cancellationToken = default);
        Task<bool> WaitForSpellReady(SpellType spell, CancellationToken cancellationToken = default);
        Task<bool> WaitForSpellReady(int gem, CancellationToken cancellationToken = default);
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

        public async Task<bool> Cast(SpellType spell, bool waitForSpellReady = false, CancellationToken cancellationToken = default)
        {
            // TODO add support for splash spell targets.
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                var me = context.TLO.Me;

                if
                (
                    !me.Class.CanCast ||
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

                if (gem == 0)
                {
                    gem = (int)me.NumGems.GetValueOrDefault(8u);

                    if (!await MemorizeSpellInternal(gem, spellBookSpell))
                    {
                        return false;
                    }

                    if (!await WaitForSpellReady(gem, cancellationToken))
                    {
                        return false;
                    }
                }

                if (waitForSpellReady && !await WaitForSpellReady(gem, cancellationToken))
                {
                    return false;
                }

                if
                (
                    !me.IsSpellReady(gem) ||
                    spellBookSpell.Mana > me.CurrentMana ||
                    spellBookSpell.EnduranceCost > me.CurrentEndurance
                )
                {
                    return false;
                }

                // TODO check for reagents.

                var castTime = spellBookSpell.CastTime.GetValueOrDefault(TimeSpan.Zero);
                var timeout = castTime + TimeSpan.FromMilliseconds(1000); // add some fat
                var castOnYou = spellBookSpell.CastOnYou;
                var castOnAnother = spellBookSpell.CastOnAnother;
                var wasCastOnYou = false;
                var wasCastOnAnother = false;
                var fizzled = false;
                var interrupted = false;

                mqLogger.Log($"Casting [\ay{spellBookSpell.Name}\aw]", TimeSpan.Zero);

                // Some spells dont write a message so assume it was successful if it timed out.
                _ = context.DoCommandAndWaitForEQ
                (
                    $"/cast {gem}",
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
                );

                if (fizzled || interrupted)
                {
                    var reason = fizzled ? "fizzled" : "was interrupted";

                    mqLogger.Log($"Casting [\ay{spellBookSpell.Name}\aw] \ar{reason}", TimeSpan.Zero);

                    return false;
                }
            }
            finally
            {
                semaphore.Release();
            }

            return true;
        }

        public async Task<bool> MemorizeSpell(int slot, SpellType spell, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                return await MemorizeSpellInternal(slot, spell, cancellationToken);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<bool> WaitForSpellReady(SpellType spell, CancellationToken cancellationToken = default)
        {
            var me = context.TLO.Me;
            string name = spell.Name;

            return await WaitForSpellReadyInternal(() => me.IsSpellReady(name), cancellationToken);
        }

        public async Task<bool> WaitForSpellReady(int gem, CancellationToken cancellationToken = default)
        {
            var me = context.TLO.Me;

            return await WaitForSpellReadyInternal(() => me.IsSpellReady(gem), cancellationToken);
        }

        private async Task<bool> WaitForSpellReadyInternal(Func<bool> isSpellReady, CancellationToken cancellationToken)
        {
            // Check if it is not already ready because waiting is expensive.
            if (isSpellReady())
            {
                return true;
            }

            if (!await Wait.While(() => !isSpellReady(), TimeSpan.FromSeconds(30), cancellationToken))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> MemorizeSpellInternal(int slot, SpellType spell, CancellationToken cancellationToken = default)
        {
            var me = context.TLO.Me;
            var spellName = spell.Name;

            if (me.GetGem(spellName).GetValueOrDefault(0u) == slot)
            {
                return true;
            }

            context.MQ.DoCommand($"/memspell {slot} \"{spellName}\"");
            mqLogger.Log($"Memorizing [\ay{spellName}\aw] in slot \ay{slot}", TimeSpan.Zero);

            try
            {
                if (!await Wait.While(() => me.GetGem(spellName).GetValueOrDefault(0) != slot, TimeSpan.FromSeconds(30), cancellationToken))
                {
                    return false;
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
