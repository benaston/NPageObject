// Copyright 2011, Ben Aston (ben@bj.ma).
// 
// This file is part of NPageObject.
// 
// NPageObject is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NPageObject is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NPageObject.  If not, see <http://www.gnu.org/licenses/>.

namespace NPageObject.NUnitConstraints
{
	using NUnit.Framework.Constraints;

	public class TextFieldTextIsConstraint<TPage> : UITestConstraintBase<TPage>
		where TPage : IPageObject<TPage>, new()
	{
		private readonly string _value;

		public TextFieldTextIsConstraint(string value) {
			_value = value;
		}

		protected IPageObjectElement<TPage> Element { get; set; }

		public override bool Matches(object element) {
			Element = (IPageObjectElement<TPage>) element;
			return Element.Context.GetText(Element) == _value;
		}

		public override void WriteDescriptionTo(MessageWriter writer) {
			writer.Write("element with selector \"" + Element.SelectorFullyQualified +
			             "\" to contain the text " + "\"" +
			             _value + "\"");
		}

		public override void WriteActualValueTo(MessageWriter writer) {
			writer.Write("something else (\"" + Element.Context.GetText(Element) + "\").");
		}
	}
}