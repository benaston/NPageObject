using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.PageObjects
{
    public abstract class DarwinEditorialPage<TPage> : PageObject<TPage> where TPage : PageObject<TPage>, new()
    {
        public IElementOn<TPage> IcisLogoLink
        {
            get { return new ElementOn<TPage>(Context, selector: ".icis-logo"); }
        }

        public IElementOn<TPage> SignOutLink
        {
            get { return new ElementOn<TPage>(Context, text: "Sign out"); }
        }
    }
}