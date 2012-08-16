using System;

namespace Tests.Common.PageObject
{
    public class PageObjectElementNotFoundException<TPage> : Exception
        where TPage : PageObject<TPage>, new()
    {
        private const string ElementNotFoundMessage =
            "Unable to find element on page to match selector and/or text. Check your page object definitions.";

        public PageObjectElementNotFoundException(IElementOn<TPage> poe)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}.",
                              ElementNotFoundMessage,
                              poe.SelectorFullyQualified,
                              string.Join(" ", poe.SelectorsFullyQualified),
                              poe.Text)) { }

        public PageObjectElementNotFoundException(IElementOn<TPage> poe, string pageSource)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}. Page source: {4}",
                              ElementNotFoundMessage,
                              poe.SelectorFullyQualified,
                              string.Join(" ", poe.SelectorsFullyQualified),
                              poe.Text,
                              pageSource)) { }

        public PageObjectElementNotFoundException(IElementOn<TPage> poe,
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