using DnR.Editorial.App.Tests.Acceptance.Support;
using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.PageObjects
{
    public static class DarwinEditorialPageActions
    {
        public static LoginPage LogOut<TPage>(this DarwinEditorialPage<TPage> page)
            where TPage : PageObject<TPage>, new()
        {
            return page.SignOutLink
                .ClickWithNavigation<TPage, LoginPage>();
        }

        public static ReportsDuePage LogOutAndInAgain<TPage>(this DarwinEditorialPage<TPage> page, Editor user)
            where TPage : PageObject<TPage>, new()
        {
            return page.SignOutLink
                       .ClickWithNavigation<TPage, LoginPage>()
                       .LogIn(user);
        }
    }
}