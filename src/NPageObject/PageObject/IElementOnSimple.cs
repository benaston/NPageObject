namespace NPageObject.PageObject
{
    public interface IElementOnSimple<out TExpectedCurrentPage>
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