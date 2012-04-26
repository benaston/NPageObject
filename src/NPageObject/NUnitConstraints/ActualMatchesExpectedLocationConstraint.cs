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
	using NHelpfulException.FrameworkExceptions;
	using NSure;
	using NUnit.Framework.Constraints;

	public class ActualMatchesExpectedLocationConstraint<TPage> : UITestConstraintBase<TPage>
		where TPage : IPageObject<TPage>, new()
	{
		public override bool Matches(object pageObject) {
			Ensure.That<ArgumentNullException>(pageObject != null, "context not supplied.");

// ReSharper disable PossibleNullReferenceException
			UITestContext = ((IPageObject<TPage>) pageObject).Context;
// ReSharper restore PossibleNullReferenceException
			var page = new TPage {Context = UITestContext,};

			return UriExpectationHelper.DoesActualMatchExpectedUri(page, UITestContext) &&
			       UITestContext.IsTextVisibleStrict(page.IdentifyingText);
		}

		public override void WriteDescriptionTo(MessageWriter writer) {
			writer.Write("page of type " + typeof (TPage).Name);
		}

		public override void WriteActualValueTo(MessageWriter writer) {
			writer.Write(UITestContext.UriActualAbsolute + ". Check your page object model.");
		}
	}
}