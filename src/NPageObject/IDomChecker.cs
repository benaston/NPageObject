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
	/// 	Defines the iface for types that permit analysis of the DOM of a webpage.
	/// </summary>
	public interface IDomChecker
	{
		bool TextContains<TPage>(IPageObjectElement<TPage> element, string text)
			where TPage : IPageObject<TPage>, new();

		string GetAttributeValue<TPage>(IPageObjectElement<TPage> element, string attributeName)
			where TPage : IPageObject<TPage>, new();

		string GetText<TPage>(IPageObjectElement<TPage> element)
			where TPage : IPageObject<TPage>, new();

		string GetDropDownListSelectedItemText<TPage>(IPageObjectElement<TPage> element)
			where TPage : IPageObject<TPage>, new();

		bool IsVisible<TPage>(IPageObjectElement<TPage> element)
			where TPage : IPageObject<TPage>, new();

		bool IsTextVisibleStrict<TPage>(string text) where TPage : IPageObject<TPage>, new();

		bool IsTextVisible<TPage>(string text) where TPage : IPageObject<TPage>, new();

		bool ContainsLinkWithText<TPage>(string text) where TPage : IPageObject<TPage>, new();

		bool ContainsLinkWithTextAndHref<TPage>(string text, string href)
			where TPage : IPageObject<TPage>, new();
	}
}