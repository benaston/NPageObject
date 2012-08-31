using System;

namespace NPageObject.Exceptions
{
    public class PageTextNotFoundException<TDriver, TPage> : Exception
        where TPage : PageObject<TDriver, TPage>, new()
    {
        private const string ExceptionMessage = "Unable to find text on page.";

        public PageTextNotFoundException(string textToFind)
            : base(string.Format("{0} Text to find: {1}.", ExceptionMessage, textToFind)) { }

        public PageTextNotFoundException(string textToFind, string pageSource)
            : base(
                string.Format("{0} Text to find: {1}. Page source: {2}",
                              ExceptionMessage,
                              textToFind,
                              pageSource)
                ) { }

        public PageTextNotFoundException(string textToFind, Exception innerException)
            : base(
                string.Format("{0} Text to find: {1}.", ExceptionMessage, textToFind),
                innerException: innerException) { }
    }
}