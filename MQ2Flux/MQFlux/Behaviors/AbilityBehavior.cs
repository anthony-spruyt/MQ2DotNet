using MediatR;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IAbilityRequest : IMQContextRequest
    {
        /// <summary>
        /// This is set by the middleware, do not set when creating a request.
        /// </summary>
        AbilityInfo Ability { get; set; }
        /// <summary>
        /// Readonly ability name as determined by the request implementation.
        /// </summary>
        string AbilityName { get; }
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
