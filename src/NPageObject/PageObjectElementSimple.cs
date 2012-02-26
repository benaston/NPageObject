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

namespace NPageObject
{
	/// <summary>
	/// 	Responsible for modelling an element on a page, selectable via the DOM, but without a defined selector of its own. Useful for conceptual element groupings without a direct selector of their own (e.g. a menu that is an li without an id), OR for element groupings that we simply do not need to address directly in UI tests.
	/// </summary>
	public class PageObjectElementSimple<TPage> : IPageObjectElementSimple<TPage>
		where TPage : IPageObject<TPage>, new()
	{
		public PageObjectElementSimple(IUITestContext<TPage> context,
		                               IPageObjectElementSimple<TPage> parentElement = null) {
			Context = context;
			ParentElement = parentElement;
		}

		public IPageObjectElementSimple<TPage> ParentElement { get; set; }

		public IUITestContext<TPage> Context { get; private set; }

		IUITestContext IHaveUITestContext.Context {
			get { return Context; }
		}

		public TPage ExpectedPage {
			get { return Context.ExpectedPage; }
		}

		public string SelectorFullyQualified {
			get {
				return ParentElement == null
				       	? CssSelector.Empty
				       	: ParentElement.SelectorFullyQualified + " " + CssSelector.Empty;
			}
		}

		public string[] SelectorsFullyQualified {
			get {
				return new[] {
					ParentElement == null
						? CssSelector.Empty
						: ParentElement.SelectorFullyQualified + " " + CssSelector.Empty
				};
			}
		}
	}
}