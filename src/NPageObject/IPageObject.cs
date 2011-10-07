namespace NPageObject
{
    /// <summary>
    ///   Responsible for defining the interface 
    ///   for all page objects to implement.
    /// </summary>
    /// <typeparam name = "TPage"></typeparam>
    public interface IPageObject<TPage> where TPage : IPageObject<TPage>, new()
    {
        /// <summary>
        ///   Include trailing slash.
        /// </summary>
        string UriRoot { get; }

        /// <summary>
        ///   Do not include first slash.
        /// </summary>
        UriExpectation UriExpectation { get; }

        IUITestContext<TPage> Context { get; set; }

        /// <summary>
        ///   A piece of text on the page that can 
        ///   be used to identify the page. e.g. the title.
        /// </summary>
        string IdentifyingText { get; }

        string Source { get; }

        TPage PerformAction(PageActionDelegate<TPage> action);
    }
}