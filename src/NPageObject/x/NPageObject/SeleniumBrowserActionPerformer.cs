using System;

namespace NPageObject.x.NPageObject
{
    public class SeleniumBrowserActionPerformer : IBrowserActionPerformer
    {
        public static readonly TimeSpan DefaultDemoSlowDown = TimeSpan.FromSeconds(3);

        private readonly IDomChecker _domChecker;
        private readonly IWebDriver _driver;
        private readonly TimeSpan _timeout;
        private readonly string _uriRoot;

        public SeleniumBrowserActionPerformer(IWebDriver driver,
                                              IDomChecker domChecker,
                                              string uriRoot,
                                              TimeSpan elementSelectionTimeout)
        {
            _driver = driver;
            _domChecker = domChecker;
            _uriRoot = uriRoot;
            _timeout = elementSelectionTimeout;
        }

        public TNewPage NavigateTo<TNewPage>(string uriContentsRelativeToRoot)
            where TNewPage : PageObject<TNewPage>, IHasMutableUrl, new()
        {
            var uri = _uriRoot + uriContentsRelativeToRoot;
            _driver.Navigate().GoToUrl(uri);

            var result = new TNewPage { Context = new SeleniumTestContext<TNewPage>(_driver, this, _domChecker, _uriRoot) };
            ((IHasMutableUrl)result).UriExpectation = new UriExpectation
            {
                UriMatch = UriMatch.Exact,
                UriContentsRelativeToRoot = uriContentsRelativeToRoot
            };

            return result;
        }

        public TNewPage NavigateTo<TNewPage>()
            where TNewPage : PageObject<TNewPage>, new()
        {
            var uri = SeleniumUITestContextHelpers.GetAbsoluteUriFromPath<TNewPage>(_driver,
                                                                                    _uriRoot,
                                                                                    _domChecker,
                                                                                    this);
            _driver.Navigate().GoToUrl(uri);

            return new TNewPage { Context = new SeleniumTestContext<TNewPage>(_driver, this, _domChecker, _uriRoot) };
        }

        public IElementOn<TPage> Click<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).
                Click();

            return element;
        }

        public IElementOn<TPage> InputText<TPage>(IElementOn<TPage> element,
                                                          string text)
            where TPage : PageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).
                SendKeys(text);

            return element;
        }
    }
}