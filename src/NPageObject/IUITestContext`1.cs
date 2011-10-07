// ReSharper disable InconsistentNaming
namespace NPageObject
{
    /// <summary>
    ///   Facade for the UI test driver.
    /// </summary>
    public interface IUITestContext<TPage> : IUITestContext, ICurrentPageAware where TPage : IPageObject<TPage>, new()
    {
        TPage ExpectedPage { get; }
        string PageSource { get; }

        bool TextContains(IPageObjectElement<TPage> element, string text);

        string GetAttributeValue(IPageObjectElement<TPage> element, string attributeName);

        string GetText(IPageObjectElement<TPage> element);

        string GetDropDownListSelectedItemText(IPageObjectElement<TPage> element);

        IPageObjectElement<TPage> Click(IPageObjectElement<TPage> element);

        IPageObjectElement<TPage> Hover(IPageObjectElement<TPage> element);

        IPageObjectElement<TPage> InputText(IPageObjectElement<TPage> element, string text);

        IPageObjectElement<TPage> ClearValue(IPageObjectElement<TPage> element);

        IPageObjectElement<TPage> PressEnter(IPageObjectElement<TPage> element);

        IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation<TExpectedSourcePage, TExpectedResultantPage>
            (IPageObjectElement<TExpectedSourcePage> element)
            where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
            where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new();

        TPage SelectFromDropDown(IPageObjectElement<TPage> element, string value);

        TPage SelectFromDropDown(IPageObjectElement<TPage> element, int index);

        bool IsVisible(IPageObjectElement<TPage> element);

        IUITestContext<TDestinationPage> PerformJourney<TDestinationPage>(
            JourneyDelegate<TPage, TDestinationPage> journey, dynamic dto = default(object))
            where TDestinationPage : IPageObject<TDestinationPage>, new();

        IPageObjectElement<TPage> PressKey(IPageObjectElement<TPage> element, Key key);
    }
}
// ReSharper restore InconsistentNaming