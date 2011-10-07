// ReSharper disable InconsistentNaming
namespace NPageObject.Exceptions
{
    using System;
    using NHelpfulException;

    public class GGPageObjectElementNotFoundException<TPage> : HelpfulException
        where TPage : IPageObject<TPage>, new()
    {
        private const string _elementNotFoundMessage =
            "Unable to find element on page to match selector and/or text. Check your page object definitions.";

        public GGPageObjectElementNotFoundException(IPageObjectElement<TPage> poe)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}.", _elementNotFoundMessage,
                              poe.SelectorFullyQualified, string.Join(" ", poe.SelectorsFullyQualified), poe.Text)) {}

        public GGPageObjectElementNotFoundException(IPageObjectElement<TPage> poe, string pageSource)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}. Page source: {4}",
                              _elementNotFoundMessage, poe.SelectorFullyQualified,
                              string.Join(" ", poe.SelectorsFullyQualified),
                              poe.Text, pageSource)) {}

        public GGPageObjectElementNotFoundException(IPageObjectElement<TPage> poe, Exception innerException)
            : base(
                string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {2}.", _elementNotFoundMessage,
                              poe.SelectorFullyQualified, string.Join(" ", poe.SelectorsFullyQualified),
                              poe.Text), innerException: innerException) {}
    }
}
// ReSharper restore InconsistentNaming