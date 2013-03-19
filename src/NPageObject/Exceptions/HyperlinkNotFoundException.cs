using System;
using NPageObject.PageObject;

namespace NPageObject.Exceptions
{
    public class HyperlinkNotFoundException<TPage> : Exception
        where TPage : PageObject<TPage>, new()
    {
        private const string ElementNotFoundMessage = "Unable to find hyperlink on page.";

        public HyperlinkNotFoundException(string hyperlinkText)
            : base(string.Format("{0} Text to find: {1}.", ElementNotFoundMessage, hyperlinkText)) { }

        public HyperlinkNotFoundException(string hyperlinkText, string hyperlinkHref)
            : base(string.Format("{0} Text to find: {1}. Page source: {2}",
                              ElementNotFoundMessage,
                              hyperlinkText,
                              hyperlinkHref)) { }

        public HyperlinkNotFoundException(string message, Exception innerException)
            : base(string.Format("{0} Message: {1}.", ElementNotFoundMessage, message),
                innerException: innerException) { }
    }
}