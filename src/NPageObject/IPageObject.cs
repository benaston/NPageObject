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
	/// 	Responsible for defining the interface for all page objects to implement.
	/// </summary>
	/// <typeparam name="TPage"> </typeparam>
	public interface IPageObject<TPage> where TPage : IPageObject<TPage>, new()
	{
		/// <summary>
		/// 	Include trailing slash.
		/// </summary>
		string UriRoot { get; }

		/// <summary>
		/// 	Do not include first slash.
		/// </summary>
		UriExpectation UriExpectation { get; }

		IUITestContext<TPage> Context { get; set; }

		/// <summary>
		/// 	A piece of text on the page that can be used to identify the page. e.g. the title.
		/// </summary>
		string IdentifyingText { get; }

		string Source { get; }

		TPage PerformAction(PageActionDelegate<TPage> action);
	}
}