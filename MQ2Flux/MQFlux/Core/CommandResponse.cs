using System;
using System.Threading.Tasks;

namespace MQFlux.Core
{
    public class CommandResponse<TResult> : ResponseWithResult<TResult>
    {
        public object ShortCircuitedBehavior { get; }

        public CommandResponse(Exception ex) : base(ex)
        {
        }

        public CommandResponse(TResult result) : base(result)
        {
        }

        public CommandResponse(object pipelineBehavior, TResult result) : base(result)
        {
            ShortCircuitedBehavior = pipelineBehavior;
        }
    }

    public static class CommandResponse
    {
        public static CommandResponse<T> FromResult<T>(T result)
        {
            return new CommandResponse<T>(result);
        }

        public static CommandResponse<T> FromResult<T>(Exception ex)
        {
            return new CommandResponse<T>(ex);
        }

        public static CommandResponse<T> FromResult<T>(object pipelineBehavior, T result)
        {
            return new CommandResponse<T>(pipelineBehavior, result);
        }

        public static Task<CommandResponse<T>> FromResultTask<T>(T result)
        {
            return Task.FromResult(FromResult(result));
        }

        public static Task<CommandResponse<T>> FromResultTask<T>(Exception ex)
        {
            return Task.FromResult(FromResult<T>(ex));
        }

        public static Task<CommandResponse<T>> FromResultTask<T>(object pipelineBehavior, T result)
        {
            return Task.FromResult(FromResult(pipelineBehavior, result));
        }
    }
}
