using System;
using System.Diagnostics;
using System.Threading;
using NPageObject.Exceptions;

namespace NPageObject
{
    public class DelegateHelper
    {
        /// <summary>
        /// Runs a delegate repeatedly until the delegate indicates completion or a timeout occurs (at which point a failure delegate is invoked), whichever is the sooner.
        /// </summary>
        public static void InvokeRepeatedly<TDelegateDto, TOutputValue>(
            RepeatedlyInvocableDelegate<TDelegateDto, TOutputValue> @delegate,
            out TOutputValue outputValue,
            TDelegateDto dto = default(TDelegateDto),
            Action failureAction = default(Action),
            int pollingIntervalInMilliseconds = 250,
            int maximumElapsedTimeInSeconds = 15,
            int initialWaitInMilliseconds = 0)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(initialWaitInMilliseconds));
            //used to improve selection reliability
            var pollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMilliseconds);
            var maximumElapsedTime = TimeSpan.FromSeconds(maximumElapsedTimeInSeconds);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (;;)
            {
                TOutputValue delegateOutputValue;
                var continueDelegateInvocation = @delegate(dto, out delegateOutputValue);

                if (continueDelegateInvocation == ShouldRepeatDelegateInvocation.No)
                {
                    outputValue = delegateOutputValue;

                    return;
                }

                if (TimeSpan.FromMilliseconds(stopwatch.Elapsed.TotalMilliseconds) > maximumElapsedTime)
                {
                    if (failureAction == default(Action))
                    {
                        throw new DelegateInvocationTimeoutException();
                    }

                    failureAction();

                    throw new DelegateInvocationTimeoutException();
                }

                Thread.Sleep(pollingInterval);
            }
        }
    }
}