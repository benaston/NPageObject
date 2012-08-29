namespace NPageObject.x.NPageObject
{
    public interface IBrowserActionPerformer
    {
        TNewPage NavigateTo<TNewPage>(string uriContentsRelativeToRoot)
            where TNewPage : PageObject<TNewPage>, IHasMutableUrl, new();

        TNewPage NavigateTo<TNewPage>()
            where TNewPage : PageObject<TNewPage>, new();

        IElementOn<TPage> Click<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new();

        IElementOn<TPage> InputText<TPage>(IElementOn<TPage> element,
                                                   string text)
            where TPage : PageObject<TPage>, new();
    }
}