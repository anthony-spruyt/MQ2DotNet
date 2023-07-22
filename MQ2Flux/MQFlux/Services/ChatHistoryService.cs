using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MQFlux.Services
{
    public interface IChatHistory
    {
        IEnumerable<ChatLogItem> Chat { get; }
        IEnumerable<ChatLogItem> EQChat { get; }
        IEnumerable<ChatLogItem> MQChat { get; }
        bool NoSpam(TimeSpan last, string text);
    }

    public class ChatLogItem
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
    }

    public static class ChatHistoryServiceExtensions
    {
        public static IServiceCollection AddChatHistory(this IServiceCollection services)
        {
            return services
                .AddSingleton<IChatHistory, ChatHistoryService>();
        }
    }

    public class ChatHistoryService : IChatHistory, IDisposable
    {
        public static readonly int MAX_HISTORY_SIZE = 5000;

        private readonly ConcurrentQueue<ChatLogItem> chat;
        private readonly ConcurrentQueue<ChatLogItem> eqChat;
        private readonly ConcurrentQueue<ChatLogItem> mqChat;
        private readonly IContext context;

        private bool disposedValue;

        public IEnumerable<ChatLogItem> Chat => chat.OrderByDescending(i => i.Timestamp);

        public IEnumerable<ChatLogItem> EQChat => eqChat.OrderByDescending(i => i.Timestamp);

        public IEnumerable<ChatLogItem> MQChat => mqChat.OrderByDescending(i => i.Timestamp);

        public ChatHistoryService(IContext context)
        {
            chat = new ConcurrentQueue<ChatLogItem>();
            eqChat = new ConcurrentQueue<ChatLogItem>();
            mqChat = new ConcurrentQueue<ChatLogItem>();

            this.context = context;

            var events = context.Events;

            events.OnChat += Events_OnChat;
            events.OnChatEQ += Events_OnChatEQ;
            events.OnChatMQ2 += Events_OnChatMQ2;
        }

        public bool NoSpam(TimeSpan timespan, string text)
        {
            if (timespan == TimeSpan.Zero)
            {
                return true;
            }

            var timestamp = DateTimeOffset.Now.Subtract(timespan);
            var enumerable = Chat.TakeWhile(i => i.Timestamp >= timestamp);
            var matchFound = enumerable.Any(i => i.Message.Contains(text));
            return !matchFound;
        }

        private void AddToQueue(object sender, string e, ConcurrentQueue<ChatLogItem> queue)
        {
            queue.Enqueue(new ChatLogItem() { Message = e, Timestamp = DateTimeOffset.Now });

            while (queue.Count > MAX_HISTORY_SIZE)
            {
                queue.TryDequeue(out var _);
            }
        }

        private void Events_OnChat(object sender, string e)
        {
            AddToQueue(sender, e, chat);
        }

        private void Events_OnChatEQ(object sender, string e)
        {
            AddToQueue(sender, e, eqChat);
        }

        private void Events_OnChatMQ2(object sender, string e)
        {
            AddToQueue(sender, e, mqChat);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (context != null && context.Events != null)
                    {
                        var events = context.Events;

                        events.OnChat -= Events_OnChat;
                        events.OnChatEQ -= Events_OnChatEQ;
                        events.OnChatMQ2 -= Events_OnChatMQ2;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MQ2ChatHistory()
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
