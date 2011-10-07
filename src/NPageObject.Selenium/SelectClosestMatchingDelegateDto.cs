namespace NPageObject.Selenium
{
    using OpenQA.Selenium;

    public class SelectClosestMatchingDelegateDto<TPage1>
        where TPage1 : IPageObject<TPage1>, new()
    {
        public IWebDriver Driver;
        public IPageObjectElement<TPage1> Element;
        public ElementVisibility ElementVisibility;
    }
}