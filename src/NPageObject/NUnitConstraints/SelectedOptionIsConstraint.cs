namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    public class SelectedOptionIsConstraint<TPage> : UITestConstraintBase<TPage>
        where TPage : IPageObject<TPage>, new()
    {
        private readonly string _value;

        public SelectedOptionIsConstraint(string value)
        {
            _value = value;
        }

        protected IPageObjectElement<TPage> Element { get; set; }

        public override bool Matches(object element)
        {
            Element = (IPageObjectElement<TPage>) element;
            return Element.Context.GetDropDownListSelectedItemText(Element) == _value;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("element with selector \"" + Element.SelectorFullyQualified + "\" to contain the text " + "\"" +
                         _value + "\"");
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.Write("something else (\"" + Element.Context.GetText(Element) + "\").");
        }
    }
}