namespace NPageObject
{
    /// <summary>
    ///   Responsible for defining the signature for 
    ///   performing actions upon a PageObject. Used by 
    ///   the PerformAction method on PageObject type.
    /// </summary>
    public delegate TSourcePage PageActionDelegate<TSourcePage>(TSourcePage tSourcePage)
        where TSourcePage : IPageObject<TSourcePage>, new();
}