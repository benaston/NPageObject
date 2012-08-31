namespace NPageObject
{
    public interface IBrowserActionPerformer
    {
        TNewPage NavigateTo<TDriver, TNewPage>(string uriContentsRelativeToRoot)
            where TNewPage : PageObject<TDriver, TNewPage>, IHasMutableUrl, new();

        TNewPage NavigateTo<TDriver, TNewPage>()
            where TNewPage : PageObject<TDriver, TNewPage>, new();

        IElementOn<TDriver, TPage> Click<TDriver, TPage>(IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new();

        IElementOn<TDriver, TPage> InputText<TDriver, TPage>(IElementOn<TDriver, TPage> element,
                                                   string text)
            where TPage : PageObject<TDriver, TPage>, new();

        IElementOn<TDriver, TPage> PressKey<TDriver, TPage>(IElementOn<TDriver, TPage> element, Key key)
            where TPage : PageObject<TDriver, TPage>, new();

        IElementOn<TDriver, TPage> PressEnter<TDriver, TPage>(IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new();

        TNewPage BrowseTo<TDriver, TNewPage>()
            where TNewPage : PageObject<TDriver, TNewPage>, new();

        IElementOn<TDriver, TPage> Hover<TDriver, TPage>(IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new();

        IElementOn<TDriver, TPage> ClearValue<TDriver, TPage>(IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new();

        void MaximizeBrowserWindow();

        ITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
            <TDriver, TExpectedSourcePage, TExpectedResultantPage>(
            IElementOn<TDriver, TExpectedSourcePage> element)
            where TExpectedSourcePage : PageObject<TDriver, TExpectedSourcePage>, new()
            where TExpectedResultantPage : PageObject<TDriver, TExpectedResultantPage>, new();

        TPage SelectFromDropDown<TDriver, TPage>(IElementOn<TDriver, TPage> element, string value)
            where TPage : PageObject<TDriver, TPage>, new();

        TPage SelectFromDropDown<TDriver, TPage>(IElementOn<TDriver, TPage> element, int index)
            where TPage : PageObject<TDriver, TPage>, new();
    }
}