namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    public class CheckableElementIsNotCheckedConstraint<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        protected IPageObjectElement<TPage> Element { get; set; }

        public override bool Matches(object element)
        {
            Element = (IPageObjectElement<TPage>) element;
            return
                !(Element.Context.GetAttributeValue(Element, "checked") == "checked" &&
                  Element.Context.GetAttributeValue(Element, "checked") == "true");
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("radio button with selector \"" + Element.SelectorFullyQualified + "\" to be checked");
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write("not checked.");
        }
    }
}