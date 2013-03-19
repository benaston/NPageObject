using NPageObject.PageObject;

namespace NPageObject
{
    public interface IDomChecker
    {
        bool TextContains<TPage>(IElementOn<TPage> element, string text)
            where TPage : PageObject<TPage>, new();

        string GetAttributeValue<TPage>(IElementOn<TPage> element, string attributeName)
            where TPage : PageObject<TPage>, new();

        string GetStyleValue<TPage>(IElementOn<TPage> element, string attributeName)
            where TPage : PageObject<TPage>, new();

        string GetText<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new();

        string GetDropDownListSelectedItemText<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new();

        bool IsVisible<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new();

        bool IsTextVisibleStrict<TPage>(string text) where TPage : PageObject<TPage>, new();

        bool IsTextVisible<TPage>(string text) where TPage : PageObject<TPage>, new();

        bool ContainsLinkWithText<TPage>(string text) where TPage : PageObject<TPage>, new();

        bool ContainsLinkWithTextAndHref<TPage>(string text, string href)
            where TPage : PageObject<TPage>, new();
    }
}