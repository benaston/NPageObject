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
	using System.Diagnostics;
	using System.Threading;
	using NSure;
	using ArgumentException = NHelpfulException.FrameworkExceptions.ArgumentException;
	using ArgumentNullException = NHelpfulException.FrameworkExceptions.ArgumentNullException;

	public static class IPageObjectElementExtensions
	{
		public static bool TextContains<T>(this IPageObjectElement<T> element, string text)
			where T : IPageObject<T>, new() {
			Ensure.That<ArgumentException>(!string.IsNullOrWhiteSpace(element.SelectorFullyQualified),
			                               "element selector not supplied.");

			return element.Context.TextContains(element, text);
		}

		public static T Click<T>(this IPageObjectElement<T> element) where T : IPageObject<T>, new() {
			return element.Context.Click(element).ExpectedPage;
		}

		public static IPageObjectElement<T> Hover<T>(this IPageObjectElement<T> element)
			where T : IPageObject<T>, new() {
			return element.Context.Hover(element);
		}

		public static T InputText<T>(this IPageObjectElement<T> element, string text)
			where T : IPageObject<T>, new() {
			return element.Context.InputText(element, text).ExpectedPage;
		}

		/// <summary>
		/// Useful for when ajax changes the "page", for example Google "instant search".
		/// </summary>
		public static TDestinationPage InputTextWithNavigation<T, TDestinationPage>(this IPageObjectElement<T> element, string text)
			where T : IPageObject<T>, new()
			where TDestinationPage : IPageObject<TDestinationPage>, new()
		{
				element.Context.InputText(element, text);
			return new TDestinationPage { Context = element.Context.SetExpectedCurrentPage<TDestinationPage>() };
		}

		public static IPageObjectElement<T> ClearValue<T>(this IPageObjectElement<T> element)
			where T : IPageObject<T>, new() {
			return element.Context.ClearValue(element);
		}

		public static IPageObjectElement<T> PressEnter<T>(this IPageObjectElement<T> element)
			where T : IPageObject<T>, new() {
			return element.Context.PressEnter(element);
		}

		public static string GetAttributeValue<T>(this IPageObjectElement<T> element, string attributeName)
			where T : IPageObject<T>, new()
		{
			return element.Context.GetAttributeValue(element, attributeName);
		}

		public static IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
			<TExpectedSourcePage, TExpectedResultantPage>(
			this IPageObjectElement<TExpectedSourcePage> element)
			where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
			where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new() {
			return
				element.Context.PressEnterWithPageNavigation<TExpectedSourcePage, TExpectedResultantPage>(
					element);
		}

		public static IUITestContext<T> AndWaitFor<T>(this IUITestContext<T> context,
		                                              TimeSpan timeSpan,
		                                              string reason)
			where T : IPageObject<T>, new() {
			Ensure.That<ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

			Thread.Sleep(timeSpan);

			return context;
		}

		public static IPageObjectElement<T> AndWaitFor<T>(this IPageObjectElement<T> element,
		                                                  TimeSpan timeSpan)
			where T : IPageObject<T>, new() {
			Ensure.That<ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

			Thread.Sleep(timeSpan);

			return element;
		}

		public static IUITestContext<T> AndWaitFor<T>(this IUITestContext<T> context,
		                                              Func<IUITestContext<T>, bool> waitFor,
		                                              TimeSpan maxTimeToWaitFor)
			where T : IPageObject<T>, new() {
			Ensure.That<ArgumentNullException>(maxTimeToWaitFor > TimeSpan.Zero,
			                                   "timespan not supplied.");
			Ensure.That<ArgumentNullException>(waitFor != null, "wait for not supplied.");

			const int pollingSleep = 100;
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			while (stopwatch.Elapsed.TotalMilliseconds < maxTimeToWaitFor.TotalMilliseconds) {
				if (waitFor(context)) {
					return context;
				}

				Thread.Sleep(pollingSleep);
			}
			return context;
		}

		/// <summary>
		/// 	Attempts to select the option with the specified text from a drop down box.
		/// </summary>
		public static T SelectOption<T>(this IPageObjectElement<T> element, string optionText)
			where T : IPageObject<T>, new() {
			return element.Context.SelectFromDropDown(element, optionText);
		}

		public static T SelectOption<T>(this IPageObjectElement<T> element, int optionIndex)
			where T : IPageObject<T>, new() {
			return element.Context.SelectFromDropDown(element, optionIndex);
		}
	}
}

// ReSharper restore InconsistentNaming