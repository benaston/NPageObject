namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    public class ElementTextContainsConstraint<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        private readonly string _value;

        public ElementTextContainsConstraint(string value)
        {
            _value = value;
        }

        protected IPageObjectElement<TPage> Element { get; set; }

        public override bool Matches(object element)
        {
            Element = (IPageObjectElement<TPage>) element;
            return !Element.Text.Contains(_value);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("element with selector \"" + Element.SelectorFullyQualified + "\" to contain the text " + "\"" +
                         _value + "\"");
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write(Element.Text);
        }
    }
}