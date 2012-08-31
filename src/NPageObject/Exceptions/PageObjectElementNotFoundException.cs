using System;

namespace NPageObject.Exceptions
{
    public class PageObjectElementNotFoundException<TDriver, TPage> : Exception
        where TPage : PageObject<TDriver, TPage>, new()
    {
        private const string ElementNotFoundMessage =
            "Unable to find element on page to match selector and/or text. Check your page object definitions.";

        public PageObjectElementNotFoundException(IElementOn<TDriver, TPage> poe)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}.",
                              ElementNotFoundMessage,
                              poe.SelectorFullyQualified,
                              string.Join(" ", poe.SelectorsFullyQualified),
                              poe.Text)) { }

        public PageObjectElementNotFoundException(IElementOn<TDriver, TPage> poe, string pageSource)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}. Page source: {4}",
                              ElementNotFoundMessage,
                              poe.SelectorFullyQualified,
                              string.Join(" ", poe.SelectorsFullyQualified),
                              poe.Text,
                              pageSource)) { }

        public PageObjectElementNotFoundException(IElementOn<TDriver, TPage> poe,
                                                  Exception innerException)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {2}.",
                              ElementNotFoundMessage,
                              poe.SelectorFullyQualified,
                              string.Join(" ", poe.SelectorsFullyQualified),
                              poe.Text),
                innerException: innerException) { }
    }
}