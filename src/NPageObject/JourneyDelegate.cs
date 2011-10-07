namespace NPageObject
{
    public delegate IUITestContext<TDestinationPage> JourneyDelegate<TSourcePage, TDestinationPage>(
        IUITestContext<TSourcePage> startContext, dynamic dto = default(object))
        where TSourcePage : IPageObject<TSourcePage>, new()
        where TDestinationPage : IPageObject<TDestinationPage>, new();
}