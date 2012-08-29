namespace NPageObject.x.NPageObject
{
    public interface IPageObjectElementSimple<out TExpectedCurrentPage> //: IHasTestContext
        where TExpectedCurrentPage : PageObject<TExpectedCurrentPage>, new()
    {
        TExpectedCurrentPage ExpectedPage { get; }

        /// <summary>
        /// Selector string constructed using the PageObjectElementSelectable hierarchy of objects (DRY-er).
        /// </summary>
        string SelectorFullyQualified { get; }

        string[] SelectorsFullyQualified { get; }

        ITestContext<TExpectedCurrentPage> Context { get; }
    }
}