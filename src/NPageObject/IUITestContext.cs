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
	/// 	See also parameterized type implementation.
	/// </summary>
	public interface IUITestContext
	{
		/// <example>
		/// 	http://localhost/
		/// </example>
		string UriRoot { get; }

		/// <example>
		/// 	/some/page.aspx
		/// </example>
		string UriActualRelative { get; }

		TNewPage BrowseTo<TNewPage>(string uriContentsRelativeToRoot)
			where TNewPage : IPageObject<TNewPage>, IHaveMutableUrl, new();

		TNew BrowseTo<TNew>() where TNew : IPageObject<TNew>, new();

		/// <summary>
		/// 	Case sensitive, whitespace in DOM converted to single spacing.
		/// </summary>
		bool IsTextVisibleStrict(string text);

		/// <summary>
		/// 	Case insensitive, whitespace in DOM removed (and hence "ignored").
		/// </summary>
		bool IsTextVisible(string text);

		/// <summary>
		/// 	Case insensitive, whitespace removed from text to find and the DOM.
		/// </summary>
		bool ContainsLinkWithText(string text);

		bool ContainsLinkWithTextAndHref(string text, string href);

		IUITestContext<T1> SetExpectedCurrentPage<T1>() where T1 : IPageObject<T1>, new();

		void ExecuteScript(string scriptToExecute);
	}
}

// ReSharper restore InconsistentNaming