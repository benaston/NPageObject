namespace NPageObject
{
    using System;

    //Add ability to specify pause before using the page
    //incase things like facebook widgets are loaded into 
    //the dom which we want to work with
    public abstract class PageObject<TPage> : IPageObject<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        public virtual string UriRoot
        {
            get { return Context.UriRoot; }
        }

        public abstract UriExpectation UriExpectation { get; }

        public IUITestContext<TPage> Context { get; set; }

        public string Source
        {
            get { return Context.PageSource; }
        }

        public abstract string IdentifyingText { get; }

        public virtual TPage PerformAction(PageActionDelegate<TPage> action)
        {
            throw new NotImplementedException(
                "Please implement the PerformAction method in your page object if you wish to supply lambdas to be invoked against it.");
        }

        public TExpectedRedirectType RedirectionOccursTo<TExpectedRedirectType>()
            where TExpectedRedirectType : IPageObject<TExpectedRedirectType>, new()
        {
            return Context.SetExpectedCurrentPage<TExpectedRedirectType>().ExpectedPage;
        }
    }
}