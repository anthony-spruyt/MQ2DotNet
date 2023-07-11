using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2DotNet.Utility;
using MQ2Flux.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public interface ISpellCastingService
    {
        Task<bool> CastAsync(SpellType spell, CancellationToken cancellationToken = default);
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
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                var me = context.TLO.Me;

                if (!me.Spawn.Class.CanCast || me.AmICasting() || (me.Moving && spell.HasCastTime()))
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
                    // Not memorized - add support for this later.
                    return false;
                }

                if (!me.IsSpellReady(spellBookSpell.Name))
                {
                    return false;
                }

                var castTime = spell.CastTime ?? TimeSpan.Zero;
                var timeout = castTime + TimeSpan.FromMilliseconds(250); // add some fat
                var castOnYou = spell.CastOnYou;
                var castOnAnother = spell.CastOnAnother;
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
                        cancellationToken
                    )
                );

                await Task.Yield();

                context.MQ2.DoCommand($"/cast {gem}");
                mq2Logger.Log($"Casting [\ay{spell.Name}\aw]");

                await waitForEQTask.TimeoutAfter(timeout);

                if (fizzled || interrupted)
                {
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
