namespace NPageObject.Selenium
{
    using System.Linq;
    using NBasicExtensionMethod;
    using OpenQA.Selenium;

    /// <summary>
    ///   NOTE 1: BA; sometimes the DOM changes as we 
    ///   iterate over in-memory references to the DOM. 
    ///   If we query a DOM element that no-longer exists, 
    ///   then an exception is thrown, which we handle by 
    ///   discounting the element (returning false here).
    /// </summary>
    public class SeleniumDomCheckerHelper
    {
        public class IsTextVisibleDelegateDto
        {
            public IWebDriver Driver;
            public string TextToFind;
        }

        public class ContainsLinkToDelegateDto
        {
            public IWebDriver Driver;
            public string Text;
            public string Href;
        }

        public class ContainsLinkWithTextDto
        {
            public IWebDriver Driver;
            public string TextToFind;
        }

        public static ShouldRepeatDelegateInvocation ContainsLinkWithTextRepeatedlyInvocableDelegateImplementation(
            ContainsLinkWithTextDto dto, out bool outputValue)

        {
            var pageElements = dto.Driver.FindElements(By.PartialLinkText(dto.TextToFind));

            if (pageElements.Any())
            {
                outputValue = true;

                return ShouldRepeatDelegateInvocation.No;
            }

            outputValue = false;

            return ShouldRepeatDelegateInvocation.Yes;
        }

        public static ShouldRepeatDelegateInvocation IsTextVisibleStrictRepeatedlyInvocableDelegateImplementation(
            IsTextVisibleDelegateDto dto, out bool outputValue)
        {
            var pageElements = dto.Driver.FindElements(By.CssSelector("*"));

            if (pageElements.Any(e =>
                                     {
                                         try
                                         {
                                             return e.Text.NormalizeWhitespaceToSingleSpaces().Contains(dto.TextToFind);
                                         }
                                         catch (StaleElementReferenceException) //see note 1
                                         {
                                             return false;
                                         }
                                     }
                ))
            {
                outputValue = true;

                return ShouldRepeatDelegateInvocation.No;
            }

            outputValue = false;

            return ShouldRepeatDelegateInvocation.Yes;
        }

        public static ShouldRepeatDelegateInvocation IsTextVisibleRepeatedlyInvocableDelegateImplementation(
            IsTextVisibleDelegateDto dto, out bool outputValue)
        {
            var pageElements = dto.Driver.FindElements(By.CssSelector("*"));

            if (
                pageElements.Any(
                    e =>
                        {
                            try
                            {
                                return
                                    e.Text.StripWhitespace().ToLower().Contains(
                                        dto.TextToFind.StripWhitespace().ToLower());
                            }
                            catch (StaleElementReferenceException) //see note 1
                            {
                                return false;
                            }
                        }
                    ))
            {
                outputValue = true;

                return ShouldRepeatDelegateInvocation.No;
            }

            outputValue = false;

            return ShouldRepeatDelegateInvocation.Yes;
        }

        public static ShouldRepeatDelegateInvocation ContainsLinkToRepeatedlyInvocableDelegateImplementation(
            ContainsLinkToDelegateDto dto, out bool outputValue)
        {
            var pageElements = dto.Driver.FindElements(By.LinkText(dto.Text));

            if (
                pageElements.Where(element => element.TagName == "a").Any(
                    e =>
                        {
                            try
                            {
                                return e.GetAttribute("href") == dto.Href;
                            }
                            catch (StaleElementReferenceException) //see note 1
                            {
                                return false;
                            }
                        }))
            {
                outputValue = true;

                return ShouldRepeatDelegateInvocation.No;
            }

            outputValue = false;

            return ShouldRepeatDelegateInvocation.Yes;
        }
    }
}