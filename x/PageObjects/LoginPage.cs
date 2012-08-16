using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.PageObjects
{
    public class LoginPage : PageObject<LoginPage> 
    {
        public override UriExpectation UriExpectation
        {
            get { return new UriExpectation { UriContentsRelativeToRoot = "Account/Login", UriMatch = UriMatch.Partial }; }
        }

        public override string IdentifyingText
        {
            get
            {
                return "Welcome to the ICIS Darwin Editorial tool";
            }
        }

        public IElementOn<LoginPage> UserNameTextField
        {
            get
            {
                return new ElementOn<LoginPage>(Context, selector: "#UserName");
            }
        }

        public IElementOn<LoginPage> PasswordTextField
        {
            get
            {
                return new ElementOn<LoginPage>(Context, selector: "#Password");
            }
        }

        public IElementOn<LoginPage> LoginButton
        {
            get
            {
                return new ElementOn<LoginPage>(Context, selector: "#Submit");
            }
        }
    }
}