namespace NPageObject.NUnitConstraints
{
    using System;
    using NSure;
    using NUnit.Framework.Constraints;
    using ArgumentNullException = NHelpfulException.FrameworkExceptions.ArgumentNullException;

    /// <summary>
    ///   If the expected markup quality is low then 
    ///   the identifying text check is bypassed
    ///   to avoid false errors.
    /// </summary>
    public class ActualMatchesExpectedLocationConstraintExpectingContext<T> : UITestConstraintBase<T>
        where T : IPageObject<T>, new()
    {
        private readonly PageIdentityVerificationType _expectedMarkupQuality;

        public ActualMatchesExpectedLocationConstraintExpectingContext(
            PageIdentityVerificationType expectedMarkupQuality = PageIdentityVerificationType.Normal)
        {
            _expectedMarkupQuality = expectedMarkupQuality;
        }

        public override bool Matches(object context)
        {
            Ensure.That<ArgumentNullException>(context != null, "context not supplied.");

            try
            {
                UITestContext = ((IUITestContext<T>) context);
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(
                    "Check your page object model parameterized types, uses of Click/ClickWithNavigation, use of Current/CurrentPage.",
                    e);
            }

            var page = new T {Context = UITestContext,};

            return UriExpectationHelper.DoesActualMatchExpectedUri(page, UITestContext) &&
                   (_expectedMarkupQuality == PageIdentityVerificationType.Normal
                        ? UITestContext.IsTextVisible(page.IdentifyingText)
                        : true);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("page of type " + typeof (T).Name);
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write(UITestContext.UriActualAbsolute);
        }
    }
}