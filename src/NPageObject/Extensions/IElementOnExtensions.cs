// ReSharper disable InconsistentNaming

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NSure;
using ArgumentNullException = NHelpfulException.FrameworkExceptions.ArgumentNullException;

namespace NPageObject.Extensions
{
    public static class IElementOnExtensions
    {
        private const char CssClassDelimiter = ' ';

        public static string Text<TDriver, TPage>(this IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            if(string.IsNullOrWhiteSpace(element.SelectorFullyQualified))
            {
                throw new ArgumentException("element selector not supplied");
            }

            return element.Context.DomChecker.GetText(element);
        }
        
        public static string Value<TDriver, TPage>(this IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            if(string.IsNullOrWhiteSpace(element.SelectorFullyQualified))
            {
                throw new ArgumentException("element selector not supplied");
            }

            return element.Context.DomChecker.GetAttributeValue(element, "value");
        }

        public static TPage Click<TDriver, TPage>(this IElementOn<TDriver, TPage> element) where TPage : PageObject<TDriver, TPage>, new()
        {
            element.Context.BrowserActionPerformer.Click(element);

            return new TPage { Context = element.Context };
        }
        
        public static bool HasClass<TDriver, TPage>(this IElementOn<TDriver, TPage> element, string @class) where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, "class").Split(CssClassDelimiter).Any(i => i == @class);
        }
        
        public static bool HasAttribute<TDriver, TPage>(this IElementOn<TDriver, TPage> element, string attribute) where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, attribute) != null;
        }

        public static TDestinationPage ClickWithNavigation<TDriver, TSourcePage, TDestinationPage>(this IElementOn<TDriver, TSourcePage> element)
            where TSourcePage : PageObject<TDriver, TSourcePage>, new()
            where TDestinationPage : PageObject<TDriver, TDestinationPage>, new()
        {
            element.Context.BrowserActionPerformer.Click(element);

            return new TDestinationPage
            {
                Context = element.Context.SetExpectedPage<TDestinationPage>()
            };
        }
        
        public static TPage InputText<TDriver, TPage>(this IElementOn<TDriver, TPage> element, string text)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            element.Context.BrowserActionPerformer.InputText(element, text);

            return new TPage { Context = element.Context };
        }

        /// <summary>
        /// Useful for when ajax changes the "page", for example Google "instant search".
        /// </summary>
        public static TDestinationPage InputTextWithNavigation<TDriver, TPage, TDestinationPage>(this ElementOn<TDriver, TPage> element, string text)
            where TPage : PageObject<TDriver, TPage>, new()
            where TDestinationPage : PageObject<TDriver, TDestinationPage>, new()
        {
            element.Context.BrowserActionPerformer.InputText(element, text);
            return new TDestinationPage { Context = element.Context.ExpectedPageIs<TDestinationPage>().Context };
        }

        public static IElementOn<TDriver, TPage> ClearValue<TDriver, TPage>(this ElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.BrowserActionPerformer.ClearValue(element);
        }

        public static IElementOn<TDriver, TPage> PressEnter<TDriver, TPage>(this ElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.BrowserActionPerformer.PressEnter(element);
        }

        public static string GetAttributeValue<TDriver, TPage>(this ElementOn<TDriver, TPage> element, string attributeName)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, attributeName);
        }

        public static ITestContext<TExpectedResultantPage> PressEnterWithPageNavigation
            <TDriver, TExpectedSourcePage, TExpectedResultantPage>(
            this ElementOn<TDriver, TExpectedSourcePage> element)
            where TExpectedSourcePage : PageObject<TDriver, TExpectedSourcePage>, new()
            where TExpectedResultantPage : PageObject<TDriver, TExpectedResultantPage>, new()
        {
            return
                element.Context.BrowserActionPerformer.PressEnterWithPageNavigation<TDriver, TExpectedSourcePage, TExpectedResultantPage>(
                    element);
        }

        public static ITestContext<TDriver, TPage> AndWaitFor<TDriver, TPage>(this ITestContext<TDriver, TPage> context,
                                                      TimeSpan timeSpan,
                                                      string reason)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            Ensure.That<ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

            Thread.Sleep(timeSpan);

            return context;
        }

        public static ElementOn<TDriver, TPage> AndWaitFor<TDriver, TPage>(this ElementOn<TDriver, TPage> element,
                                                          TimeSpan timeSpan)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            Ensure.That<NHelpfulException.FrameworkExceptions.ArgumentNullException>(timeSpan > TimeSpan.Zero, "timespan not supplied.");

            Thread.Sleep(timeSpan);

            return element;
        }

        public static ITestContext<TDriver, TPage> AndWaitFor<TDriver, TPage>(this ITestContext<TDriver, TPage> context,
                                                      Func<ITestContext<TDriver, TPage>, bool> waitFor,
                                                      TimeSpan maxTimeToWaitFor)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            Ensure.That<NHelpfulException.FrameworkExceptions.ArgumentNullException>(maxTimeToWaitFor > TimeSpan.Zero,
                                               "timespan not supplied.");
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
        /// 	Attempts to select the option with the specified text from a drop down box.
        /// </summary>
        public static TPage SelectOption<TDriver, TPage>(this ElementOn<TDriver, TPage> element, string optionText)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.BrowserActionPerformer.SelectFromDropDown(element, optionText);
        }

        public static TPage SelectOption<TDriver, TPage>(this ElementOn<TDriver, TPage> element, int optionIndex)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.BrowserActionPerformer.SelectFromDropDown(element, optionIndex);
        }

        public static bool IsChecked<TDriver, TPage>(this IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, "checked") == "checked" ||
                   element.Context.DomChecker.GetAttributeValue(element, "checked") == "true";
        }

        public static bool TextContains<TDriver, TPage>(this IElementOn<TDriver, TPage> element, string text)
        where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.TextContains(element, text);
        }


        public static bool IsVisible<TDriver, TPage>(this IElementOn<TDriver, TPage> element)
        where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.IsVisible(element);
        }

        public static bool SelectedOptionIs<TDriver, TPage>(this IElementOn<TDriver, TPage> element, string text)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.GetDropDownListSelectedItemText(element).Equals(text, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsEnabled<TDriver, TPage>(this IElementOn<TDriver, TPage> element)
            where TPage : PageObject<TDriver, TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, "disabled") != "disabled";
        }
    }
}
// ReSharper restore InconsistentNaming