using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2DotNet.Utility;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public interface IItemService
    {
        Task<bool> UseItemAsync(ItemType item, string verb = "Using", CancellationToken cancellationToken = default);
        bool UseItem(ItemType item, string verb = "Using", CancellationToken cancellationToken = default);
    }

    public static class ItemServiceExtensions
    {
        public static IServiceCollection AddItemService(this IServiceCollection services)
        {
            return services
                .AddSingleton<IItemService, ItemService>();
        }
    }

    public class ItemService : IItemService
    {
        private readonly IMQ2Logger mq2Logger;
        private readonly IMQ2Context context;
        private readonly ConcurrentDictionary<string, DateTime> cachedCommands;

        public ItemService(IMQ2Logger mq2Logger, IMQ2Context context)
        {
            this.mq2Logger = mq2Logger;
            this.context = context;
            cachedCommands = new ConcurrentDictionary<string, DateTime>();
        }

        public bool UseItem(ItemType item, string verb = "Using", CancellationToken cancellationToken = default)
        {
            PurgeCache();

            string command = $"/useitem {item.Name}";

            if (cachedCommands.TryAdd(command, DateTime.UtcNow))
            {
                mq2Logger.Log($"{verb} [\ag{item.Name}\aw]");
                context.MQ2.DoCommand(command);

                return true;
            }

            return false;
        }

        public async Task<bool> UseItemAsync(ItemType item, string verb = "Using", CancellationToken cancellationToken = default)
        {
            PurgeCache();

            string command = $"/useitem {item.Name}";

            if (!cachedCommands.TryAdd(command, DateTime.UtcNow))
            {
                return false;
            }

            mq2Logger.Log($"{verb} [\ag{item.Name}\aw]");

            var castTime = item.Clicky.CastTime;

            if (castTime == null || castTime.Value == TimeSpan.Zero)
            {
                context.MQ2.DoCommand(command);

                return true;
            }

            var rtt = context.TLO.EverQuest.Ping > 0 ? TimeSpan.FromMilliseconds(context.TLO.EverQuest.Ping.Value * 3) : TimeSpan.FromMilliseconds(1500);
            var timeout = castTime.Value.Add(rtt);
            var castOnYou = item.Clicky.Spell.CastOnYou;

            try
            {
                Task coldTask = context.Chat.WaitForEQ(text => string.Compare(text, castOnYou) == 0, cancellationToken);
                Task warmTask = Task.Run(() => coldTask);
                
                context.MQ2.DoCommand(command);

                await warmTask.TimeoutAfter(timeout);
            }
            catch (TimeoutException ex)
            {
                mq2Logger.LogError(ex);

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
    }
}
