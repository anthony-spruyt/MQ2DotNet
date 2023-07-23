using MediatR;
using Microsoft.Extensions.Logging;
using MQFlux.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class QueryExceptionBehavior<TRequest, TResult> : QueryBehavior<TRequest, TResult>
        where TRequest : Query<TResult>
    {
        private readonly ILogger<QueryExceptionBehavior<TRequest, TResult>> logger;

        public QueryExceptionBehavior(ILogger<QueryExceptionBehavior<TRequest, TResult>> logger)
        {
            this.logger = logger;
        }

        public override Task<QueryResponse<TResult>> Handle(TRequest request, RequestHandlerDelegate<QueryResponse<TResult>> next, CancellationToken cancellationToken)
        {
            try
            {
                return next();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Query error");

                return QueryResponse.FromResultTask<TResult>(ex);
            }
        }
    }
}
