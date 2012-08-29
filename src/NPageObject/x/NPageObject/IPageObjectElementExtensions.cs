// ReSharper disable InconsistentNaming

using System;
using System.Linq;

namespace NPageObject.x.NPageObject
{
    public static class IPageObjectElementExtensions
    {
        private const char CssClassDelimiter = ' ';

        public static bool TextContains<T>(this IElementOn<T> element, string text)
            where T : PageObject<T>, new()
        {
            if(string.IsNullOrWhiteSpace(element.SelectorFullyQualified))
            {
                throw new ArgumentException("element selector not supplied");
            }

            return element.Context.DomChecker.TextContains(element, text);
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
        
        public static TPage InputText<TPage>(this IElementOn<TPage> element, string text)
            where TPage : PageObject<TPage>, new()
        {
            element.Context.BrowserActionPerformer.InputText(element, text);

            return new TPage { Context = element.Context };
        }

        
    }
}
// ReSharper restore InconsistentNaming