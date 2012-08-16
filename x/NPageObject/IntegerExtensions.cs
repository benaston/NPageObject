using System;

namespace Tests.Common.PageObject
{
    public static class IntegerExtensions
    {
        public static TimeSpan Seconds(this int i) { return TimeSpan.FromSeconds(i); }
    }
}