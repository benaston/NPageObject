namespace NPageObject.Selenium
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    /// <summary>
    ///   Encpsulates functionality to perform browser actions
    ///   using Selenium.
    /// </summary>
    public class SeleniumBrowserActionPerformer : IBrowserActionPerformer
    {
        public static readonly TimeSpan DefaultDemoSlowDown = TimeSpan.FromSeconds(3);

        private readonly IDomChecker _domChecker;
        private readonly IWebDriver _driver;
        private readonly bool _isInDemonstrationMode;
        private readonly TimeSpan _timeout;
        private readonly string _uriRoot;

        public SeleniumBrowserActionPerformer(IWebDriver driver, IDomChecker domChecker, bool isInDemonstrationMode,
                                              string uriRoot, TimeSpan elementSelectionTimeout)
        {
            _driver = driver;
            _domChecker = domChecker;
            _isInDemonstrationMode = isInDemonstrationMode;
            _uriRoot = uriRoot;
            _timeout = elementSelectionTimeout;
        }

        public IPageObjectElement<TPage> PressKey<TPage>(IPageObjectElement<TPage> element, Key key)
            where TPage : IPageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).SendKeys(
                key.ToString());

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        public IPageObjectElement<TPage> PressEnter<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).SendKeys(
                Keys.Enter);

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        public TNewPage BrowseTo<TNewPage>(string uriContentsRelativeToRoot)
            where TNewPage : IPageObject<TNewPage>, IHaveMutableUrl, new()
        {
            var uri = _uriRoot + uriContentsRelativeToRoot;
            _driver.Navigate().GoToUrl(uri);
            MaximizeBrowserWindow();

            var result = new TNewPage
                             {Context = new SeleniumUITestContext<TNewPage>(_driver, this, _domChecker, _uriRoot)};
            ((IHaveMutableUrl)result).UriExpectation = new UriExpectation
                                                           {
                                                               Match = UriMatch.Exact,
                                                               UriContentsRelativeToRoot = uriContentsRelativeToRoot
                                                           };

            return result;
        }

        public TNewPage BrowseTo<TNewPage>()
            where TNewPage : IPageObject<TNewPage>, new()
        {
            var uri = SeleniumUITestContextHelpers.GetAbsoluteUriFromPath<TNewPage>(_driver, _uriRoot, _domChecker, this);
            _driver.Navigate().GoToUrl(uri);
            MaximizeBrowserWindow();

            return new TNewPage {Context = new SeleniumUITestContext<TNewPage>(_driver, this, _domChecker, _uriRoot)};
        }

        public IPageObjectElement<TPage> Click<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).Click();

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        public IPageObjectElement<TPage> Hover<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new()
        {
            new Actions(_driver).MoveToElement(SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                                               element,
                                                                                                               _timeout,
                                                                                                               DesiredSelectionSpeed
                                                                                                                   .Fast))
                .Perform();

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        public IPageObjectElement<TPage> InputText<TPage>(IPageObjectElement<TPage> element,
                                                          string text)
            where TPage : IPageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).SendKeys(text);

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        public IPageObjectElement<TPage> ClearValue<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new()
        {
            var nativeElement = SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element,
                                                                                                _timeout);
            nativeElement.Click();
            nativeElement.Clear();

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        /// <summary>
        ///   NOTE: BA; this is a total hack because the .NET selenium 
        ///   driver does not expose a method to resize the browser window.
        ///   It also does not appear to work with the Chrome driver.
        /// </summary>
        public void MaximizeBrowserWindow()
        {
            _driver.FindElement(By.TagName("body")).SendKeys(Keys.F11);
        }

        public IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
            <TExpectedSourcePage, TExpectedResultantPage>(
            IPageObjectElement<TExpectedSourcePage> element)
            where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
            where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).SendKeys(
                Keys.Enter);

            return new SeleniumUITestContext<TExpectedResultantPage>(_driver, this, _domChecker, _uriRoot);
        }

        public TPage SelectFromDropDown<TPage>(IPageObjectElement<TPage> element, string value)
            where TPage : IPageObject<TPage>, new()
        {
            var nativeDropDownElement = SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                                        element,
                                                                                                        _timeout);
            var options = nativeDropDownElement.FindElements(By.TagName("option"));

            foreach (var option in options)
            {
                if (option.Text == value)
                {
                    break;
                }
                nativeDropDownElement.SendKeys(Keys.ArrowDown);
            }

            nativeDropDownElement.SendKeys(Keys.Enter);

            return element.ExpectedPage;
        }

        //NOTE: BA; no idea if this actually works, supplied to fix build
        public TPage SelectFromDropDown<TPage>(IPageObjectElement<TPage> element, int index)
            where TPage : IPageObject<TPage>, new()
        {
            var nativeDropDownElement = SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                                        element,
                                                                                                        _timeout);

            for (var x = 0; x < index; x++)
            {
                nativeDropDownElement.SendKeys(Keys.ArrowDown);
            }

            nativeDropDownElement.SendKeys(Keys.Enter);

            return element.ExpectedPage;
        }
    }
}