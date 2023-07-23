using System;

namespace MQFlux.Core
{
    public abstract class ResponseWithResult<TResult> : Response
    {
        public TResult Result { get; }

        public ResponseWithResult(Exception ex) : base(ex)
        {
            Result = default;
        }

        public ResponseWithResult(TResult result) : base()
        {
            Result = result;
        }
    }
}
