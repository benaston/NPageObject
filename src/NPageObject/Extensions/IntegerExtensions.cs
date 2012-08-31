using System;

namespace NPageObject.Extensions
{
    public static class IntegerExtensions
    {
        public static TimeSpan Seconds(this int i) { return TimeSpan.FromSeconds(i); }
    }
}