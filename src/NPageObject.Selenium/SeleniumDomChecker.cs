namespace NPageObject.Selenium
{
    using System;
    using System.Linq;
    using Exceptions;
    using NSure;
    using OpenQA.Selenium;

    /// <summary>
    ///   Responsible for implementing the IDomChecker 
    ///   iface for selenium. See also the iface comment.
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

        public bool TextContains<TPage>(IPageObjectElement<TPage> element, string text)
            where TPage : IPageObject<TPage>, new()
        {
            var ele = SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element,
                                                                                      _maximumElementSelectionTime);

            return ele.Text.Contains(text);
        }

        public string GetAttributeValue<TPage>(IPageObjectElement<TPage> element, string attributeName)
            where TPage : IPageObject<TPage>, new()
        {
            return
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element,
                                                                                _maximumElementSelectionTime).
                    GetAttribute(
                        attributeName);
        }

        public string GetText<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new()
        {
            return
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element,
                                                                                _maximumElementSelectionTime).Text;
        }

        public string GetDropDownListSelectedItemText<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new()
        {
            var elements = SeleniumUITestContextHelpers.SelectNative(element.SelectorFullyQualified + " > option",
                                                                     _driver);

            Ensure.That(elements != null,
                        "unable to find drop down options matching selector \"" + element.SelectorFullyQualified +
                        " > option\"");

            var selectedElement =
                elements.Where(e => e.GetAttribute("selected") == "selected" || e.GetAttribute("selected") == "true").
                    FirstOrDefault();

            return selectedElement.Text ?? elements.First().Text;
        }

        public bool IsVisible<TPage>(IPageObjectElement<TPage> element)
            where TPage : IPageObject<TPage>, new()
        {
            try
            {
                SeleniumUITestContextHelpers.SelectClosestMatchingNativeElement(_driver, element,
                                                                                _maximumElementSelectionTime);
            }
            catch (GGPageObjectElementNotFoundException<TPage>)
            {
                return false;
            }

            return true;
        }

        public bool ContainsLinkWithText<TPage>(string text)
            where TPage : IPageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.ContainsLinkWithTextDto, bool> d =
                SeleniumDomCheckerHelper.ContainsLinkWithTextRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.ContainsLinkWithTextDto {Driver = _driver, TextToFind = text,};
            Action failureAction = () => { throw new GGHyperlinkNotFoundException<TPage>(text, _driver.PageSource); };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            }
            catch (GGHyperlinkNotFoundException<TPage>)
            {
                return false;
            }
        }

        public bool ContainsLinkWithTextAndHref<TPage>(string text, string href)
            where TPage : IPageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.ContainsLinkToDelegateDto, bool> d =
                SeleniumDomCheckerHelper.ContainsLinkToRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.ContainsLinkToDelegateDto {Driver = _driver, Text = text, Href = href,};
            Action failureAction = () => { throw new GGHyperlinkNotFoundException<TPage>(text, _driver.PageSource); };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            }
            catch (GGHyperlinkNotFoundException<TPage>)
            {
                return false;
            }
        }

        public bool IsTextVisibleStrict<TPage>(string text)
            where TPage : IPageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.IsTextVisibleDelegateDto, bool> d =
                SeleniumDomCheckerHelper.IsTextVisibleStrictRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.IsTextVisibleDelegateDto {Driver = _driver, TextToFind = text,};
            Action failureAction = () =>
                                       {
                                           throw new GGPageTextNotFoundException<TPage>(
                                               text, _driver.PageSource);
                                       };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            }
            catch (GGPageTextNotFoundException<TPage>)
            {
                return false;
            }
        }

        public bool IsTextVisible<TPage>(string text) where TPage : IPageObject<TPage>, new()
        {
            RepeatedlyInvocableDelegate<SeleniumDomCheckerHelper.IsTextVisibleDelegateDto, bool> d =
                SeleniumDomCheckerHelper.IsTextVisibleRepeatedlyInvocableDelegateImplementation;
            var dto = new SeleniumDomCheckerHelper.IsTextVisibleDelegateDto {Driver = _driver, TextToFind = text,};
            Action failureAction = () =>
                                       {
                                           throw new GGPageTextNotFoundException<TPage>(
                                               text, _driver.PageSource);
                                       };

            try
            {
                return d.InvokeRepeatedlyForUpTo(_maximumElementSelectionTime, dto, failureAction);
            }
            catch (GGPageTextNotFoundException<TPage>)
            {
                return false;
            }
        }
    }
}