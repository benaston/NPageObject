namespace NPageObject.NUnitConstraints
{
    using NHelpfulException.FrameworkExceptions;
    using NSure;
    using NUnit.Framework.Constraints;

    public class ActualMatchesExpectedLocationConstraint<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        public override bool Matches(object pageObject)
        {
            Ensure.That<ArgumentNullException>(pageObject != null, "context not supplied.");

// ReSharper disable PossibleNullReferenceException
            UITestContext = ((IPageObject<TPage>) pageObject).Context;
// ReSharper restore PossibleNullReferenceException
            var page = new TPage {Context = UITestContext,};

            return UriExpectationHelper.DoesActualMatchExpectedUri(page, UITestContext) &&
                   UITestContext.IsTextVisibleStrict(page.IdentifyingText);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("page of type " + typeof (TPage).Name);
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write(UITestContext.UriActualAbsolute + ". Check your page object model.");
        }
    }
}