using MediatR;
using Microsoft.Extensions.Logging;
using MQ2DotNet.MQ2API;
using MQFlux.Core;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace MQFlux.Commands
{
    public class FlushDataTypeErrorsCommand : Command<Unit>
    {
    }

    public class FlushDataTypeErrorsCommandHandler : CommandHandler<FlushDataTypeErrorsCommand, Unit>
    {
        private readonly ILogger<FlushDataTypeErrorsCommandHandler> logger;

        public FlushDataTypeErrorsCommandHandler(ILogger<FlushDataTypeErrorsCommandHandler> logger)
        {
            this.logger = logger;
        }

        public override Task<CommandResponse<Unit>> Handle(FlushDataTypeErrorsCommand request, CancellationToken cancellationToken)
        {
            if (!MQ2DataType.DataTypeErrors.Any())
            {
                return CommandResponse.FromResultTask(Unit.Value);
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

            return CommandResponse.FromResultTask(Unit.Value);
        }
    }
}
