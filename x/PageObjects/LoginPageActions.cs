using DnR.Editorial.App.Tests.Acceptance.Support;
using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.PageObjects
{
    public static class LoginPageActions
    {
        public static ReportsDuePage LogIn(this LoginPage page, Editor user)
        {
            return page.UserNameTextField
                       .InputText(user.UserName)
                       .PasswordTextField
                       .InputText(user.Password)
                       .LoginButton
                       .ClickWithNavigation<LoginPage, ReportsDuePage>();
        }
    }
}