using System;
using System.Linq;
using Bjma.Utility.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

// ReSharper disable InconsistentNaming

namespace NPageObject
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ReUseUITestDriverAcrossTests : Attribute {}

    /// <summary>
    ///   Responsible for providing a base type for UITest types, giving
    ///   them access to the UI test framework wrapper and associated 
    ///   functionality (primarily the fluent UI-testing API).
    /// </summary>
    [Category("UI")]
    public abstract class UITestBase<T> where T : IPageObject<T>, new()
    {
// ReSharper disable StaticFieldInGenericType
        public static readonly int DefaultElementSelectionTimeoutSeconds = 30;
// ReSharper restore StaticFieldInGenericType
        private IWebDriver _driver;

        protected IUITestContext<T> UITestContext { get; private set; }        

        public string UriRoot
        {
            get { return "uriRoot".AppSetting(); }
        }

        public string WebDriver
        {
            get { return "webDriver".AppSetting(); }
        }

        public UITestDriverLifetime CurrentUITestDriverLifetime
        {
            get
            {
                return GetType().GetCustomAttributes(typeof (ReUseUITestDriverAcrossTests), true).Any()
                           ? UITestDriverLifetime.TestFixture
                           : "uiTestDriverLifetime".AppSetting().TryParseOrDefault(UITestDriverLifetime.Test);
            }
        }

        public TimeSpan ElementSelectionTimeout
        {
            get
            {
                return
                    "elementSelectionTimeoutSeconds".AppSetting().TryParseOrDefault(
                        DefaultElementSelectionTimeoutSeconds).Seconds();
            }
        }

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            if (CurrentUITestDriverLifetime == UITestDriverLifetime.TestFixture)
            {
                _driver = CreateWebDriver();
            }
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            if (CurrentUITestDriverLifetime == UITestDriverLifetime.TestFixture)
            {
                _driver.Dispose();
            }
        }

        [SetUp]
        public void SetUp()
        {
            if (CurrentUITestDriverLifetime == UITestDriverLifetime.Test)
            {
                _driver = CreateWebDriver();
            }
            var isInDemonstrationMode = "isInDemonstrationMode".AppSetting().TryParseOrDefault(false);
            var domChecker = new SeleniumDomChecker(_driver, ElementSelectionTimeout);
            UITestContext = new SeleniumUITestContext<T>(_driver,
                                                         new SeleniumBrowserActionPerformer(_driver, domChecker,
                                                                                            isInDemonstrationMode,
                                                                                            UriRoot,
                                                                                            ElementSelectionTimeout),
                                                         domChecker, UriRoot);            
        }

        [TearDown]
        public void TearDown()
        {            
            if (CurrentUITestDriverLifetime == UITestDriverLifetime.Test)
            {
                _driver.Dispose();
            }
        }

        public TPageType BrowseTo<TPageType>(string uriContentsRelativeToRoot)
            where TPageType : IPageObject<TPageType>, IHaveMutableUrl, new()
        {
            return UITestContext.BrowseTo<TPageType>(uriContentsRelativeToRoot);
        }

        public TPageType BrowseTo<TPageType>() where TPageType : IPageObject<TPageType>, new()
        {
            return UITestContext.BrowseTo<TPageType>();
        }

        public T LoadDefaultPage()
        {
            return UITestContext.BrowseTo<T>();
        }

        public T ExpectedPage()
        {
            return UITestContext.ExpectedPage;
        }

        public TPageType ExpectedPage<TPageType>() where TPageType : IPageObject<TPageType>, new()
        {
            return UITestContext.SetExpectedCurrentPage<TPageType>().ExpectedPage;
        }

        public IWebDriver CreateWebDriver()
        {
            var webDriverType = Type.GetType(WebDriver + ", WebDriver");

            return (IWebDriver)Activator.CreateInstance(webDriverType ?? typeof (ChromeDriver));
        }
    }
}

// ReSharper restore InconsistentNaming