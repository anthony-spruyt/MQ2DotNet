using MediatR;
using Microsoft.Extensions.Logging;
using MQ2DotNet.MQ2API;
using MQ2Flux.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class FlushDataTypeErrorsCommandHandler : IRequestHandler<FlushDataTypeErrorsCommand>
    {
        private readonly ILogger<FlushDataTypeErrorsCommandHandler> logger;

        public FlushDataTypeErrorsCommandHandler(ILogger<FlushDataTypeErrorsCommandHandler> logger)
        {
            this.logger = logger;
        }

        public async Task Handle(FlushDataTypeErrorsCommand request, CancellationToken cancellationToken)
        {
            if (!MQ2DataType.DataTypeErrors.Any())
            {
                return;
            }

            await Task.Run
            (
                () =>
                {
                    try
                    {
                        var keys = MQ2DataType.DataTypeErrors.Keys.ToArray();

                        foreach (var key in keys)
                        {
                            MQ2DataType.DataTypeErrors.TryRemove(key, out var ex);

                            logger.LogError(ex, "{key} in the format of {{name}}_{{index}}_{{typeof(T)}}", key);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to dump MQ2DataType errors.");
                    }
                },
                cancellationToken
            ).ConfigureAwait(false);
        }
    }
}
