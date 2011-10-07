// ReSharper disable InconsistentNaming

namespace NPageObject.Selenium
{
    using System;
    using System.Text.RegularExpressions;
    using OpenQA.Selenium;
    using NSure;
    using ArgumentException = NHelpfulException.FrameworkExceptions.ArgumentException;
    using ArgumentNullException = NHelpfulException.FrameworkExceptions.ArgumentNullException;

    /// <summary>
    ///   Responsible for providing an implementation 
    ///   of the IUITestContext for Selenium. See comment 
    ///   on iface.
    /// </summary>
    public class SeleniumUITestContext<TPage> : IUITestContext<TPage> where TPage : IPageObject<TPage>, new()
    {
        private readonly IBrowserActionPerformer _browserActionPerformer;
        private readonly IDomChecker _domChecker;
        private string _uriRoot;

        public SeleniumUITestContext(IWebDriver webDriver,
                                     IBrowserActionPerformer browserActionPerformer,
                                     IDomChecker domChecker,
                                     string uriRoot)
        {
            Ensure.That<ArgumentNullException>(webDriver != null, "webDriver not supplied.")
                .And<ArgumentNullException>(browserActionPerformer != null, "browserActionPerformer not supplied.")
                .And<ArgumentNullException>(domChecker != null, "domChecker not supplied.")
                .And<ArgumentException>(string.IsNullOrWhiteSpace(uriRoot), "baseUrl not supplied.")
                .And<ArgumentException>(Regex.IsMatch(uriRoot, "^https?://"), "baseUrl does not match URI regex.");

            Driver = webDriver;
            _browserActionPerformer = browserActionPerformer;
            _domChecker = domChecker;
            UriRoot = uriRoot;
        }

        public IWebDriver Driver { get; private set; }

        public string PageSource
        {
            get { return Driver.PageSource; }
        }

        public string UriRoot
        {
            get { return _uriRoot; }
            private set { _uriRoot = value.TrimEnd('/') + "/"; }
        }

        public TPage ExpectedPage
        {
            get { return new TPage {Context = this,}; }
        }

        public string UriActualAbsolute
        {
            get { return Driver.Url; }
        }

        public string UriActualRelative
        {
            get { return Driver.Url.Replace(UriRoot, "/"); }
        }

        public IUITestContext<TDestinationPage> SetExpectedCurrentPage<TDestinationPage>()
            where TDestinationPage : IPageObject<TDestinationPage>, new()
        {
            return new SeleniumUITestContext<TDestinationPage>(Driver, _browserActionPerformer, _domChecker, UriRoot);
        }

        public IUITestContext<TDestinationPage> PerformJourney<TDestinationPage>(
            JourneyDelegate<TPage, TDestinationPage> journey, dynamic dto)
            where TDestinationPage : IPageObject<TDestinationPage>, new()
        {
            return journey(this, dto);
        }

        /// <summary>
        ///   Executes JavaScript in the context of the 
        ///   selected frame or window. Do not abuse as 
        ///   you could seriously invalidate your test.
        /// </summary>
        /// <param name = "scriptToExecute">The JavaScript to execute. Script tags are not required.</param>
        public void ExecuteScript(string scriptToExecute)
        {
            var javaScriptExecutor = Driver as IJavaScriptExecutor;
            if (javaScriptExecutor == null)
            {
                throw new NotSupportedException("ExecuteScript not supported on current webdriver");
            }

            javaScriptExecutor.ExecuteScript(scriptToExecute);
        }

        public bool TextContains(IPageObjectElement<TPage> element, string text)
        {
            return _domChecker.TextContains(element, text);
        }

        public string GetAttributeValue(IPageObjectElement<TPage> element, string attributeName)
        {
            return _domChecker.GetAttributeValue(element, attributeName);
        }

        public string GetText(IPageObjectElement<TPage> element)
        {
            return _domChecker.GetText(element);
        }

        public string GetDropDownListSelectedItemText(IPageObjectElement<TPage> element)
        {
            return _domChecker.GetDropDownListSelectedItemText(element);
        }

        public bool IsVisible(IPageObjectElement<TPage> element)
        {
            return _domChecker.IsVisible(element);
        }

        public bool IsTextVisibleStrict(string text)
        {
            return _domChecker.IsTextVisibleStrict<TPage>(text);
        }

        public bool IsTextVisible(string text)
        {
            return _domChecker.IsTextVisible<TPage>(text);
        }

        public bool ContainsLinkWithText(string text)
        {
            return _domChecker.ContainsLinkWithText<TPage>(text);
        }

        public bool ContainsLinkWithTextAndHref(string text, string href)
        {
            return _domChecker.ContainsLinkWithTextAndHref<TPage>(text, href);
        }

        public TNewPage BrowseTo<TNewPage>(string uriContentsRelativeToRoot)
            where TNewPage : IPageObject<TNewPage>, IHaveMutableUrl, new()
        {
            return _browserActionPerformer.BrowseTo<TNewPage>(uriContentsRelativeToRoot);
        }

        public TNewPage BrowseTo<TNewPage>() where TNewPage : IPageObject<TNewPage>, new()
        {
            return _browserActionPerformer.BrowseTo<TNewPage>();
        }

        public IPageObjectElement<TPage> Click(IPageObjectElement<TPage> element)
        {
            return _browserActionPerformer.Click(element);
        }

        public IPageObjectElement<TPage> Hover(IPageObjectElement<TPage> element)
        {
            return _browserActionPerformer.Hover(element);
        }

        public IPageObjectElement<TPage> InputText(IPageObjectElement<TPage> element,
                                                   string text)
        {
            return _browserActionPerformer.InputText(element, text);
        }

        public IPageObjectElement<TPage> ClearValue(IPageObjectElement<TPage> element)
        {
            return _browserActionPerformer.ClearValue(element);
        }

        public IPageObjectElement<TPage> PressEnter(IPageObjectElement<TPage> element)
        {
            return _browserActionPerformer.PressEnter(element);
        }

        public IPageObjectElement<TPage> PressKey(IPageObjectElement<TPage> element, Key key)
        {
            return _browserActionPerformer.PressKey(element, key);
        }

        public TPage SelectFromDropDown(IPageObjectElement<TPage> element, string value)
        {
            return _browserActionPerformer.SelectFromDropDown(element, value);
        }

        public TPage SelectFromDropDown(IPageObjectElement<TPage> element, int index)
        {
            return _browserActionPerformer.SelectFromDropDown(element, index);
        }

        public IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
            <TExpectedSourcePage, TExpectedResultantPage>(
            IPageObjectElement<TExpectedSourcePage> element)
            where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
            where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new()
        {
            return
                _browserActionPerformer.PressEnterWithPageNavigation<TExpectedSourcePage, TExpectedResultantPage>(
                    element);
        }
    }
}

// ReSharper restore InconsistentNaming