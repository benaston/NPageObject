namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    /// <summary>
    ///   Per PageDisplaysTextConstraint, but the matching method expects a context.
    /// </summary>
    public class TextIsVisibleConstraintExpectingContext<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        public TextIsVisibleConstraintExpectingContext(string text, StringMatch matchType)
        {
            Text = text;
            MatchType = matchType;
        }

        protected string Text { get; set; }
        public StringMatch MatchType { get; set; }

        public override bool Matches(object context)
        {
            return MatchType == StringMatch.Strict
                       ? ((IUITestContext<TPage>) context).IsTextVisibleStrict(Text)
                       : ((IUITestContext<TPage>) context).IsTextVisible(Text);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("page to contain text \"" + Text + "\"");
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write("not found on the page.");
        }
    }
}