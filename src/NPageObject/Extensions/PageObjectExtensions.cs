using System;
using System.Threading;

namespace NPageObject.Extensions
{
    public static class PageObjectExtensions
    {
        public static bool MatchesActualBrowserLocation<TDriver, TPage>(this TPage page)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            return UriExpectationHelper.DoesActualMatchExpectedUri(page, page.Context) &&
                   page.Context.DomChecker.IsTextVisibleStrict <TDriver, TPage>(page.IdentifyingText);
        }

        public static TPage AndWaitFor<TDriver, TPage>(this TPage page,
                                                    TimeSpan timeSpan,
                                                    string reason)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            if (timeSpan <= TimeSpan.Zero) { throw new ArgumentException("timespan"); }

            Thread.Sleep(timeSpan);

            return page;
        }

        public static bool TextIsVisible<TDriver, TPage>(this TPage page, string text)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return page.Context.DomChecker.IsTextVisible<TDriver, TPage>(text);
        }
    }
}