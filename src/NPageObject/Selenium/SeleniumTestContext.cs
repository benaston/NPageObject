using System;
using NPageObject.PageObject;
using OpenQA.Selenium;

namespace NPageObject.Selenium
{
    /// <summary>
    /// The test context wraps up an instance of selenium web driver.
    /// </summary>
    /// <typeparam name="TExpectedPage">The page to start your test on.</typeparam>
    public class SeleniumTestContext<TExpectedPage> : ITestContext<TExpectedPage> where TExpectedPage : PageObject<TExpectedPage>, new()
    {
        private readonly IWebDriver _driver;
        private string _uriRoot;
        private readonly IBrowserActionPerformer _browserActionPerformer;
        private readonly IDomChecker _domChecker;
        private TExpectedPage _expectedPage;

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

        public TExpectedPage ExpectedPage
        {
            get
            {
                if(_expectedPage == null)
                {
                    throw new Exception("Expected page is not set. You must call ExpectedPageIs<TDestinationPage>() on the test context before using this property.");
                }

                return _expectedPage;
            }
            private set { _expectedPage = value; }
        }

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