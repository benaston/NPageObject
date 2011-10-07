namespace NPageObject
{
    public interface IBrowserActionPerformer
    {
        IPageObjectElement<TPage> PressKey<TPage>(IPageObjectElement<TPage> element, Key key)
            where TPage : IPageObject<TPage>, new();

        IPageObjectElement<TPage> PressEnter<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new();

        TNewPage BrowseTo<TNewPage>(string uriContentsRelativeToRoot)
            where TNewPage : IPageObject<TNewPage>, IHaveMutableUrl, new();

        TNewPage BrowseTo<TNewPage>()
            where TNewPage : IPageObject<TNewPage>, new();

        IPageObjectElement<TPage> Click<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new();

        IPageObjectElement<TPage> Hover<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new();

        IPageObjectElement<TPage> InputText<TPage>(IPageObjectElement<TPage> element,
                                                   string text)
            where TPage : IPageObject<TPage>, new();

        IPageObjectElement<TPage> ClearValue<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new();

        void MaximizeBrowserWindow();

        IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
            <TExpectedSourcePage, TExpectedResultantPage>(
            IPageObjectElement<TExpectedSourcePage> element)
            where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
            where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new();

        TPage SelectFromDropDown<TPage>(IPageObjectElement<TPage> element, string value)
            where TPage : IPageObject<TPage>, new();

        TPage SelectFromDropDown<TPage>(IPageObjectElement<TPage> element, int index)
            where TPage : IPageObject<TPage>, new();
    }
}