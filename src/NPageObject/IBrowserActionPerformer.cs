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
	public interface IBrowserActionPerformer
	{
		IPageObjectElement<TPage> PressKey<TPage>(IPageObjectElement<TPage> element, Key key)
			where TPage : IPageObject<TPage>, new();

		IPageObjectElement<TPage> PressEnter<TPage>(IPageObjectElement<TPage> element)
			where TPage : IPageObject<TPage>, new();

		TNewPage BrowseTo<TNewPage>(string uriContentsRelativeToRoot)
			where TNewPage : IPageObject<TNewPage>, IHasMutableUrl, new();

		TNewPage BrowseTo<TNewPage>()
			where TNewPage : IPageObject<TNewPage>, new();

		IPageObjectElement<TPage> Click<TPage>(IPageObjectElement<TPage> element)
			where TPage : IPageObject<TPage>, new();

		IPageObjectElement<TPage> Hover<TPage>(IPageObjectElement<TPage> element)
			where TPage : IPageObject<TPage>, new();

		IPageObjectElement<TPage> InputText<TPage>(IPageObjectElement<TPage> element,
		                                           string text)
			where TPage : IPageObject<TPage>, new();

		IPageObjectElement<TPage> ClearValue<TPage>(IPageObjectElement<TPage> element)
			where TPage : IPageObject<TPage>, new();

		void MaximizeBrowserWindow();

		IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
			<TExpectedSourcePage, TExpectedResultantPage>(
			IPageObjectElement<TExpectedSourcePage> element)
			where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
			where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new();

		TPage SelectFromDropDown<TPage>(IPageObjectElement<TPage> element, string value)
			where TPage : IPageObject<TPage>, new();

		TPage SelectFromDropDown<TPage>(IPageObjectElement<TPage> element, int index)
			where TPage : IPageObject<TPage>, new();
	}
}