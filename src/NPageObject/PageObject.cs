namespace NPageObject
{
    public abstract class PageObject<TDriver, TPage> where TPage : PageObject<TDriver, TPage>, new()
    {
        //enables overriding for pages outside of the "normal" URI scheme
        public virtual string UriRoot { get { return Context.UriRoot; } }

        public ITestContext<TDriver, TPage> Context { get; set; }

        public abstract UriExpectation UriExpectation { get; }

        public abstract string IdentifyingText { get; }

        public TDestinationPage NavigateTo<TDestinationPage>() where TDestinationPage : PageObject<TDriver, TDestinationPage>, new()
        {
            return Context.NavigateTo<TDestinationPage>();
        }
    }
}