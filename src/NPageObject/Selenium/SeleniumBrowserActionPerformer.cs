using System;
using NPageObject.Enumerations;
using NPageObject.Extensions;
using NPageObject.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace NPageObject.Selenium
{
    /// <summary>
    /// Wraps up all the browser interaction behavior exposed 
    /// by Selenium web driver e.g. navigation, clicking and hovering.
    /// </summary>
    public class SeleniumBrowserActionPerformer : IBrowserActionPerformer
    {
        public static readonly TimeSpan DefaultDemoSlowDown = TimeSpan.FromSeconds(3);

        private readonly IDomChecker _domChecker;
        private readonly IWebDriver _driver;
        private readonly TimeSpan _timeout;
        private readonly bool _isInDemonstrationMode;
        private readonly string _uriRoot;

        public SeleniumBrowserActionPerformer(IWebDriver driver,
                                              IDomChecker domChecker,
                                              string uriRoot,
                                              TimeSpan elementSelectionTimeout, 
                                              bool isInDemonstrationMode = false)
        {
            _driver = driver;
            _domChecker = domChecker;
            _uriRoot = uriRoot;
            _timeout = elementSelectionTimeout;
            _isInDemonstrationMode = isInDemonstrationMode;
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
        
        public IElementOn<TPage> Clear<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).Clear();

            return element;
        }

        public IElementOn<TPage> PressKey<TPage>(IElementOn<TPage> element, Key key)
            where TPage : PageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).
                SendKeys(
                    key.ToString());

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        public IElementOn<TPage> PressEnter<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element, _timeout).
                SendKeys(
                    Keys.Enter);

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        public IElementOn<TPage> Hover<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            new Actions(_driver).MoveToElement(
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                element,
                                                                                _timeout))
                .Perform();

            return element.AfterPossiblyWaiting(DefaultDemoSlowDown, () => _isInDemonstrationMode);
        }

        /// <summary>
        /// NOTE: This is a total hack because the .NET 
        /// selenium driver does not expose a method to 
        /// resize the browser window. It also does not 
        /// appear to work with the Chrome driver.
        /// </summary>
        public void MaximizeBrowserWindow()
        {
            _driver.FindElement(By.TagName("body")).SendKeys(Keys.F11);
        }

        public TPage SelectFromDropDown<TPage>(IElementOn<TPage> element, string value)
            where TPage : PageObject<TPage>, new()
        {
            var nativeDropDownElement =
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
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

        public IElementOn<TPage> SelectFromDropDown<TPage>(IElementOn<TPage> element, int index)
            where TPage : PageObject<TPage>, new()
        {
            var nativeDropDownElement =
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                element,
                                                                                _timeout);

            for (var x = 0; x < index; x++)
            {
                nativeDropDownElement.SendKeys(Keys.ArrowDown);
            }

            nativeDropDownElement.SendKeys(Keys.Enter);

            return element;
        }
    }
}