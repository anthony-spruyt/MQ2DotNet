using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2Flux.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public interface ISpellCastingService
    {
        Task<bool> CastAsync(SpellType spell, CancellationToken cancellationToken = default);
        Task<bool> MemorizeSpellAsync(uint slot, SpellType spell, CancellationToken cancellationToken = default);
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
        private readonly IMQ2Context context;
        private readonly IMQ2Logger mq2Logger;

        private SemaphoreSlim semaphore;
        private bool disposedValue;

        public SpellCastingService(IMQ2Context context, IMQ2Logger mq2Logger)
        {
            this.context = context;
            this.mq2Logger = mq2Logger;

            semaphore = new SemaphoreSlim(1);
        }

        public async Task<bool> CastAsync(SpellType spell, CancellationToken cancellationToken = default)
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

                var gem = me.GetGem(spellBookSpell.Name) ?? 0;

                if (gem == 0)
                {
                    if (!await MemorizeSpellInternalAsync(me.NumGems.Value, spellBookSpell))
                    {
                        return false;
                    }

                    DateTime waitForSpellReadyUntil = DateTime.UtcNow + TimeSpan.FromSeconds(5);

                    try
                    {
                        while
                        (
                            waitForSpellReadyUntil >= DateTime.UtcNow &&
                            !me.IsSpellReady(spellBookSpell.Name) &&
                            !cancellationToken.IsCancellationRequested
                        )
                        {
                            await MQ2Flux.Yield(cancellationToken);
                        }
                    }
                    catch (TimeoutException)
                    {
                        return false;
                    }
                }
                
                if (!me.IsSpellReady(spellBookSpell.Name))
                {
                    return false;
                }

                if (spellBookSpell.Mana > me.CurrentMana || spellBookSpell.EnduranceCost > me.CurrentEndurance)
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

                Task waitForEQTask = Task.Run
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

                context.MQ2.DoCommand($"/cast {spellBookSpell.Name}");

                mq2Logger.Log($"Casting [\ay{spellBookSpell.Name}\aw]", TimeSpan.Zero);

                await waitForEQTask;

                if (fizzled || interrupted)
                {
                    var reason = fizzled ? "fizzled" : "was interrupted";
                    mq2Logger.Log($"Casting [\ay{spell.Name}\aw] \ar{reason}", TimeSpan.Zero);
                    
                    return false;
                }
            }
            catch (TimeoutException)
            {
                // Some spells dont write a message so assume it was successful if it timed out.
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

        private async Task<bool> MemorizeSpellInternalAsync(uint slot, SpellType spell, CancellationToken cancellationToken = default)
        {
            var me = context.TLO.Me;
            var spellName = spell.Name;


            if (me.GetGem(spellName) == slot)
            {
                return true;
            }

            DateTime waitUntil = DateTime.UtcNow + GetMemorizeTimeout((uint?)spell.Level ?? 0u, me.Spawn.Level.Value);

            context.MQ2.DoCommand($"/memspell {slot} \"{spellName}\"");
            mq2Logger.Log($"Memorizing [\ay{spellName}\aw]", TimeSpan.Zero);

            try
            {
                while
                (
                    waitUntil >= DateTime.UtcNow &&
                    me.GetGem(spellName) != slot &&
                    !cancellationToken.IsCancellationRequested
                )
                {
                    await MQ2Flux.Yield(cancellationToken);
                }
            }
            catch (TimeoutException)
            {
                return false;
            }
            finally
            {
                // Sometimes memming the spell fails for some reason/gets stuck so auto close the spellbook after.
                context.TLO.CloseSpellBook();
            }

            return true;
        }

        /// <summary>
        /// TODO: tweak this, all I could find about this was that if the level difference is 0 then the memorization time is 8 seconds
        /// and that it gets better as the difference increases.
        /// https://forums.daybreakgames.com/eq/index.php?threads/its-time-to-consolidate-spell-memorization-with-spell-refresh.284000/
        /// </summary>
        /// <param name="spellLevel"></param>
        /// <param name="casterLevel"></param>
        /// <returns></returns>
        private TimeSpan GetMemorizeTimeout(uint spellLevel, uint casterLevel)
        {
            if (casterLevel < spellLevel)
            {
                return TimeSpan.FromSeconds(8);
            }

            var difference = casterLevel - spellLevel;
            var seconds = 8d - difference / 2d;

            if (seconds < 1d)
            {
                seconds = 1d;
            }

            return TimeSpan.FromSeconds(seconds);
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
