using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Utils
{
    public static class Wait
    {
        /// <summary>
        /// Wait until a condition evaluates as false with a timeout.
        /// </summary>
        /// <param name="thisIsTrue"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>False if timed out, otherwise True</returns>
        public static async Task<bool> While(Func<bool> thisIsTrue, TimeSpan timeout, CancellationToken cancellationToken)
        {
            using (CancellationTokenSource timeOutCancellationTokenSource = new CancellationTokenSource(timeout))
            {
                using (CancellationTokenSource linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeOutCancellationTokenSource.Token, cancellationToken))
                {
                    while (thisIsTrue())
                    {

                        try
                        {
                            await MQFlux.Yield(linkedCancellationTokenSource.Token);
                        }
                        catch (OperationCanceledException) { }

                        if (linkedCancellationTokenSource.Token.IsCancellationRequested)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
