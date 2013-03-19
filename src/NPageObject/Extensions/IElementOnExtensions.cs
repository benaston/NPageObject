// ReSharper disable InconsistentNaming

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NPageObject.PageObject;

namespace NPageObject.Extensions
{
    /// <summary>
    /// These extension methods help make tests read more like English.
    /// </summary>
    public static class IElementOnExtensions
    {
        private const char CssClassDelimiter = ' ';

        public static bool TextContains<T>(this IElementOn<T> element, string text)
            where T : PageObject<T>, new()
        {
            if (string.IsNullOrWhiteSpace(element.SelectorFullyQualified))
            {
                throw new ArgumentException("element selector not supplied");
            }

            return element.Context.DomChecker.TextContains(element, text);
        }

        public static string Text<TPage>(this IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            if (string.IsNullOrWhiteSpace(element.SelectorFullyQualified))
            {
                throw new ArgumentException("element selector not supplied");
            }

            return element.Context.DomChecker.GetText(element);
        }

        public static string Value<TPage>(this IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            if (string.IsNullOrWhiteSpace(element.SelectorFullyQualified))
            {
                throw new ArgumentException("element selector not supplied");
            }

            return element.Context.DomChecker.GetAttributeValue(element, "value");
        }

        public static TPage Click<TPage>(this IElementOn<TPage> element) where TPage : PageObject<TPage>, new()
        {
            element.Context.BrowserActionPerformer.Click(element);

            return new TPage { Context = element.Context };
        }

        public static bool HasClass<TPage>(this IElementOn<TPage> element, string @class) where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, "class").Split(CssClassDelimiter).Any(i => i == @class);
        }

        public static bool DoesNotHaveClass<TPage>(this IElementOn<TPage> element, string @class) where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, "class").Split(CssClassDelimiter).All(i => i != @class);
        }

        public static bool ColorIs<TPage>(this IElementOn<TPage> element, string style) where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetStyleValue(element, "color").RgbaToHexColor() == style;
        }

        public static bool HasAttribute<TPage>(this IElementOn<TPage> element, string attribute) where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, attribute) != null;
        }

        public static TDestinationPage ClickWithNavigation<TSourcePage, TDestinationPage>(this IElementOn<TSourcePage> element)
            where TSourcePage : PageObject<TSourcePage>, new()
            where TDestinationPage : PageObject<TDestinationPage>, new()
        {
            element.Context.BrowserActionPerformer.Click(element);

            return new TDestinationPage
            {
                Context = element.Context.SetExpectedPage<TDestinationPage>()
            };
        }

        public static TDestinationPage PressEnterWithNavigation<TSourcePage, TDestinationPage>(this IElementOn<TSourcePage> element)
            where TSourcePage : PageObject<TSourcePage>, new()
            where TDestinationPage : PageObject<TDestinationPage>, new()
        {
            element.Context.BrowserActionPerformer.PressEnter(element);

            return new TDestinationPage
            {
                Context = element.Context.SetExpectedPage<TDestinationPage>()
            };
        }

        public static TPage InputText<TPage>(this IElementOn<TPage> element, string text)
            where TPage : PageObject<TPage>, new()
        {
            element.Context.BrowserActionPerformer.InputText(element, text);

            return new TPage { Context = element.Context };
        }

        /// <summary>
        /// Useful for when ajax changes the "page", for example Google "instant search".
        /// </summary>
        public static TDestinationPage InputTextWithNavigation<TPage, TDestinationPage>(this IElementOn<TPage> element, string text)
            where TPage : PageObject<TPage>, new()
            where TDestinationPage : PageObject<TDestinationPage>, new()
        {
            element.Context.BrowserActionPerformer.InputText(element, text);
            return new TDestinationPage { Context = element.Context.ExpectedPageIs<TDestinationPage>().Context };
        }

        public static IElementOn<TPage> Clear<TPage>(this IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.BrowserActionPerformer.Clear(element);
        }

        public static IElementOn<TPage> PressEnter<TPage>(this IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.BrowserActionPerformer.PressEnter(element);
        }

        public static string GetAttributeValue<TPage>(this IElementOn<TPage> element, string attributeName)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, attributeName);
        }

        public static string GetCssStyleValue<TPage>(this IElementOn<TPage> element, string cssStyleName)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetStyleValue(element, cssStyleName);
        }

        public static ITestContext<TPage> AndWaitFor<TPage>(this ITestContext<TPage> context,
                                                      TimeSpan timeSpan,
                                                      string reason)
            where TPage : PageObject<TPage>, new()
        {
            if (timeSpan <= TimeSpan.Zero)
            {
                throw new ArgumentException("timespan");
            }

            Thread.Sleep(timeSpan);

            return context;
        }

        public static IElementOn<TPage> AndWaitFor<TPage>(this IElementOn<TPage> element,
                                                          TimeSpan timeSpan)
            where TPage : PageObject<TPage>, new()
        {
            if (timeSpan <= TimeSpan.Zero)
            {
                throw new ArgumentException("timespan");
            }

            Thread.Sleep(timeSpan);

            return element;
        }

        public static ITestContext<TPage> AndWaitFor<TPage>(this ITestContext<TPage> context,
                                                      Func<ITestContext<TPage>, bool> waitFor,
                                                      TimeSpan maxTimeToWaitFor)
            where TPage : PageObject<TPage>, new()
        {
            if (maxTimeToWaitFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("maxTimeToWaitFor");
            }

            if (waitFor == null)
            {
                throw new ArgumentException("waitFor");
            }

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
        /// Attempts to select the option with the specified text from a drop down box.
        /// </summary>
        public static TPage SelectOption<TPage>(this IElementOn<TPage> element, string optionText)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.BrowserActionPerformer.SelectFromDropDown(element, optionText);
        }

        public static TPage SelectOption<TPage>(this IElementOn<TPage> element, int optionIndex)
            where TPage : PageObject<TPage>, new()
        {
            element.Context.BrowserActionPerformer.SelectFromDropDown(element, optionIndex);

            //is this neccessary?
            return new TPage
            {
                Context = element.Context.SetExpectedPage<TPage>()
            };
        }

        public static bool IsChecked<TPage>(this IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, "checked") == "checked" ||
                   element.Context.DomChecker.GetAttributeValue(element, "checked") == "true";
        }

        public static bool IsVisible<TPage>(this IElementOn<TPage> element)
        where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.IsVisible(element);
        }

        public static bool SelectedOptionIs<TPage>(this IElementOn<TPage> element, string text)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetDropDownListSelectedItemText(element).Equals(text, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsEnabled<TPage>(this IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            return element.Context.DomChecker.GetAttributeValue(element, "disabled") != "disabled";
        }
    }
}
// ReSharper restore InconsistentNaming