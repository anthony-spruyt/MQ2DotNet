using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Queries;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class CanProcessQueryHandler : IRequestHandler<CanProcessQuery, bool>
    {
        private readonly IEventService eventService;

        public CanProcessQueryHandler(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public Task<bool> Handle(CanProcessQuery request, CancellationToken cancellationToken)
        {
            var canProcess =
                eventService.GameState == GameState.InGame &&
                !eventService.Camping &&
                !eventService.Zoning;

            return Task.FromResult(canProcess);
        }
    }
}
