using System;

namespace MQFlux.Core
{
    public abstract class Response
    {
        public Exception Exception { get; }

        public Response(Exception ex)
        {
            Exception = ex;
        }

        public Response()
        {
            Exception = null;
        }
    }
}
