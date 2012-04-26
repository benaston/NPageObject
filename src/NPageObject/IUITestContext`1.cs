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

namespace NPageObject
{
	/// <summary>
	/// 	Facade for the UI test driver.
	/// </summary>
	public interface IUITestContext<TPage> : IUITestContext, ICurrentPageAware
		where TPage : IPageObject<TPage>, new()
	{
		TPage ExpectedPage { get; }
		string PageSource { get; }

		bool TextContains(IPageObjectElement<TPage> element, string text);

		string GetAttributeValue(IPageObjectElement<TPage> element, string attributeName);

		string GetText(IPageObjectElement<TPage> element);

		string GetDropDownListSelectedItemText(IPageObjectElement<TPage> element);

		IPageObjectElement<TPage> Click(IPageObjectElement<TPage> element);

		IPageObjectElement<TPage> Hover(IPageObjectElement<TPage> element);

		IPageObjectElement<TPage> InputText(IPageObjectElement<TPage> element, string text);

		IPageObjectElement<TPage> ClearValue(IPageObjectElement<TPage> element);

		IPageObjectElement<TPage> PressEnter(IPageObjectElement<TPage> element);

		IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
			<TExpectedSourcePage, TExpectedResultantPage>
			(IPageObjectElement<TExpectedSourcePage> element)
			where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
			where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new();

		TPage SelectFromDropDown(IPageObjectElement<TPage> element, string value);

		TPage SelectFromDropDown(IPageObjectElement<TPage> element, int index);

		bool IsVisible(IPageObjectElement<TPage> element);

		IUITestContext<TDestinationPage> PerformJourney<TDestinationPage>(
			JourneyDelegate<TPage, TDestinationPage> journey, dynamic dto = default(object))
			where TDestinationPage : IPageObject<TDestinationPage>, new();

		IPageObjectElement<TPage> PressKey(IPageObjectElement<TPage> element, Key key);
	}
}

// ReSharper restore InconsistentNaming