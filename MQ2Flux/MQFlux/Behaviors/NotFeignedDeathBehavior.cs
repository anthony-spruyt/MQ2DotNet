﻿using MediatR;
using MQ2DotNet.EQ;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotFeignedDeathRequest : IContextRequest
    {

    }

    public class NotFeignedDeathBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INotFeignedDeathRequest notFeignedDeathRequest &&
                notFeignedDeathRequest.Context.TLO.Me.Spawn.State == SpawnState.Feign)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}

