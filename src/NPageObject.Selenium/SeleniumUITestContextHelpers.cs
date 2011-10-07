// ReSharper disable InconsistentNaming
namespace NPageObject.Selenium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NPageObject.Exceptions;
    using NSure;
    using OpenQA.Selenium;
    using ArgumentException = NHelpfulException.FrameworkExceptions.ArgumentException;
    using ArgumentNullException = NHelpfulException.FrameworkExceptions.ArgumentNullException;

    /// <summary>
    ///   Helper functionality for the SeleniumUITestContext. Helps 
    ///   thin out the SeleniumUITestContext type (which has a lot 
    ///   of responsibility).
    /// </summary>
    public static class SeleniumUITestContextHelpers
    {
        public const int DefaultMaximumSelectionTimeInSeconds = 15;

        public static IWebElement SelectClosestMatchingNativeElement<TPage1>(IWebDriver driver,
                                                                             IPageObjectElement<TPage1> element,
                                                                             TimeSpan maximimSelectionElapsedTime,
                                                                             DesiredSelectionSpeed selectionSpeed =
                                                                                 DesiredSelectionSpeed.Normal,
                                                                             ElementVisibility elementVisibility
                                                                                 = ElementVisibility.Visible)
            where TPage1 : IPageObject<TPage1>, new()
        {
            if (maximimSelectionElapsedTime == TimeSpan.Zero)
            {
                maximimSelectionElapsedTime = TimeSpan.FromSeconds(DefaultMaximumSelectionTimeInSeconds);
            }

            var dto = new SelectClosestMatchingDelegateDto<TPage1>
                          {Driver = driver, Element = element, ElementVisibility = elementVisibility,};
            RepeatedlyInvocableDelegate<SelectClosestMatchingDelegateDto<TPage1>, IWebElement> d =
                SelectClosestMatchingElementRepeatedlyInvocableDelegateImplementation;
            Action failureAction =
                () => { throw new GGPageObjectElementNotFoundException<TPage1>(element, driver.PageSource); };

            return d.InvokeRepeatedlyForUpTo(maximimSelectionElapsedTime, dto, failureAction);
        }

        public static string GetAbsoluteUriFromPath<TPage2>(IWebDriver driver, string uriRoot, IDomChecker domChecker,
                                                            IBrowserActionPerformer browserActionPerformer)
            where TPage2 : IPageObject<TPage2>, new()
        {
            var page = new TPage2
                           {
                               Context =
                                   new SeleniumUITestContext<TPage2>(driver,
                                                                     browserActionPerformer,
                                                                     domChecker,
                                                                     uriRoot)
                           };

            return page.UriRoot.Trim('/') + "/" + page.UriExpectation.UriContentsRelativeToRoot.TrimStart('/');
        }

        /// <summary>
        ///   Returns the selenium "native" elements (IWebElement).
        /// </summary>
        public static IEnumerable<IWebElement> SelectNative(string selector, ISearchContext driver,
                                                            ElementVisibility visibility = ElementVisibility.Visible)
        {
            Ensure.That<ArgumentException>(string.IsNullOrWhiteSpace(selector), "selector not supplied.")
                .And<ArgumentNullException>(driver != null, "driver not supplied.");

            var elements = (selector.StartsWith("/")
                                ? driver.FindElements(By.XPath(selector))
                                : driver.FindElements(By.CssSelector(selector))).ToList();

            switch (visibility)
            {
                case ElementVisibility.Visible:
                    return elements.Where(e => e.Displayed).ToList();
                case ElementVisibility.NotVisible:
                    return elements.Where(e => !e.Displayed).ToList();
                default:
                    return elements;
            }
        }

        private static ShouldRepeatDelegateInvocation
            SelectClosestMatchingElementRepeatedlyInvocableDelegateImplementation<TPage1>(
            SelectClosestMatchingDelegateDto<TPage1> dto, out IWebElement matchingElement)
            where TPage1 : IPageObject<TPage1>, new()
        {
            try
            {
                var nativeElements = string.IsNullOrWhiteSpace(dto.Element.SelectorFullyQualified)
                                         ? new List<IWebElement>()
                                         : SelectNative(dto.Element.SelectorFullyQualified, dto.Driver,
                                                        dto.ElementVisibility);

                foreach (var selector in dto.Element.SelectorsFullyQualified)
                {
                    nativeElements = nativeElements.Union(SelectNative(selector, dto.Driver, dto.ElementVisibility));
                }

                if (nativeElements!= null && nativeElements.Any())
                {
                    var elementToReturn = TryFilterNativeElementsByContentString(dto.Element, nativeElements);

                    if (elementToReturn != null)
                    {
                        matchingElement = elementToReturn;

                        return ShouldRepeatDelegateInvocation.No;
                    }
                }
            }
            catch (StaleElementReferenceException)
            {
                //continue and wait to retry...
            }

            matchingElement = default(IWebElement);

            return ShouldRepeatDelegateInvocation.Yes;
        }

        /// <summary>
        ///   Takes the text associated with a non-native element (needle) and a 
        ///   collection of native elements (haystack) and finds the 
        ///   closest match to the needle in the haystack.
        ///   NOTE 1: BA; InvalidOperationException is thrown sometimes when calling 
        ///   the Text property on elements.
        /// </summary>
        private static IWebElement TryFilterNativeElementsByContentString<TPage1>(IPageObjectElement<TPage1> needle,
                                                                                  IEnumerable<IWebElement> haystack)
            where TPage1 : IPageObject<TPage1>, new()
        {
            Ensure.That(needle != null, "needle not supplied.");

// ReSharper disable PossibleNullReferenceException
            if (string.IsNullOrWhiteSpace(needle.Text))
// ReSharper restore PossibleNullReferenceException
            {
                return haystack.First();
            }

            var elementsByText = haystack.Where(e =>
                                                    {
                                                        try
                                                        {
                                                            return e.Text.Trim().ToLower() ==
                                                                   needle.Text.Trim().ToLower();
                                                        }
                                                        catch (InvalidOperationException)
                                                        {
                                                            return false;
                                                        }
                                                    });

            if (elementsByText != null && elementsByText.Any())
            {
                return elementsByText.First();
            }

            var elementsByHref =
                haystack.Where(e => e.GetAttribute("href") != null && e.GetAttribute("href").Contains(needle.Text));
            if (elementsByHref != null && elementsByHref.Any())
            {
                return elementsByHref.First();
            }

            var elementsByAltText =
                haystack.Where(
                    e => e.GetAttribute("alt") != null && e.GetAttribute("alt").Contains(needle.Text));
            if (elementsByAltText != null && elementsByAltText.Any())
            {
                return elementsByAltText.First();
            }

            var elementsByTitleText =
                haystack.Where(
                    e =>
                    e.GetAttribute("title") != null && e.GetAttribute("title").Contains(needle.Text));
            if (elementsByTitleText != null && elementsByTitleText.Any())
            {
                return elementsByTitleText.First();
            }

            var elementsByValueText =
                haystack.Where(
                    e =>
                    e.GetAttribute("value") != null && e.GetAttribute("value").Contains(needle.Text));
            if (elementsByValueText != null && elementsByValueText.Any())
            {
                return elementsByValueText.First();
            }

            return null;
        }
    }
}

// ReSharper restore InconsistentNaming