namespace NPageObject
{
    public interface IDomChecker
    {
        bool TextContains<TDriver, TPage>(IElementOn<TDriver, TPage> element, string text)
            where TPage : PageObject<TDriver, TPage>, new();

        string GetAttributeValue<TDriver, TPage>(IElementOn<TDriver, TPage> element, string attributeName)
            where TPage : PageObject<TDriver, TPage>, new();

        string GetText<TDriver, TPage>(IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new();

        string GetDropDownListSelectedItemText<TDriver, TPage>(IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new();

        bool IsVisible<TDriver, TPage>(IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new();

        bool IsTextVisibleStrict<TDriver, TPage>(string text) where TPage : PageObject<TDriver, TPage>, new();

        bool IsTextVisible<TDriver, TPage>(string text) where TPage : PageObject<TDriver, TPage>, new();

        bool ContainsLinkWithText<TDriver, TPage>(string text) where TPage : PageObject<TDriver, TPage>, new();

        bool ContainsLinkWithTextAndHref<TDriver, TPage>(string text, string href)
            where TPage : PageObject<TDriver, TPage>, new();
    }
}