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

	public class PageObjectElementNotFoundException<TPage> : HelpfulException
		where TPage : IPageObject<TPage>, new()
	{
		private const string _elementNotFoundMessage =
			"Unable to find element on page to match selector and/or text. Check your page object definitions.";

		public PageObjectElementNotFoundException(IPageObjectElement<TPage> poe)
			: base(
				string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}.",
				              _elementNotFoundMessage,
				              poe.SelectorFullyQualified,
				              string.Join(" ", poe.SelectorsFullyQualified),
				              poe.Text)) {}

		public PageObjectElementNotFoundException(IPageObjectElement<TPage> poe, string pageSource)
			: base(
				string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {3}. Page source: {4}",
				              _elementNotFoundMessage,
				              poe.SelectorFullyQualified,
				              string.Join(" ", poe.SelectorsFullyQualified),
				              poe.Text,
				              pageSource)) {}

		public PageObjectElementNotFoundException(IPageObjectElement<TPage> poe,
		                                          Exception innerException)
			: base(
				string.Format("{0} Selector: {1}. Alternate selectors: {2}. Text: {2}.",
				              _elementNotFoundMessage,
				              poe.SelectorFullyQualified,
				              string.Join(" ", poe.SelectorsFullyQualified),
				              poe.Text),
				innerException: innerException) {}
	}
}

// ReSharper restore InconsistentNaming