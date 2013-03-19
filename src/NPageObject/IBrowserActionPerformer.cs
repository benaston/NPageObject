using NPageObject.Enumerations;
using NPageObject.PageObject;

namespace NPageObject
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

        IElementOn<TPage> Clear<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new();

        IElementOn<TPage> PressKey<TPage>(IElementOn<TPage> element, Key key)
            where TPage : PageObject<TPage>, new();

        IElementOn<TPage> PressEnter<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new();

        IElementOn<TPage> Hover<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new();

        /// <summary>
        /// NOTE: This is a total hack because the .NET 
        /// selenium driver does not expose a method to 
        /// resize the browser window. It also does not 
        /// appear to work with the Chrome driver.
        /// </summary>
        void MaximizeBrowserWindow();

        TPage SelectFromDropDown<TPage>(IElementOn<TPage> element, string value)
            where TPage : PageObject<TPage>, new();

        IElementOn<TPage> SelectFromDropDown<TPage>(IElementOn<TPage> element, int index)
            where TPage : PageObject<TPage>, new();
    }
}