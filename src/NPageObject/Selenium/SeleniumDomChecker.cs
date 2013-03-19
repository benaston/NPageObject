using System;
using System.Linq;
using NPageObject.Exceptions;
using NPageObject.Extensions;
using NPageObject.PageObject;
using NPageObject.Reliability;
using OpenQA.Selenium;

namespace NPageObject.Selenium
{
    /// <summary>
    /// Wraps up all DOM-related functionality of Selenium web driver.
    /// </summary>
    public class SeleniumDomChecker : IDomChecker
    {
        private readonly IWebDriver _driver;
        private readonly TimeSpan _maximumElementSelectionTime;

        public SeleniumDomChecker(IWebDriver driver, TimeSpan maximumElementSelectionTime)
        {
            _driver = driver;
            _maximumElementSelectionTime = maximumElementSelectionTime;
        }

        public bool TextContains<TPage>(IElementOn<TPage> element, string text)
            where TPage : PageObject<TPage>, new()
        {
            var ele = SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                      element,
                                                                                      _maximumElementSelectionTime);

            return ele.Text.Contains(text);
        }

        public string GetAttributeValue<TPage>(IElementOn<TPage> element,
                                               string attributeName)
            where TPage : PageObject<TPage>, new()
        {
            return
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                element,
                                                                                _maximumElementSelectionTime)
                    .
                    GetAttribute(
                        attributeName);
        }
        
        public string GetStyleValue<TPage>(IElementOn<TPage> element,
                                               string style)
            where TPage : PageObject<TPage>, new()
        {
            return
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                element,
                                                                                _maximumElementSelectionTime)
                    .GetCssValue(style);
        }

        public string GetText<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            return
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                element,
                                                                                _maximumElementSelectionTime)
                    .Text;
        }

        public string GetDropDownListSelectedItemText<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            var elements =
                SeleniumUITestContextHelpers.SelectNative(element.SelectorFullyQualified + " > option",
                                                          _driver);

            if (elements == null)
            {
                throw new Exception("unable to find drop down options matching selector \"" +
                                    element.SelectorFullyQualified +
                                    " > option\"");
            }

            var selectedElement =
                elements.FirstOrDefault(
                    e => e.GetAttribute("selected") == "selected" || e.GetAttribute("selected") == "true");

            return selectedElement.Text ?? elements.First().Text;
        }

        public bool IsVisible<TPage>(IElementOn<TPage> element)
            where TPage : PageObject<TPage>, new()
        {
            try
            {
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver,
                                                                                element,
                                                                                _maximumElementSelectionTime);
            } catch (PageObjectElementNotFoundException<TPage>)
            {
                return false;
            }

            return true;
        }

        public bool ContainsLinkWithText<TPage>(string text)
            where TPage : PageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.ContainsLinkWithTextDto, bool> d =
                SeleniumDomCheckerHelper.ContainsLinkWithTextRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.ContainsLinkWithTextDto {Driver = _driver, TextToFind = text,};
            Action failureAction =
                () => { throw new HyperlinkNotFoundException<TPage>(text, _driver.PageSource); };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            } catch (HyperlinkNotFoundException<TPage>)
            {
                return false;
            }
        }

        public bool ContainsLinkWithTextAndHref<TPage>(string text, string href)
            where TPage : PageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.ContainsLinkToDelegateDto, bool> d =
                SeleniumDomCheckerHelper.ContainsLinkToRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.ContainsLinkToDelegateDto
            {Driver = _driver, Text = text, Href = href,};
            Action failureAction =
                () => { throw new HyperlinkNotFoundException<TPage>(text, _driver.PageSource); };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            } catch (HyperlinkNotFoundException<TPage>)
            {
                return false;
            }
        }

        public bool IsTextVisibleStrict<TPage>(string text)
            where TPage : PageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.IsTextVisibleDelegateDto, bool> d =
                SeleniumDomCheckerHelper.IsTextVisibleStrictRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.IsTextVisibleDelegateDto {Driver = _driver, TextToFind = text,};
            Action failureAction = () => {
                throw new PageTextNotFoundException<TPage>(
                    text, _driver.PageSource);
            };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            } catch (PageTextNotFoundException<TPage>)
            {
                return false;
            }
        }

        public bool IsTextVisible<TPage>(string text) where TPage : PageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.IsTextVisibleDelegateDto, bool> d =
                SeleniumDomCheckerHelper.IsTextVisibleRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.IsTextVisibleDelegateDto {Driver = _driver, TextToFind = text,};
            Action failureAction = () => {
                throw new PageTextNotFoundException<TPage>(
                    text, _driver.PageSource);
            };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            } catch (PageTextNotFoundException<TPage>)
            {
                return false;
            }
        }
    }
}