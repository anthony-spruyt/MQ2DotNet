using MQFlux.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Extensions
{
    public static class ContextExtensions
    {
        public static async Task<bool> DoCommandAndWaitFor(this IContext context, string command, Predicate<string> predicate, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitFor
                (
                    predicate,
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitFor(this IContext context, string command, string containsText, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitFor
                (
                    text => text.Contains(containsText),
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitFor(this IContext context, string command, string matchesText, bool ignoreCase, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitFor
                (
                    text => string.Compare(text, matchesText, ignoreCase) == 0,
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitForEQ(this IContext context, string command, Predicate<string> predicate, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitForEQ
                (
                    predicate,
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitForEQ(this IContext context, string command, string containsText, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitForEQ
                (
                    text => text.Contains(containsText),
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitForEQ(this IContext context, string command, string matchesText, bool ignoreCase, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitForEQ
                (
                    text => string.Compare(text, matchesText, ignoreCase) == 0,
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitForMQ2(this IContext context, string command, Predicate<string> predicate, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitForMQ2
                (
                    predicate,
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitForMQ2(this IContext context, string command, string containsText, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitForMQ2
                (
                    text => text.Contains(containsText),
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }

        public static async Task<bool> DoCommandAndWaitForMQ2(this IContext context, string command, string matchesText, bool ignoreCase, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task<bool> waitForEQTask = Task.Run
            (
                () => context.Chat.WaitForMQ2
                (
                    text => string.Compare(text, matchesText, ignoreCase) == 0,
                    timeout,
                    cancellationToken
                )
            );

            context.MQ.DoCommand(command);

            return await waitForEQTask;
        }
    }
}
