// ReSharper disable InconsistentNaming
namespace NPageObject
{
    using System;
    using System.Threading;
    using NSure;
    using ArgumentNullException = NHelpfulException.FrameworkExceptions.ArgumentNullException;

    public static class IPageObjectExtensions
    {
        /// <summary>
        ///   Pause execution for a specified interval. Useful 
        ///   in rare cases where we know progressing without 
        ///   a wait will result in problems.
        /// </summary>
        /// <typeparam name = "TPage">The expected type of page object.</typeparam>
        /// <param name = "pageObject">The page object to return.</param>
        /// <param name = "timeSpan">Duration to wait for.</param>
        /// <param name = "reason">Please explain the reason for the wait as 
        ///   the underlying driver wrapper ensures robust selection for most 
        ///   cases. This string is unused programatically.</param>
        /// <returns></returns>
        public static TPage AndWaitFor<TPage>(this TPage pageObject, TimeSpan timeSpan, string reason)
            where TPage : IPageObject<TPage>, new()
        {
            Ensure.That<ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

            Thread.Sleep(timeSpan);

            return pageObject;
        }
    }
}

// ReSharper restore InconsistentNaming