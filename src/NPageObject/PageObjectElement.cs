namespace NPageObject
{
    using System.Collections.Generic;

    /// <summary>
    ///   Optional parent element meant to enable use of a hierarchy 
    ///   of PageObjectElementSelectables for DRY construction of 
    ///   selectors.
    /// </summary>
    public class PageObjectElement<TParentPage> : IPageObjectElement<TParentPage>
        where TParentPage : IPageObject<TParentPage>, new()
    {
        public PageObjectElement(IUITestContext<TParentPage> context, string selector = "*", string text = "",
                                 IPageObjectElementSimple<TParentPage> parentElement = null)
        {
            DirectSelector = selector;
            Text = text;
            Context = context;
            ParentElement = parentElement;
        }

        /// <summary>
        ///   Use the selectors array if your element has multiple 
        ///   possible selectors, for example if different renderings 
        ///   for MVT purposes are used.
        /// </summary>
        public PageObjectElement(IUITestContext<TParentPage> context, string[] selectors, string text = "",
                                 IPageObjectElementSimple<TParentPage> parentElement = null)
        {
            DirectSelectors = selectors;
            Text = text;
            Context = context;
            ParentElement = parentElement;
        }

        public IPageObjectElementSimple<TParentPage> ParentElement { get; set; }

        public string[] DirectSelectors { get; set; }

        public string DirectSelector { get; set; }

        public bool IsVisible()
        {
            return Context.IsVisible(this);
        }

        public string SelectorFullyQualified
        {
            get
            {
                return ParentElement == null
                           ? DirectSelector
                           : ParentElement.SelectorFullyQualified + " " + DirectSelector;
            }
        }

        public string[] SelectorsFullyQualified
        {
            get
            {
                var selectors = new List<string>();

                DirectSelectors = DirectSelectors ?? new string[0];

                foreach (var s in DirectSelectors)
                {
                    selectors.Add(ParentElement == null
                                      ? s
                                      : ParentElement.SelectorFullyQualified + " " + s);
                }

                return selectors.ToArray();
            }
        }

        public string Text { get; set; }

        public TParentPage ExpectedPage
        {
            get { return Context.ExpectedPage; }
        }

        public TDestinationPage PressEnterWithNavigation<TDestinationPage>()
            where TDestinationPage : IPageObject<TDestinationPage>, new()
        {
            this.PressEnter();
            return new TDestinationPage {Context = Context.SetExpectedCurrentPage<TDestinationPage>()};
        }

        public TDestinationPage ClickWithNavigation<TDestinationPage>()
            where TDestinationPage : IPageObject<TDestinationPage>, new()
        {
            this.Click();
            return new TDestinationPage {Context = Context.SetExpectedCurrentPage<TDestinationPage>()};
        }

        public IUITestContext<TParentPage> Context { get; private set; }

        IUITestContext IHaveUITestContext.Context
        {
            get { return Context; }
        }
    }
}