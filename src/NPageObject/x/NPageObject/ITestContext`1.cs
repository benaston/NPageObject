namespace NPageObject.x.NPageObject
{
    public interface ITestContext<out TExpectedPage> : ITestContext where TExpectedPage : PageObject<TExpectedPage>, new()
    {
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