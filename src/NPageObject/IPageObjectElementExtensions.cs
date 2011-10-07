 // ReSharper disable InconsistentNaming

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
            where T : IPageObject<T>, new()
        {
            Ensure.That<ArgumentException>(string.IsNullOrWhiteSpace(element.SelectorFullyQualified),
                                           "element selector not supplied.");

            return element.Context.TextContains(element, text);
        }

        public static T Click<T>(this IPageObjectElement<T> element) where T : IPageObject<T>, new()
        {
            return element.Context.Click(element).ExpectedPage;
        }

        public static IPageObjectElement<T> Hover<T>(this IPageObjectElement<T> element) where T : IPageObject<T>, new()
        {
            return element.Context.Hover(element);
        }

        public static T InputText<T>(this IPageObjectElement<T> element, string text) where T : IPageObject<T>, new()
        {
            return element.Context.InputText(element, text).ExpectedPage;
        }

        public static IPageObjectElement<T> ClearValue<T>(this IPageObjectElement<T> element)
            where T : IPageObject<T>, new()
        {
            return element.Context.ClearValue(element);
        }

        public static IPageObjectElement<T> PressEnter<T>(this IPageObjectElement<T> element)
            where T : IPageObject<T>, new()
        {
            return element.Context.PressEnter(element);
        }

        public static IUITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
            <TExpectedSourcePage, TExpectedResultantPage>(this IPageObjectElement<TExpectedSourcePage> element)
            where TExpectedSourcePage : IPageObject<TExpectedSourcePage>, new()
            where TExpectedResultantPage : IPageObject<TExpectedResultantPage>, new()
        {
            return element.Context.PressEnterWithPageNavigation<TExpectedSourcePage, TExpectedResultantPage>(element);
        }

        public static IUITestContext<T> AndWaitFor<T>(this IUITestContext<T> context, TimeSpan timeSpan, string reason)
            where T : IPageObject<T>, new()
        {
            Ensure.That<ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

            Thread.Sleep(timeSpan);

            return context;
        }

        public static IPageObjectElement<T> AndWaitFor<T>(this IPageObjectElement<T> element, TimeSpan timeSpan)
            where T : IPageObject<T>, new()
        {
            Ensure.That<ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

            Thread.Sleep(timeSpan);

            return element;
        }

        public static IUITestContext<T> AndWaitFor<T>(this IUITestContext<T> context,
                                                      Func<IUITestContext<T>, bool> waitFor, TimeSpan maxTimeToWaitFor)
            where T : IPageObject<T>, new()
        {
            Ensure.That<ArgumentNullException>(maxTimeToWaitFor > TimeSpan.Zero, "timespan not supplied.");
            Ensure.That<ArgumentNullException>(waitFor != null, "wait for not supplied.");

            const int pollingSleep = 100;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed.TotalMilliseconds < maxTimeToWaitFor.TotalMilliseconds)
            {
                if (waitFor(context))
                {
                    return context;
                }

                Thread.Sleep(pollingSleep);
            }
            return context;
        }

        /// <summary>
        ///   Attempts to select the option with the 
        ///   specified text from a drop down box.
        /// </summary>
        public static T SelectOption<T>(this IPageObjectElement<T> element, string optionText)
            where T : IPageObject<T>, new()
        {
            return element.Context.SelectFromDropDown(element, optionText);
        }

        public static T SelectOption<T>(this IPageObjectElement<T> element, int optionIndex)
            where T : IPageObject<T>, new()
        {
            return element.Context.SelectFromDropDown(element, optionIndex);
        }
    }
}

// ReSharper restore InconsistentNaming