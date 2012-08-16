using System;
using System.Configuration;
using OpenQA.Selenium.Chrome;

namespace Tests.Common.PageObject
{
    public abstract class Scenario : IDisposable
    {
        private static readonly string StartUri = ConfigurationManager.AppSettings["TestUrl"];

        static Scenario()
        {
            var driver = new ChromeDriver();
            var domChecker = new SeleniumDomChecker(driver, TimeSpan.FromSeconds(5));
            var browserActionPerformer = new SeleniumBrowserActionPerformer(driver, 
                                                                            domChecker, 
                                                                            StartUri,
                                                                            TimeSpan.FromSeconds(5));
            Context = new SeleniumTestContext<DefaultPage>(driver, browserActionPerformer, domChecker, StartUri);
        }

        public static ITestContext<DefaultPage> Context { get; protected set; }

        public void Dispose() { Context.Driver.Quit(); }
    }
}