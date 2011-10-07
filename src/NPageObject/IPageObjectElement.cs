namespace NPageObject
{
    /// <summary>
    ///   Responsible for defining the iface for 
    ///   page object elements that can be directly 
    ///   selected (and hence have actions performed 
    ///   upon like clicking, hovering etc).
    /// </summary>
    public interface IPageObjectElement<TParentPage> : IPageObjectElementSimple<TParentPage>
        where TParentPage : IPageObject<TParentPage>, new()
    {
        string Text { get; }

        /// <summary>
        ///   Selector string associated directly with 
        ///   this element (ignoring any hierarchical 
        ///   selectors that might be required to find 
        ///   it in the DOM).
        /// </summary>
        string DirectSelector { get; }

        bool IsVisible();

        TDestinationPage PressEnterWithNavigation<TDestinationPage>()
            where TDestinationPage : IPageObject<TDestinationPage>, new();

        TDestinationPage ClickWithNavigation<TDestinationPage>()
            where TDestinationPage : IPageObject<TDestinationPage>, new();
    }
}