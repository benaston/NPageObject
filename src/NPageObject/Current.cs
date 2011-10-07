namespace NPageObject
{
    using NUnitConstraints;

    /// <summary>
    ///   Type to hang fluent NUnit constraints off.
    ///   See also <see cref = "CurrentPage" />.
    /// </summary>
    public static class Current
    {
        public static ActualMatchesExpectedLocationConstraint<TPage> LocationIs<TPage>()
            where TPage : IPageObject<TPage>, new()
        {
            return new ActualMatchesExpectedLocationConstraint<TPage>();
        }

        public static TextIsVisibleConstraintExpectingPageObject<TPage> TextIsVisible<TPage>(string text)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextIsVisibleConstraintExpectingPageObject<TPage>(text, StringMatch.Lenient);
        }

        public static TextIsVisibleConstraintExpectingPageObject<TPage> TextIsVisibleLenient<TPage>(string text)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextIsVisibleConstraintExpectingPageObject<TPage>(text, StringMatch.Lenient);
        }
    }
}