using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface ITargetService
    {
        Task<bool> ClearTarget();
        Task<bool> CycleGroupMembers();
        Task<bool> CycleFriendlies(uint distance);
        Task<bool> CycleEnemies(uint distance);
        Task<bool> CycleXTargets();
        Task<bool> DoTarget(GroundType ground);
        Task<bool> DoTarget(SpawnType spawn);
        Task<bool> DoTarget(SwitchType @switch);
    }

    public static class TargetServiceExtensions
    {
        public static IServiceCollection AddTargetService(this IServiceCollection services)
        {
            return services
                .AddSingleton<ITargetService, TargetService>();
        }
    }

    public class TargetService : ITargetService
    {
        public Task<bool> ClearTarget()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CycleEnemies(uint distance)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CycleFriendlies(uint distance)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CycleGroupMembers()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CycleXTargets()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DoTarget(GroundType ground)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DoTarget(SpawnType spawn)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DoTarget(SwitchType @switch)
        {
            throw new System.NotImplementedException();
        }
    }
}
