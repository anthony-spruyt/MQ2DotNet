using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Utils
{
    public static class Wait
    {
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
