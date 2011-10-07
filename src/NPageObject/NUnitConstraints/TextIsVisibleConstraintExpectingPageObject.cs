namespace NPageObject
{
    using NUnit.Framework.Constraints;
    using NUnitConstraints;

    public class TextIsVisibleConstraintExpectingPageObject<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        public TextIsVisibleConstraintExpectingPageObject(string text, StringMatch matchType)
        {
            Text = text;
            MatchType = matchType;
        }

        public StringMatch MatchType { get; set; }
        protected string Text { get; set; }

        public override bool Matches(object pageObject)
        {
            return MatchType == StringMatch.Strict
                       ? (((IPageObject<TPage>) pageObject).Context).IsTextVisibleStrict(Text)
                       : (((IPageObject<TPage>) pageObject).Context).IsTextVisible(Text);
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