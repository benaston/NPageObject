namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    public class TextFieldValueIsNotConstraint<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        private readonly string _value;

        public TextFieldValueIsNotConstraint(string value)
        {
            _value = value;
        }

        protected IPageObjectElement<TPage> Element { get; set; }

        public override bool Matches(object element)
        {
            Element = (IPageObjectElement<TPage>) element;
            return Element.Context.GetAttributeValue(Element, "value") != _value;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("text field with selector \"" + Element.SelectorFullyQualified + "\" to not contain the value " +
                         "\"" + _value + "\"");
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write("present.");
        }
    }
}