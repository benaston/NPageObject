namespace NPageObject
{
    using System;
    using System.Threading;

    public static class ObjectExtensions
    {
        public static T AfterWaiting<T>(this T o, TimeSpan waitTime)
        {
            Thread.Sleep(waitTime);

            return o;
        }

        public static T AfterPossiblyWaiting<T>(this T o, TimeSpan waitTime, Func<bool> shouldWait)
        {
            if (shouldWait())
            {
                Thread.Sleep(waitTime);
            }

            return o;
        }
    }
}