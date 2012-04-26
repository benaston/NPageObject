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
	using System;
	using System.Text.RegularExpressions;

	public class UriExpectationHelper
	{
		/// <summary>
		/// 	Returns true if actual URI from the context matches the expected URI from the PageObject.
		/// </summary>
		public static bool DoesActualMatchExpectedUri<T>(T page, IUITestContext<T> uiTestContext)
			where T : IPageObject<T>, new() {
			switch (page.UriExpectation.Match) {
				case UriMatch.Exact:
					return uiTestContext.UriActualAbsolute ==
					       page.UriRoot + page.UriExpectation.UriContentsRelativeToRoot;
				case UriMatch.Partial:
					return
						uiTestContext.UriActualAbsolute.Contains(page.UriExpectation.UriContentsRelativeToRoot);
				case UriMatch.Regex:
					return Regex.IsMatch(uiTestContext.UriActualAbsolute,
					                     page.UriExpectation.UriContentsRelativeToRoot);
				default:
					throw new Exception("Invalid UriMatch value.");
			}
		}
	}
}