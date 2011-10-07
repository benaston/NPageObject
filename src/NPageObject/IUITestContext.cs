// ReSharper disable InconsistentNaming
namespace NPageObject
{
    /// <summary>
    ///   See also parameterized type implementation.
    /// </summary>
    public interface IUITestContext
    {
        /// <example>
        ///   http://www.local.justgiving.com/
        /// </example>
        string UriRoot { get; }

        /// <example>
        ///   /some/page.aspx
        /// </example>
        string UriActualRelative { get; }

        TNewPage BrowseTo<TNewPage>(string uriContentsRelativeToRoot)
            where TNewPage : IPageObject<TNewPage>, IHaveMutableUrl, new();

        TNew BrowseTo<TNew>() where TNew : IPageObject<TNew>, new();

        /// <summary>
        ///   Case sensitive, whitespace in DOM converted to single spacing.
        /// </summary>
        bool IsTextVisibleStrict(string text);

        /// <summary>
        ///   Case insensitive, whitespace in DOM removed (and hence "ignored").
        /// </summary>
        bool IsTextVisible(string text);

        /// <summary>
        ///   Case insensitive, whitespace removed from text to find and the DOM.
        /// </summary>
        bool ContainsLinkWithText(string text);

        bool ContainsLinkWithTextAndHref(string text, string href);

        IUITestContext<T1> SetExpectedCurrentPage<T1>() where T1 : IPageObject<T1>, new();

        void ExecuteScript(string scriptToExecute);
    }
}
// ReSharper restore InconsistentNaming