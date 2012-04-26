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
	using System.Threading;
	using NSure;
	using ArgumentNullException = NHelpfulException.FrameworkExceptions.ArgumentNullException;

	public static class IPageObjectExtensions
	{
		/// <summary>
		/// 	Pause execution for a specified interval. Useful in rare cases where we know progressing without a wait will result in problems.
		/// </summary>
		/// <typeparam name="TPage"> The expected type of page object. </typeparam>
		/// <param name="pageObject"> The page object to return. </param>
		/// <param name="timeSpan"> Duration to wait for. </param>
		/// <param name="reason"> Please explain the reason for the wait as the underlying driver wrapper ensures robust selection for most cases. This string is unused programatically. </param>
		public static TPage AndWaitFor<TPage>(this TPage pageObject, TimeSpan timeSpan, string reason)
			where TPage : IPageObject<TPage>, new() {
			Ensure.That<ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

			Thread.Sleep(timeSpan);

			return pageObject;
		}
	}
}

// ReSharper restore InconsistentNaming