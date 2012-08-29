namespace NPageObject.x.NPageObject
{
    public class SelectClosestMatchingDelegateDto<TPage1>
        where TPage1 : PageObject<TPage1>, new()
    {
        public IWebDriver Driver;
        public IElementOn<TPage1> Element;
        public ElementVisibility ElementVisibility;
    }
}