namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    public class ElementIsNotVisibleConstraint<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        protected IPageObjectElement<TPage> Element { get; set; }

        public override bool Matches(object element)
        {
            Element = (IPageObjectElement<TPage>) element;
            return !Element.Context.IsVisible(Element);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("element \"" + Element.SelectorFullyQualified + "\" to not be visible");
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write("visible.");
        }
    }
}