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
	using NUnitConstraints;

	/// <summary>
	/// 	Type to hang fluent NUnit constraints off. See also <see cref="CurrentPage" /> .
	/// </summary>
	public static class Current
	{
		public static ActualMatchesExpectedLocationConstraint<TPage> LocationIs<TPage>()
			where TPage : IPageObject<TPage>, new() {
			return new ActualMatchesExpectedLocationConstraint<TPage>();
		}

		public static TextIsVisibleConstraintExpectingPageObject<TPage> TextIsVisible<TPage>(
			string text)
			where TPage : IPageObject<TPage>, new() {
			return new TextIsVisibleConstraintExpectingPageObject<TPage>(text, StringMatch.Lenient);
		}

		public static TextIsVisibleConstraintExpectingPageObject<TPage> TextIsVisibleLenient<TPage>(
			string text)
			where TPage : IPageObject<TPage>, new() {
			return new TextIsVisibleConstraintExpectingPageObject<TPage>(text, StringMatch.Lenient);
		}
	}
}