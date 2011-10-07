// ReSharper disable InconsistentNaming
namespace NPageObject.Exceptions
{
    using System;
    using NHelpfulException;

    public class GGPageTextNotFoundException<TPage> : HelpfulException
        where TPage : IPageObject<TPage>, new()
    {
        private const string _elementNotFoundMessage = "Unable to find text on page.";

        public GGPageTextNotFoundException(string textToFind)
            : base(string.Format("{0} Text to find: {1}.", _elementNotFoundMessage, textToFind)) {}

        public GGPageTextNotFoundException(string textToFind, string pageSource)
            : base(
                string.Format("{0} Text to find: {1}. Page source: {2}", _elementNotFoundMessage, textToFind, pageSource)
                ) {}

        public GGPageTextNotFoundException(string textToFind, Exception innerException)
            : base(
                string.Format("{0} Text to find: {1}.", _elementNotFoundMessage, textToFind),
                innerException: innerException) {}
    }
}
// ReSharper restore InconsistentNaming