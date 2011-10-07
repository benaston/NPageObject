namespace NPageObject
{
    /// <summary>
    ///   Responsible for modelling an element on a page,
    ///   selectable via the DOM, but without a defined 
    ///   selector of its own. Useful for conceptual element 
    ///   groupings without a direct selector of 
    ///   their own (e.g. a menu that is an li without an id),
    ///   OR for element groupings that we simply do not need 
    ///   to address directly in UI tests.
    /// </summary>
    public class PageObjectElementSimple<TPage> : IPageObjectElementSimple<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        public PageObjectElementSimple(IUITestContext<TPage> context,
                                       IPageObjectElementSimple<TPage> parentElement = null)
        {
            Context = context;
            ParentElement = parentElement;
        }

        public IPageObjectElementSimple<TPage> ParentElement { get; set; }

        public IUITestContext<TPage> Context { get; private set; }

        IUITestContext IHaveUITestContext.Context
        {
            get { return Context; }
        }

        public TPage ExpectedPage
        {
            get { return Context.ExpectedPage; }
        }

        public string SelectorFullyQualified
        {
            get
            {
                return ParentElement == null
                           ? CssSelector.Empty
                           : ParentElement.SelectorFullyQualified + " " + CssSelector.Empty;
            }
        }

        public string[] SelectorsFullyQualified
        {
            get
            {
                return new[]
                           {
                               ParentElement == null
                                   ? CssSelector.Empty
                                   : ParentElement.SelectorFullyQualified + " " + CssSelector.Empty
                           };
            }
        }
    }
}