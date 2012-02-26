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
	using System.Collections.Generic;

	/// <summary>
	/// 	Optional parent element meant to enable use of a hierarchy of PageObjectElementSelectables for DRY construction of selectors.
	/// </summary>
	public class PageObjectElement<TParentPage> : IPageObjectElement<TParentPage>
		where TParentPage : IPageObject<TParentPage>, new()
	{
		public PageObjectElement(IUITestContext<TParentPage> context,
		                         string selector = "*",
		                         string text = "",
		                         IPageObjectElementSimple<TParentPage> parentElement = null) {
			DirectSelector = selector;
			Text = text;
			Context = context;
			ParentElement = parentElement;
		}

		/// <summary>
		/// 	Use the selectors array if your element has multiple possible selectors, for example if different renderings for MVT purposes are used.
		/// </summary>
		public PageObjectElement(IUITestContext<TParentPage> context,
		                         string[] selectors,
		                         string text = "",
		                         IPageObjectElementSimple<TParentPage> parentElement = null) {
			DirectSelectors = selectors;
			Text = text;
			Context = context;
			ParentElement = parentElement;
		}

		public IPageObjectElementSimple<TParentPage> ParentElement { get; set; }

		public string[] DirectSelectors { get; set; }

		public string DirectSelector { get; set; }

		public bool IsVisible() {
			return Context.IsVisible(this);
		}

		public string SelectorFullyQualified {
			get {
				return ParentElement == null
				       	? DirectSelector
				       	: ParentElement.SelectorFullyQualified + " " + DirectSelector;
			}
		}

		public string[] SelectorsFullyQualified {
			get {
				var selectors = new List<string>();

				DirectSelectors = DirectSelectors ?? new string[0];

				foreach (var s in DirectSelectors) {
					selectors.Add(ParentElement == null
					              	? s
					              	: ParentElement.SelectorFullyQualified + " " + s);
				}

				return selectors.ToArray();
			}
		}

		public string Text { get; set; }

		public TParentPage ExpectedPage {
			get { return Context.ExpectedPage; }
		}

		public TDestinationPage PressEnterWithNavigation<TDestinationPage>()
			where TDestinationPage : IPageObject<TDestinationPage>, new() {
			this.PressEnter();
			return new TDestinationPage {Context = Context.SetExpectedCurrentPage<TDestinationPage>()};
		}

		public TDestinationPage ClickWithNavigation<TDestinationPage>()
			where TDestinationPage : IPageObject<TDestinationPage>, new() {
			this.Click();
			return new TDestinationPage {Context = Context.SetExpectedCurrentPage<TDestinationPage>()};
		}

		public IUITestContext<TParentPage> Context { get; private set; }

		IUITestContext IHaveUITestContext.Context {
			get { return Context; }
		}
	}
}