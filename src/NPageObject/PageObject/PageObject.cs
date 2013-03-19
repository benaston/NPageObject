namespace NPageObject.PageObject
{
    public abstract class PageObject<TPageObject> where TPageObject : PageObject<TPageObject>, new()
    {
        //enables overriding for pages outside of the "normal" URI scheme
        public virtual string UriRoot
        {
            get { return Context.UriRoot; }
        }

        public ITestContext<TPageObject> Context { get; set; }

        public abstract UriExpectation UriExpectation { get; }

        public abstract string IdentifyingText { get; }

        public string Uri { get { return "/"; } }

        public TDestinationPageObject NavigateTo<TDestinationPageObject>() where TDestinationPageObject : PageObject<TDestinationPageObject>, new()
        {
            return Context.NavigateTo<TDestinationPageObject>();
        }
    }
}