using OpenQA.Selenium;

namespace Tests.Common.PageObject
{
    public interface ITestContext<out TExpectedPage> where TExpectedPage : PageObject<TExpectedPage>, new()
    {
        IWebDriver Driver { get; }

        TExpectedPage ExpectedPage { get; }

        string UriActualAbsolute { get; }
        
        string UriRoot { get; }

        IBrowserActionPerformer BrowserActionPerformer { get; }
        
        IDomChecker DomChecker { get; }

        TPageObject NavigateTo<TPageObject>() where TPageObject : PageObject<TPageObject>, new();

        TDestinationPage ExpectedPageIs<TDestinationPage>()
            where TDestinationPage : PageObject<TDestinationPage>, new();

        SeleniumTestContext<TDestinationPage> SetExpectedPage<TDestinationPage>()
            where TDestinationPage : PageObject<TDestinationPage>, new();
    }
}