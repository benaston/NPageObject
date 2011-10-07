// ReSharper disable InconsistentNaming
namespace NPageObject.Exceptions
{
    using System;
    using NHelpfulException;

    public class GGHyperlinkNotFoundException<TPage> : HelpfulException
        where TPage : IPageObject<TPage>, new()
    {
        private const string _elementNotFoundMessage = "Unable to find hyperlink on page.";

        public GGHyperlinkNotFoundException(string hyperlinkText)
            : base(string.Format("{0} Text to find: {1}.", _elementNotFoundMessage, hyperlinkText)) {}

        public GGHyperlinkNotFoundException(string hyperlinkText, string hyperlinkHref)
            : base(
                string.Format("{0} Text to find: {1}. Page source: {2}", _elementNotFoundMessage, hyperlinkText,
                              hyperlinkHref)) {}

        public GGHyperlinkNotFoundException(string message, Exception innerException)
            : base(
                string.Format("{0} Message: {1}.", _elementNotFoundMessage, message), innerException: innerException) {}
    }
}
// ReSharper restore InconsistentNaming