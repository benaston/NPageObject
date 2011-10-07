namespace NPageObject
{
    using System;
    using System.Text.RegularExpressions;

    public class UriExpectationHelper
    {
        /// <summary>
        ///   Returns true if actual URI from the context matches the 
        ///   expected URI from the PageObject.
        /// </summary>
        public static bool DoesActualMatchExpectedUri<T>(T page, IUITestContext<T> uiTestContext)
            where T : IPageObject<T>, new()
        {
            switch (page.UriExpectation.Match)
            {
                case UriMatch.Exact:
                    return uiTestContext.UriActualAbsolute ==
                           page.UriRoot + page.UriExpectation.UriContentsRelativeToRoot;
                case UriMatch.Partial:
                    return uiTestContext.UriActualAbsolute.Contains(page.UriExpectation.UriContentsRelativeToRoot);
                case UriMatch.Regex:
                    return Regex.IsMatch(uiTestContext.UriActualAbsolute, page.UriExpectation.UriContentsRelativeToRoot);
                default:
                    throw new Exception("Invalid UriMatch value.");
            }
        }
    }
}