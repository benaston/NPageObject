// Copyright 2012, Ben Aston (ben@bj.ma).
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
// along with NPageObject. If not, see <http://www.gnu.org/licenses/>.

namespace NPageObject.NUnitConstraints
{
	using NUnit.Framework.Constraints;

	public class CheckableElementIsCheckedConstraint<TPage> : UITestConstraintBase<TPage>
		where TPage : IPageObject<TPage>, new()
	{
		protected IPageObjectElement<TPage> Element { get; set; }

		public override bool Matches(object element) {
			Element = (IPageObjectElement<TPage>) element;
			return Element.Context.GetAttributeValue(Element, "checked") == "checked" ||
			       Element.Context.GetAttributeValue(Element, "checked") == "true";
		}

		public override void WriteDescriptionTo(MessageWriter writer) {
			writer.Write("checkable element with selector \"" + Element.SelectorFullyQualified +
			             "\" to be checked");
		}

		public override void WriteActualValueTo(MessageWriter writer) {
			writer.Write("not checked.");
		}
	}
}