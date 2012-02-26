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
	using System;

	//Add ability to specify pause before using the page
	//incase things like facebook widgets are loaded into 
	//the dom which we want to work with
	public abstract class PageObject<TPage> : IPageObject<TPage>
		where TPage : IPageObject<TPage>, new()
	{
		public virtual string UriRoot {
			get { return Context.UriRoot; }
		}

		public abstract UriExpectation UriExpectation { get; }

		public IUITestContext<TPage> Context { get; set; }

		public string Source {
			get { return Context.PageSource; }
		}

		public abstract string IdentifyingText { get; }

		public virtual TPage PerformAction(PageActionDelegate<TPage> action) {
			throw new NotImplementedException(
				"Please implement the PerformAction method in your page object if you wish to supply lambdas to be invoked against it.");
		}

		public TExpectedRedirectType RedirectionOccursTo<TExpectedRedirectType>()
			where TExpectedRedirectType : IPageObject<TExpectedRedirectType>, new() {
			return Context.SetExpectedCurrentPage<TExpectedRedirectType>().ExpectedPage;
		}
	}
}