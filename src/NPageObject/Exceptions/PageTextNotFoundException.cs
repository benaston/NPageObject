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

namespace NPageObject.Exceptions
{
	using System;
	using NHelpfulException;

	public class PageTextNotFoundException<TPage> : HelpfulException
		where TPage : IPageObject<TPage>, new()
	{
		private const string _elementNotFoundMessage = "Unable to find text on page.";

		public PageTextNotFoundException(string textToFind)
			: base(string.Format("{0} Text to find: {1}.", _elementNotFoundMessage, textToFind)) {}

		public PageTextNotFoundException(string textToFind, string pageSource)
			: base(
				string.Format("{0} Text to find: {1}. Page source: {2}",
				              _elementNotFoundMessage,
				              textToFind,
				              pageSource)
				) {}

		public PageTextNotFoundException(string textToFind, Exception innerException)
			: base(
				string.Format("{0} Text to find: {1}.", _elementNotFoundMessage, textToFind),
				innerException: innerException) {}
	}
}

// ReSharper restore InconsistentNaming