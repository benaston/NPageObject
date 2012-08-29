namespace NPageObject.x.NPageObject
{
    public class SeleniumTestContext<TExpectedPage> : ITestContext<TExpectedPage> where TExpectedPage : PageObject<TExpectedPage>, new()
    {
        private readonly IWebDriver _driver;
        private string _uriRoot;
        private readonly IBrowserActionPerformer _browserActionPerformer;
        private readonly IDomChecker _domChecker;

        public SeleniumTestContext(IWebDriver driver, IBrowserActionPerformer browserActionPerformer, IDomChecker domChecker, string uriRoot)
        {
            _driver = driver;
            _uriRoot = uriRoot;
            _browserActionPerformer = browserActionPerformer;
            _domChecker = domChecker;
        }

        public string UriActualAbsolute
        {
            get { return _driver.Url; }
        }

        /// <summary>
        /// Corresponds to the left-part of typical URIs used in the system.
        /// </summary>
        public string UriRoot
        {
            get { return _uriRoot; }
            private set { _uriRoot = value.TrimEnd('/') + "/"; }
        }

        public TExpectedPage ExpectedPage { get; private set; }

        public IWebDriver Driver
        {
            get { return _driver; }
        }

        public IBrowserActionPerformer BrowserActionPerformer
        {
            get { return _browserActionPerformer; }
        }
        
        public IDomChecker DomChecker
        {
            get { return _domChecker; }
        }

        public TDestinationPage ExpectedPageIs<TDestinationPage>()
            where TDestinationPage : PageObject<TDestinationPage>, new()
        {
            return new TDestinationPage
            {
                Context = new SeleniumTestContext<TDestinationPage>(_driver,
                                                                    _browserActionPerformer,
                                                                    _domChecker,
                                                                    UriRoot)
            };
        }

        public SeleniumTestContext<TDestinationPage> SetExpectedPage<TDestinationPage>()
            where TDestinationPage : PageObject<TDestinationPage>, new()
        {
            return
                new SeleniumTestContext<TDestinationPage>(_driver,
                                                          _browserActionPerformer,
                                                          _domChecker,
                                                          UriRoot);

        }

        public TPageObject NavigateTo<TPageObject>() where TPageObject : PageObject<TPageObject>, new()
        {
            return _browserActionPerformer.NavigateTo<TPageObject>();
        }
    }
}