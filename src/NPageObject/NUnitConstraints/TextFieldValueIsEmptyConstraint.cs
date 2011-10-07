namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    public class TextFieldValueIsEmptyConstraint<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        protected IPageObjectElement<TPage> Element { get; set; }

        public override bool Matches(object element)
        {
            Element = (IPageObjectElement<TPage>) element;
            return string.IsNullOrEmpty(Element.Context.GetAttributeValue(Element, "value"));
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("text field with selector \"" + Element.SelectorFullyQualified + "\" to be empty");
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write("not empty.");
        }
    }
}