﻿using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2Flux.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
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
        private IMQ2Context context;
        private IMQ2Logger mq2Logger;

        private SemaphoreSlim semaphore;
        private bool disposedValue;

        public AbilityService(IMQ2Context context, IMQ2Logger mq2Logger)
        {
            this.context = context;
            this.mq2Logger = mq2Logger;

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
                    me.AmICasting() ||
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

                var timeout = TimeSpan.FromMilliseconds(1000);
                var result = false;

                Task waitForEQTask = Task.Run
                (
                    () => context.Chat.WaitForEQ
                    (
                        text =>
                        {
                            if (!string.IsNullOrWhiteSpace(successText) && text.Contains(successText))
                            {
                                result = true;

                                return true;
                            }

                            if (!string.IsNullOrWhiteSpace(failureText) && text.Contains(failureText))
                            {
                                result = false;

                                return true;
                            }

                            return false;
                        },
                        timeout,
                        cancellationToken
                    )
                );

                DoAbility(abilityName);

                await waitForEQTask;

                return result;
            }
            catch (TimeoutException)
            {
                return false;
            }
            finally
            {
                semaphore.Release();
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

        private void DoAbility(string abilityName)
        {
            context.MQ2.DoCommand($"/doability {abilityName}");

            mq2Logger.Log($"Do ability [\ay{abilityName}\aw]", TimeSpan.Zero);
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
