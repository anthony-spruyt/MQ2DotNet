using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface ITargetService
    {
        Task<bool> ClearTarget(CancellationToken cancellationToken = default);
        Task<bool> CycleEnemies(float? distance = null, CancellationToken cancellationToken = default);
        Task<bool> CycleFriendlies(float? distance = null, CancellationToken cancellationToken = default);
        Task<bool> CycleGroupMembers(CancellationToken cancellationToken = default);
        Task<bool> CycleSpawns(float? distance = null, Predicate<SpawnType> predicate = null, CancellationToken cancellationToken = default);
        Task<bool> CycleXTargets(CancellationToken cancellationToken = default);
        Task<bool> Target(GroundType ground, CancellationToken cancellationToken = default);
        Task<bool> Target(SpawnType spawn, bool waitForBuffsPopulated = false, CancellationToken cancellationToken = default);
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

            //mqLogger.Log("Clear target");
            context.MQ.DoCommand("/target clear");

            if (context.TLO.Target == null)
            {
                return Task.FromResult(true);
            }

            return Wait.While(() => context.TLO.Target != null, TimeSpan.FromSeconds(1), cancellationToken);
        }

        public async Task<bool> CycleEnemies(float? distance = null, CancellationToken cancellationToken = default)
        {
            var spawns = context.TLO
                .Spawns(distance)
                .Where(i => i.Aggressive);

            return await CycleSpawnsInternal(spawns, cancellationToken);
        }

        public async Task<bool> CycleFriendlies(float? distance = null, CancellationToken cancellationToken = default)
        {
            var spawns = context.TLO
                .Spawns(distance)
                .Where(i => !i.Aggressive);

            return await CycleSpawnsInternal(spawns, cancellationToken);
        }

        public async Task<bool> CycleGroupMembers(CancellationToken cancellationToken = default)
        {
            if (!context.TLO.Me.Grouped)
            {
                return false;
            }

            var spawns = context.TLO.Group.GroupMembers
                .Where(i => i.Present)
                .Select(i => i.Spawn);

            return await CycleSpawnsInternal(spawns, cancellationToken);
        }

        public async Task<bool> CycleSpawns(float? distance = null, Predicate<SpawnType> predicate = null, CancellationToken cancellationToken = default)
        {
            var spawns = predicate == null ?
                context.TLO
                   .Spawns(distance) :
                context.TLO
                   .Spawns(distance)
                   .Where(i => predicate(i));

            return await CycleSpawnsInternal(spawns, cancellationToken);
        }

        public async Task<bool> CycleXTargets(CancellationToken cancellationToken = default)
        {
            var spawns = context.TLO.Me.XTargets
                .Select(i => i.Spawn);

            return await CycleSpawnsInternal(spawns, cancellationToken);
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
            //mqLogger.Log($"Target ground item [\ao{ground.Name}\aw]");

            return Wait.While(() => context.TLO.ItemTarget == null || context.TLO.ItemTarget.ID.GetValueOrDefault(0) != id, TimeSpan.FromSeconds(1), cancellationToken);
        }

        public Task<bool> Target(SpawnType spawn, bool waitForBuffsPopulated = false, CancellationToken cancellationToken = default)
        {
            var id = spawn.ID.GetValueOrDefault(0u);

            if ((context.TLO.Target?.ID).GetValueOrDefault(0u) == id)
            {
                // Already targetted
                return Task.FromResult(true);
            }

            spawn.DoTarget();
            //mqLogger.Log($"Target spawn [\ao{spawn.Name}\aw]");

            return Wait.While
            (
                () => 
                    context.TLO.Target == null || 
                    context.TLO.Target.ID.GetValueOrDefault(0u) != id ||
                    (
                        waitForBuffsPopulated &&
                        !context.TLO.Target.BuffsPopulated
                    ),
                TimeSpan.FromSeconds(2),
                cancellationToken
            );
        }

        public Task<bool> Target(SwitchType @switch, CancellationToken cancellationToken = default)
        {
            if (@switch.IsTargeted)
            {
                // Already targetted
                return Task.FromResult(true);
            }

            @switch.Target();
            //mqLogger.Log($"Target switch [\ao{@switch.Name}\aw]");

            return Wait.While(() => !@switch.IsTargeted, TimeSpan.FromSeconds(1), cancellationToken);
        }

        private async Task<bool> CycleSpawnsInternal(IEnumerable<SpawnType> spawns, CancellationToken cancellationToken)
        {
            uint count = 0;
            uint spawnCount = 0;

            foreach (var spawn in spawns)
            {
                spawnCount++;

                if (await Target(spawn, waitForBuffsPopulated: true, cancellationToken))
                {
                    count++;
                }
            }

            if (count != spawnCount)
            {
                return false;
            }

            //mqLogger.Log($"Cycled [\ao{count}\aw] spawns");

            //await ClearTarget(cancellationToken);

            return true;
        }
    }
}
