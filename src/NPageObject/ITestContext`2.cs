namespace NPageObject
{
    public interface ITestContext<TDriver, out TExpectedPage> : ITestContext<TDriver> 
        where TExpectedPage : PageObject<TDriver, TExpectedPage>, new()
    {
        TExpectedPage ExpectedPage { get; }

        string UriActualAbsolute { get; }
        
        string UriRoot { get; }

        IBrowserActionPerformer BrowserActionPerformer { get; }
        
        IDomChecker DomChecker { get; }

        TPageObject NavigateTo<TPageObject>() where TPageObject : PageObject<TDriver, TPageObject>, new();

        TDestinationPage ExpectedPageIs<TDestinationPage>()
            where TDestinationPage : PageObject<TDriver, TDestinationPage>, new();

        ITestContext<TDriver, TDestinationPage> SetExpectedPage<TDestinationPage>()
            where TDestinationPage : PageObject<TDriver, TDestinationPage>, new();
    }
}