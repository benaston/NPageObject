using System.Collections.Generic;

namespace NPageObject.x.NPageObject
{
    /// <summary>
    /// Optional parent element meant to enable use of a hierarchy of PageObjectElementSelectables for DRY construction of selectors.
    /// </summary>
    public class ElementOn<TParentPage> : IElementOn<TParentPage>
        where TParentPage : PageObject<TParentPage>, new()
    {
        public ElementOn(ITestContext<TParentPage> context,
                                 string selector = "*",
                                 string text = "",
                                 IPageObjectElementSimple<TParentPage> parentElement = null)
        {
            DirectSelector = selector;
            Text = text;
            Context = context;
            ParentElement = parentElement;
        }

        /// <summary>
        /// Use the selectors array if your element has multiple possible selectors, for example if different renderings for MVT purposes are used.
        /// </summary>
        public ElementOn(ITestContext<TParentPage> context,
                                 string[] selectors,
                                 string text = "",
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

        public ITestContext<TParentPage> Context { get; private set; }
    }
}