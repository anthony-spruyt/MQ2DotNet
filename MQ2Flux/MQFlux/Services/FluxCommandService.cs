using MediatR;
using MQFlux.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface IFluxCommand : IMQCommand
    {

    }

    public class FluxCommandService : IFluxCommand, IDisposable
    {
        public string Command => "/flux";
        public CancellationToken CancellationToken => cancellationTokenSource.Token;

        private readonly IMQLogger mqLogger;
        private readonly IMediator mediator;

        private bool disposedValue;
        private CancellationTokenSource cancellationTokenSource;
        private SemaphoreSlim semaphore;

        public FluxCommandService(IMQLogger logger, IMediator mediator)
        {
            this.mqLogger = logger;
            this.mediator = mediator;

            cancellationTokenSource = new CancellationTokenSource();
            semaphore = new SemaphoreSlim(1);

        }

        public void Handle(string[] args)
        {
            _ = Task.Run
            (
                async () =>
                {
                    if (semaphore == null || !await semaphore.WaitAsync(0))
                    {
                        mqLogger.Log($"The previous command has not yet completed");

                        return;
                    }

                    try
                    {
                        if (args == null || args.Length == 0)
                        {
                            mqLogger.Log($"No command argument provided.");

                            LogHelp();

                            return;
                        }

                        if (args.Any(i => new string[] { "quit", "--quit", "-q" }.Contains(i)))
                        {
                            cancellationTokenSource.Cancel();

                            return;
                        }

                        if (args.Any(i => new string[] { "pause", "--pause", "-p" }.Contains(i)))
                        {
                            await mediator.Send(new PauseCommand(), cancellationTokenSource.Token);

                            return;
                        }

                        if (args.Any(i => new string[] { "resume", "--resume", "-r" }.Contains(i)))
                        {
                            await mediator.Send(new ResumeCommand(), cancellationTokenSource.Token);

                            return;
                        }

                        if (args.Any(i => new string[] { "test", "--test", "-t" }.Contains(i)))
                        {
                            await mediator.Send(new TestCommand(), cancellationTokenSource.Token);

                            return;
                        }

                        if (args.Any(i => new string[] { "help", "--help", "-h" }.Contains(i)))
                        {
                            LogHelp();

                            return;
                        }

                        mqLogger.Log($"Unknown command argument");

                        LogHelp();

                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        mqLogger.LogError(ex);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }
            ).ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (cancellationTokenSource != null)
                    {
                        cancellationTokenSource.Dispose();
                        cancellationTokenSource = null;
                    }

                    if (semaphore != null)
                    {
                        semaphore.Dispose();
                        semaphore = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void LogHelp()
        {
            mqLogger.Log("TODO help");
        }
    }
}
