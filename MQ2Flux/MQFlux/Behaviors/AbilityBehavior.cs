using MediatR;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Core;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IAbilityRequest : IContextRequest
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

    public class AbilityBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IAbilityRequest abilityRequest)
            {
                abilityRequest.Ability = abilityRequest.Context.TLO.Me.GetAbilityInfo(abilityRequest.AbilityName);

                if (abilityRequest.Ability == null || abilityRequest.Ability.Level == 0 || !abilityRequest.Ability.Ready)
                {
                    return CommandResponse.FromResultTask(false);
                }
            }

            return next();
        }
    }
}
