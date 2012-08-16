using System;
using System.Text.RegularExpressions;

namespace Tests.Common.PageObject
{
    public class UriExpectationHelper
    {
        /// <summary>
        /// Returns true if actual URI from the context matches the expected URI from the PageObject.
        /// </summary>
        public static bool DoesActualMatchExpectedUri<T>(T page, ITestContext<T> uiTestContext)
            where T : PageObject<T>, new()
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