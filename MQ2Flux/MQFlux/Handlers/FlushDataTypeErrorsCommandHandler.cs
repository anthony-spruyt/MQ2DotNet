using MediatR;
using Microsoft.Extensions.Logging;
using MQ2DotNet.MQ2API;
using MQFlux.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class FlushDataTypeErrorsCommandHandler : IRequestHandler<FlushDataTypeErrorsCommand>
    {
        private readonly ILogger<FlushDataTypeErrorsCommandHandler> logger;

        public FlushDataTypeErrorsCommandHandler(ILogger<FlushDataTypeErrorsCommandHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(FlushDataTypeErrorsCommand request, CancellationToken cancellationToken)
        {
            if (!MQ2DataType.DataTypeErrors.Any())
            {
                return Task.CompletedTask;
            }

            try
            {
                var keys = MQ2DataType.DataTypeErrors.Keys.ToArray();

                foreach (var key in keys)
                {
                    if (MQ2DataType.DataTypeErrors.TryRemove(key, out var ex))
                    {
                        logger.LogError(ex, "MQ2DataType error -> {key}", key);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to dump MQ2DataType errors.");
            }

            return Task.CompletedTask;
        }
    }
}
