namespace NPageObject
{
    using NUnitConstraints;

    /// <summary>
    ///   Type to hang fluent NUnit constraints off.
    ///   See also <see cref = "Current" />.
    /// </summary>
    public static class CurrentPage
    {
        public static ActualMatchesExpectedLocationConstraintExpectingContext<TPage> LocationIs<TPage>(
            PageIdentityVerificationType expectedMarkupQuality = PageIdentityVerificationType.Normal)
            where TPage : IPageObject<TPage>, new()
        {
            return new ActualMatchesExpectedLocationConstraintExpectingContext<TPage>(expectedMarkupQuality);
        }

        public static TextIsVisibleConstraintExpectingContext<TPage> TextIsVisible<TPage>(string text)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextIsVisibleConstraintExpectingContext<TPage>(text, StringMatch.Strict);
        }

        public static TextIsVisibleConstraintExpectingContext<TPage> TextIsVisibleLenient<TPage>(string text)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextIsVisibleConstraintExpectingContext<TPage>(text, StringMatch.Lenient);
        }
    }
}