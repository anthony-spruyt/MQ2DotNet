using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2DotNet.Utility;
using MQ2Flux.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public interface IItemService
    {
        Task AutoInventoryAsync(Predicate<ItemType> predicate = null, CancellationToken cancellationToken = default);
        Task<bool> UseItemAsync(ItemType item, string verb = "Using", CancellationToken cancellationToken = default);
    }

    public static class ItemServiceExtensions
    {
        public static IServiceCollection AddItemService(this IServiceCollection services)
        {
            return services
                .AddSingleton<IItemService, ItemService>();
        }
    }

    public class ItemService : IItemService, IDisposable
    {
        private readonly IMQ2Logger mq2Logger;
        private readonly IMQ2Context context;
        private readonly ConcurrentDictionary<string, DateTime> cachedCommands;

        private SemaphoreSlim semaphore;
        private bool disposedValue;

        public ItemService(IMQ2Logger mq2Logger, IMQ2Context context)
        {
            this.mq2Logger = mq2Logger;
            this.context = context;

            cachedCommands = new ConcurrentDictionary<string, DateTime>();
            semaphore = new SemaphoreSlim(1);
        }

        public async Task AutoInventoryAsync(Predicate<ItemType> predicate = null, CancellationToken cancellationToken = default)
        {
            DateTime waitUntil = DateTime.UtcNow + TimeSpan.FromSeconds(2);

            while (waitUntil >= DateTime.UtcNow && context.TLO.Cursor == null && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }

            if (cancellationToken.IsCancellationRequested || (predicate != null && !predicate(context.TLO.Cursor)))
            {
                return;
            }

            mq2Logger.Log($"Putting [\ag{context.TLO.Cursor.Name}\aw] into your inventory", TimeSpan.Zero);

            while (context.TLO.Cursor != null && !cancellationToken.IsCancellationRequested)
            {
                context.MQ2.DoCommand("/autoinv");

                await Task.Delay(500, cancellationToken);
            }
        }

        public async Task<bool> UseItemAsync(ItemType item, string verb = "Using", CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                if (!IsValid(item))
                {
                    return false;
                }

                PurgeCache();

                string command = $"/useitem {item.Name}";

                if (!cachedCommands.TryAdd(command, DateTime.UtcNow))
                {
                    return false;
                }

                if (!item.HasCastTime())
                {
                    context.MQ2.DoCommand(command);

                    mq2Logger.Log($"{verb} [\ag{item.Name}\aw]", TimeSpan.Zero);

                    return true;
                }

                var castTime = item.Clicky.CastTime.Value;
                var timeout = castTime + TimeSpan.FromMilliseconds(500); // add some fat
                var castOnYou = item.Clicky.Spell.CastOnYou;
                var castOnAnother = item.Clicky.Spell.CastOnAnother;
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

                context.MQ2.DoCommand(command);

                mq2Logger.Log($"{verb} [\ag{item.Name}\aw] \austarted", TimeSpan.Zero);

                await waitForEQTask.TimeoutAfter(timeout);

                if (fizzled || interrupted)
                {
                    //var reason = fizzled ? "fizzled" : "was interrupted";
                    //mq2Logger.Log($"{verb} [\ay{item.Name}\aw] \ar{reason}", TimeSpan.Zero);

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

            //mq2Logger.Log($"{verb} [\ay{item.Name}\aw] \agsucceeded", TimeSpan.Zero);

            return true;
        }

        private bool IsValid(ItemType item)
        {
            if (!item.CanUse || !item.IsTimerReady())
            {
                return false;
            }

            var me = context.TLO.Me;

            if (me.AmICasting() || (item.HasCastTime() && me.Moving))
            {
                return false;
            }

            return true;
        }

        private void PurgeCache()
        {
            var rtt = context.TLO.EverQuest.Ping > 0 ? TimeSpan.FromMilliseconds(context.TLO.EverQuest.Ping.Value * 3) : TimeSpan.FromMilliseconds(1500);
            var purgeOlderThan = DateTime.UtcNow.Subtract(rtt);
            var keys = cachedCommands.Where(i => i.Value < DateTime.UtcNow).Select(i => i.Key).ToArray();

            foreach (var key in keys)
            {
                cachedCommands.TryRemove(key, out var _);
            }
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
        // ~ItemService()
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
