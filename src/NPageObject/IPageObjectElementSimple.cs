namespace NPageObject
{
    public interface IPageObjectElementSimple<TExpectedCurrentPage> : IHaveUITestContext
        where TExpectedCurrentPage : IPageObject<TExpectedCurrentPage>, new()
    {
        new IUITestContext<TExpectedCurrentPage> Context { get; }

        TExpectedCurrentPage ExpectedPage { get; }

        /// <summary>
        ///   Selector string constructed using the 
        ///   PageObjectElementSelectable hierarchy of 
        ///   objects (DRY-er).
        /// </summary>
        string SelectorFullyQualified { get; }

        string[] SelectorsFullyQualified { get; }
    }
}