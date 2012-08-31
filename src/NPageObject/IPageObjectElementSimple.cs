namespace NPageObject
{
    public interface IPageObjectElementSimple<TDriver, out TExpectedCurrentPage>
        where TExpectedCurrentPage : PageObject<TDriver, TExpectedCurrentPage>, new()
    {
        TExpectedCurrentPage ExpectedPage { get; }

        /// <summary>
        /// Selector string constructed using the 
        /// PageObjectElementSelectable hierarchy of objects (DRY-er).
        /// </summary>
        string SelectorFullyQualified { get; }

        string[] SelectorsFullyQualified { get; }

        ITestContext<TDriver, TExpectedCurrentPage> Context { get; }
    }
}