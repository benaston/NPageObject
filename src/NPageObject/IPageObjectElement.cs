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
	/// 	Responsible for defining the iface for page object elements that can be directly selected (and hence have actions performed upon like clicking, hovering etc).
	/// </summary>
	public interface IPageObjectElement<TParentPage> : IPageObjectElementSimple<TParentPage>
		where TParentPage : IPageObject<TParentPage>, new()
	{
		string Text { get; }

		/// <summary>
		/// 	Selector string associated directly with this element (ignoring any hierarchical selectors that might be required to find it in the DOM).
		/// </summary>
		string DirectSelector { get; }

		bool IsVisible();

		TDestinationPage PressEnterWithNavigation<TDestinationPage>()
			where TDestinationPage : IPageObject<TDestinationPage>, new();

		TDestinationPage ClickWithNavigation<TDestinationPage>()
			where TDestinationPage : IPageObject<TDestinationPage>, new();
	}
}