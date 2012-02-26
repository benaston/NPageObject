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

	/// <summary>
	/// 	Per PageDisplaysTextConstraint, but the matching method expects a context.
	/// </summary>
	public class TextIsVisibleConstraintExpectingContext<TPage> : UITestConstraintBase<TPage>
		where TPage : IPageObject<TPage>, new()
	{
		public TextIsVisibleConstraintExpectingContext(string text, StringMatch matchType) {
			Text = text;
			MatchType = matchType;
		}

		protected string Text { get; set; }
		public StringMatch MatchType { get; set; }

		public override bool Matches(object context) {
			return MatchType == StringMatch.Strict
			       	? ((IUITestContext<TPage>) context).IsTextVisibleStrict(Text)
			       	: ((IUITestContext<TPage>) context).IsTextVisible(Text);
		}

		public override void WriteDescriptionTo(MessageWriter writer) {
			writer.Write("page to contain text \"" + Text + "\"");
		}

		public override void WriteActualValueTo(MessageWriter writer) {
			writer.Write("not found on the page.");
		}
	}
}