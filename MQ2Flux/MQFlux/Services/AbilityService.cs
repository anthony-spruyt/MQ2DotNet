using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface IAbilityService
    {
        Task<bool> DoAbilityAsync(AbilityInfo ability, string successText = null, string failureText = null, CancellationToken cancellationToken = default);
        Task<bool> DoAbilityAsync(string abilityName, string successText = null, string failureText = null, CancellationToken cancellationToken = default);
    }

    public static class IAbilityServiceExtensions
    {
        public static IServiceCollection AddAbilityService(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAbilityService, AbilityService>();
        }
    }

    public class AbilityService : IAbilityService, IDisposable
    {
        private readonly IMQContext context;
        private readonly IMQLogger mqLogger;

        private SemaphoreSlim semaphore;
        private bool disposedValue;

        public AbilityService(IMQContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;

            semaphore = new SemaphoreSlim(1);
        }

        public async Task<bool> DoAbilityInternalAsync(AbilityInfo ability, string successText = null, string failureText = null, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                var me = context.TLO.Me;

                if
                (
                    ability == null ||
                    !ability.Ready ||
                    (
                        me.AmICasting() &&
                        string.Compare(me.Spawn.Class.ShortName, "BRD") != 0
                    ) ||
                    (
                        me.Spawn.Class.CanCast &&
                        context.TLO.IsSpellBookOpen()
                    )
                )
                {
                    return false;
                }

                var abilityName = ability.Skill.Name;

                if (string.IsNullOrWhiteSpace(successText) && string.IsNullOrWhiteSpace(failureText))
                {
                    DoAbility(abilityName);

                    return true;
                }

                var timeout = TimeSpan.FromMilliseconds(2000);
                var success = false;

                Task<bool> waitForEQTask = Task.Run
                (
                    () => context.Chat.WaitForEQ
                    (
                        text =>
                        {
                            if (!string.IsNullOrWhiteSpace(successText) && text.Contains(successText))
                            {
                                success = true;

                                return true;
                            }

                            if (!string.IsNullOrWhiteSpace(failureText) && text.Contains(failureText))
                            {
                                success = false;

                                return true;
                            }

                            return false;
                        },
                        timeout,
                        cancellationToken
                    )
                );

                DoAbility(abilityName);

                var foundAbilityResult = await waitForEQTask;

                return foundAbilityResult && success;
            }
            finally
            {
                semaphore.Release();
            }

            void DoAbility(string abilityName)
            {
                context.MQ.DoCommand($"/doability {abilityName}");

                mqLogger.Log($"Do ability [\au{abilityName}\aw]", TimeSpan.Zero);
            }
        }

        public async Task<bool> DoAbilityAsync(AbilityInfo ability, string successText = null, string failureText = null, CancellationToken cancellationToken = default)
        {
            return await DoAbilityInternalAsync(ability, successText, failureText, cancellationToken);
        }

        public async Task<bool> DoAbilityAsync(string abilityName, string successText = null, string failureText = null, CancellationToken cancellationToken = default)
        {
            var ability = context.TLO.Me.GetAbilityInfo(abilityName);

            return await DoAbilityInternalAsync(ability, successText, failureText, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (semaphore != null)
                    {
                        semaphore.Dispose();
                        semaphore = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AbilityService()
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
