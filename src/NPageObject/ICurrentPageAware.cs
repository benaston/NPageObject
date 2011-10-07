namespace NPageObject
{
    /// <summary>
    ///   <![CDATA[
    ///   Defines iface used to identify types that can 
    ///   identify the current URI. Used to circumvent 
    ///   limitations surrounding generic specifications 
    ///   for UITestContexts<T>.
    ///   ]]>
    /// </summary>
    public interface ICurrentPageAware
    {
        /// <example>
        ///   http://www.local.justgiving.com/some/page.aspx
        /// </example>
        string UriActualAbsolute { get; }
    }
}