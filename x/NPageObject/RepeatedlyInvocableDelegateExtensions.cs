using System;

namespace Tests.Common.PageObject
{
    public static class RepeatedlyInvocableDelegateExtensions
    {
        /// <summary>
        /// Facade onto the TestExtensions.InvokeRepeatedly method.
        /// </summary>
        public static TOutputValue InvokeRepeatedlyForUpTo<TDelegateDto, TOutputValue>(
            this RepeatedlyInvocableDelegate<TDelegateDto, TOutputValue> d,
            TimeSpan maximumElapsedTime,
            TDelegateDto dto = default(TDelegateDto),
            Action failureAction = default(Action),
            int pollingIntervalInMilliseconds = 250,
            int maximumElapsedTimeInSeconds = 15,
            int initialWaitInMilliseconds = 0)
        {
            TOutputValue outputValue;
            DelegateHelper.InvokeRepeatedly(d,
                                            out outputValue,
                                            dto,
                                            failureAction,
                                            pollingIntervalInMilliseconds,
                                            maximumElapsedTimeInSeconds,
                                            initialWaitInMilliseconds);

            return outputValue;
        }
    }
}