using MediatR;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2Flux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface IAbilityRequest : IMQContextRequest
    {
        /// <summary>
        /// Readonly ability name as determined by the request implementation.
        /// </summary>
        string AbilityName { get; }
        /// <summary>
        /// This is set by the middleware, do not set when creating a request.
        /// </summary>
        AbilityInfo Ability { get; set; }
    }

    public class AbilityBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IAbilityRequest abilityRequest)
            {
                abilityRequest.Ability = abilityRequest.Context.TLO.Me.GetAbilityInfo(abilityRequest.AbilityName);
            
                if (abilityRequest.Ability == null || !abilityRequest.Ability.Ready)
                {
                    return Task.FromResult(default(TResponse));
                }
            }

            return next();
        }
    }
}
