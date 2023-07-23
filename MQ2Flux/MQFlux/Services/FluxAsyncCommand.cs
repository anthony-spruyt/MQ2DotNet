using MediatR;
using MQFlux.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface IFluxAsyncCommand : IMQAsyncCommand
    {

    }

    public class FluxAsyncCommand : IFluxAsyncCommand, IDisposable
    {
        public string Command => "/flux";

        public CancellationToken CancellationToken => cancellationTokenSource.Token;

        private readonly IMQLogger logger;
        private readonly IMediator mediator;

        private bool disposedValue;
        private CancellationTokenSource cancellationTokenSource;
        private SemaphoreSlim semaphore;

        public FluxAsyncCommand(IMQLogger logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;

            cancellationTokenSource = new CancellationTokenSource();
            semaphore = new SemaphoreSlim(1);

        }

        public async Task Handle(string[] args)
        {
            if (semaphore == null || !await semaphore.WaitAsync(0))
            {
                return;
            }

            try
            {
                if (args == null || args.Length == 0)
                {
                    logger.Log($"No command argument provided.");

                    LogHelp();

                    return;
                }

                if (args.Any(i => string.Compare(i, "stop", true) == 0))
                {
                    cancellationTokenSource.Cancel();

                    return;
                }

                if (args.Any(i => string.Compare(i, "save", true) == 0))
                {
                    await mediator.Send(new SaveConfigCommand(), cancellationTokenSource.Token);
                    //Mediate(new SaveConfigRequest(), cancellationTokenSource.Token);

                    return;
                }

                if (args.Any(i => string.Compare(i, "-t", true) == 0 || string.Compare(i, "--test", true) == 0))
                {
                    await mediator.Send(new TestCommand(), cancellationTokenSource.Token);
                    //Mediate(new TestRequest(), cancellationTokenSource.Token);

                    return;
                }

                if (args.Any(i => string.Compare(i, "-h", true) == 0))
                {
                    LogHelp();

                    return;
                }

                logger.Log($"Unknown command argument");

                LogHelp();

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
            finally
            {
                semaphore.Release();
            }
        }

        //private void Mediate(IRequest request, CancellationToken token)
        //{
        //    _ = Task.Run
        //    (
        //        async () =>
        //        {
        //            try
        //            {
        //                await mediator.Send(request, token);
        //            }
        //            catch (Exception ex)
        //            {
        //                logger.Log($"{this.GetType().Name} failed to mediate the command");
        //                logger.LogError(ex);
        //            }
        //        }
        //    ).ConfigureAwait(false);
        //}

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
            logger.Log("TODO help");
        }
    }
}
