using System;

namespace NPageObject.x.NPageObject
{
    public static class IntegerExtensions
    {
        public static TimeSpan Seconds(this int i) { return TimeSpan.FromSeconds(i); }
    }
}