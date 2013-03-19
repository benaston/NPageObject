namespace NPageObject.PageObject
{
    /// <summary>
    /// Responsible for defining the iface for page 
    /// object elements that can be directly selected 
    /// (and hence have actions performed upon like 
    /// clicking, hovering etc).
    /// </summary>
    public interface IElementOn<out TParentPage> : IElementOnSimple<TParentPage>
        where TParentPage : PageObject<TParentPage>, new()
    {
        string Text { get; }

        /// <summary>
        /// Selector string associated directly with this element (ignoring any hierarchical selectors that might be required to find it in the DOM).
        /// </summary>
        string DirectSelector { get; }
    }
}