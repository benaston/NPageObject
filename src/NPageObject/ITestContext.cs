using OpenQA.Selenium;

namespace NPageObject
{
    public interface ITestContext
    {
        IWebDriver Driver { get; }
    }
}