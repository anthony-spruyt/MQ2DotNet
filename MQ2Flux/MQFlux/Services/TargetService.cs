using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Utils;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface ITargetService
    {
        Task<bool> ClearTarget(CancellationToken cancellationToken = default);
        Task<bool> CycleGroupMembers(CancellationToken cancellationToken = default);
        Task<bool> CycleFriendlies(uint distance, CancellationToken cancellationToken = default);
        Task<bool> CycleEnemies(uint distance, CancellationToken cancellationToken = default);
        Task<bool> CycleXTargets(CancellationToken cancellationToken = default);
        Task<bool> Target(GroundType ground, CancellationToken cancellationToken = default);
        Task<bool> Target(SpawnType spawn, CancellationToken cancellationToken = default);
        Task<bool> Target(SwitchType @switch, CancellationToken cancellationToken = default);
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
        private readonly IContext context;
        private readonly IMQLogger mqLogger;

        public TargetService(IContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
        }

        public Task<bool> ClearTarget(CancellationToken cancellationToken = default)
        {
            if (context.TLO.Target == null)
            {
                // Already clear
                return Task.FromResult(true);
            }

            mqLogger.Log("Clear target");
            context.MQ.DoCommand("/target clear");

            if (context.TLO.Target == null)
            {
                return Task.FromResult(true);
            }

            return Wait.While(() => context.TLO.Target != null, TimeSpan.FromSeconds(1), cancellationToken);
        }

        public Task<bool> CycleEnemies(uint distance, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CycleFriendlies(uint distance, CancellationToken cancellationToken = default)
        {
            try
            {
                var spawns = context.Spawns.All.Where(i => !i.Aggressive && i.Distance3D < distance);

                foreach (var spawn in spawns)
                {
                    await Target(spawn, cancellationToken);

                    await Wait.While(() => !spawn.BuffsPopulated, TimeSpan.FromSeconds(5), cancellationToken);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public Task<bool> CycleGroupMembers(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CycleXTargets(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Target(GroundType ground, CancellationToken cancellationToken = default)
        {
            var id = ground.ID.GetValueOrDefault(0);

            if ((context.TLO.ItemTarget.ID).GetValueOrDefault(0) == id)
            {
                // Already targetted
                return Task.FromResult(true);
            }

            ground.DoTarget();
            mqLogger.Log($"Target ground item \a[{ground.Name}]");

            return Wait.While(() => context.TLO.ItemTarget == null || context.TLO.ItemTarget.ID.GetValueOrDefault(0) != id, TimeSpan.FromSeconds(1), cancellationToken);
        }

        public Task<bool> Target(SpawnType spawn, CancellationToken cancellationToken = default)
        {
            var id = spawn.ID.GetValueOrDefault(0u);

            if ((context.TLO.Target?.ID).GetValueOrDefault(0u) == id)
            {
                // Already targetted
                return Task.FromResult(true);
            }

            spawn.DoTarget();
            mqLogger.Log($"Target spawn \a[{spawn.Name}]");

            return Wait.While(() => context.TLO.Target == null || context.TLO.Target.ID.GetValueOrDefault(0u) != id, TimeSpan.FromSeconds(1), cancellationToken);
        }

        public Task<bool> Target(SwitchType @switch, CancellationToken cancellationToken = default)
        {
            if (@switch.IsTargeted)
            {
                // Already targetted
                return Task.FromResult(true);
            }

            @switch.Target();
            mqLogger.Log($"Target switch \a[{@switch.Name}]");

            return Wait.While(() => !@switch.IsTargeted, TimeSpan.FromSeconds(1), cancellationToken);
        }
    }
}
