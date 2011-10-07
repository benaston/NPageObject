namespace NPageObject
{
    /// <summary>
    ///   Defines the iface for types that permit 
    ///   analysis of the DOM of a webpage.
    /// </summary>
    public interface IDomChecker
    {
        bool TextContains<TPage>(IPageObjectElement<TPage> element, string text) where TPage : IPageObject<TPage>, new();

        string GetAttributeValue<TPage>(IPageObjectElement<TPage> element, string attributeName)
            where TPage : IPageObject<TPage>, new();

        string GetText<TPage>(IPageObjectElement<TPage> element) where TPage : IPageObject<TPage>, new();

        string GetDropDownListSelectedItemText<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new();

        bool IsVisible<TPage>(IPageObjectElement<TPage> element) where TPage : IPageObject<TPage>, new();

        bool IsTextVisibleStrict<TPage>(string text) where TPage : IPageObject<TPage>, new();

        bool IsTextVisible<TPage>(string text) where TPage : IPageObject<TPage>, new();

        bool ContainsLinkWithText<TPage>(string text) where TPage : IPageObject<TPage>, new();

        bool ContainsLinkWithTextAndHref<TPage>(string text, string href) where TPage : IPageObject<TPage>, new();
    }
}