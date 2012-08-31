using System;
using System.Text.RegularExpressions;

namespace NPageObject
{
    public class UriExpectationHelper
    {
        /// <summary>
        /// Returns true if actual URI from the context matches the expected URI from the PageObject.
        /// </summary>
        public static bool DoesActualMatchExpectedUri<TDriver, TPage>(TPage page, ITestContext<TDriver, TPage> uiTestContext)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            switch (page.UriExpectation.UriMatch)
            {
                case UriMatch.Exact:
                    return uiTestContext.UriActualAbsolute ==
                           page.UriRoot + page.UriExpectation.UriContentsRelativeToRoot;
                case UriMatch.Partial:
                    return
                        uiTestContext.UriActualAbsolute.Contains(page.UriExpectation.UriContentsRelativeToRoot);
                case UriMatch.Regex:
                    return Regex.IsMatch(uiTestContext.UriActualAbsolute,
                                         page.UriExpectation.UriContentsRelativeToRoot);
                default:
                    throw new Exception("Invalid UriMatch value.");
            }
        }
    }
}