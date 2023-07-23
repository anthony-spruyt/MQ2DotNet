using System;
using System.Threading.Tasks;

namespace MQFlux.Core
{
    public class QueryResponse<TResult> : ResponseWithResult<TResult>
    {
        public QueryResponse(Exception ex) : base(ex)
        {
        }

        public QueryResponse(TResult result) : base(result)
        {
        }
    }

    public static class QueryResponse
    {
        public static QueryResponse<T> FromResult<T>(T result)
        {
            return new QueryResponse<T>(result);
        }

        public static QueryResponse<T> FromResult<T>(Exception ex)
        {
            return new QueryResponse<T>(ex);
        }

        public static Task<QueryResponse<T>> FromResultTask<T>(T result)
        {
            return Task.FromResult(FromResult(result));
        }

        public static Task<QueryResponse<T>> FromResultTask<T>(Exception ex)
        {
            return Task.FromResult(FromResult<T>(ex));
        }
    }
}
