using System;

namespace Tests.Common.PageObject
{
    public class HyperlinkNotFoundException<TPage> : Exception
        where TPage : PageObject<TPage>, new()
    {
        private const string _elementNotFoundMessage = "Unable to find hyperlink on page.";

        public HyperlinkNotFoundException(string hyperlinkText)
            : base(string.Format("{0} Text to find: {1}.", _elementNotFoundMessage, hyperlinkText)) { }

        public HyperlinkNotFoundException(string hyperlinkText, string hyperlinkHref)
            : base(
                string.Format("{0} Text to find: {1}. Page source: {2}",
                              _elementNotFoundMessage,
                              hyperlinkText,
                              hyperlinkHref)) { }

        public HyperlinkNotFoundException(string message, Exception innerException)
            : base(
                string.Format("{0} Message: {1}.", _elementNotFoundMessage, message),
                innerException: innerException) { }
    }
}