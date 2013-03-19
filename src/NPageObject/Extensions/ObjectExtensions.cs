using System;
using System.Threading;

namespace NPageObject.Extensions
{
    public static class ObjectExtensions
    {
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