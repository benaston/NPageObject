using System;
using System.Threading;

namespace NPageObject.x.NPageObject
{
    public static class PageObjectExtensions
    {
        public static bool MatchesActualBrowserLocation<TPage>(this TPage page)
            where TPage : PageObject<TPage>, new()
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            return UriExpectationHelper.DoesActualMatchExpectedUri(page, page.Context) &&
                   page.Context.DomChecker.IsTextVisibleStrict <TPage>(page.IdentifyingText);
        }

        public static TPage AndWaitFor<TPage>(this TPage page,
                                                    TimeSpan timeSpan,
                                                    string reason)
            where TPage : PageObject<TPage>, new()
        {
            if (timeSpan <= TimeSpan.Zero) { throw new ArgumentException("timespan"); }

            Thread.Sleep(timeSpan);

            return page;
        }
    }
}