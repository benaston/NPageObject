using System;
using System.Collections.Generic;
using System.Configuration;
using NPageObject.PageObject;
using NPageObject.Selenium;
using OpenQA.Selenium.Chrome;

namespace NPageObject
{
    /// <summary>
    /// Provides your test with a default test context, plus 
    /// the ability to add additional test contexts for tests 
    /// simulating multiple users. Specflow scenarios should 
    /// inherit from this type.
    /// </summary>
    public abstract class Scenario : IDisposable
    {
        public const string DefaultContextName = "default";
        private static readonly string StartUri = ConfigurationManager.AppSettings["TestUrl"];
        private static Dictionary<string, ITestContext<DefaultPage>> _contexts = new Dictionary<string, ITestContext<DefaultPage>>();

        static Scenario()
        {
        }

        public static Dictionary<string, ITestContext<DefaultPage>> Contexts
        {
            get { return _contexts; }

            protected set { _contexts = value; }
        }

        public static ITestContext<DefaultPage> DefaultContext 
        { 
            get { return Contexts[DefaultContextName]; } 
        }

        /// <summary>
        /// Useful for "multiple user" tests. 
        /// Each context corresponds to a browser session.
        /// </summary>
        public static void AddContextWithName(string name)
        {
            ChromeDriver driver = null;
            try
            {
                driver = new ChromeDriver();
                var domChecker = new SeleniumDomChecker(driver, TimeSpan.FromSeconds(5));
                var browserActionPerformer = new SeleniumBrowserActionPerformer(driver,
                                                                                domChecker,
                                                                                StartUri,
                                                                                TimeSpan.FromSeconds(5));
                Contexts.Add(DefaultContextName, new SeleniumTestContext<DefaultPage>(driver, browserActionPerformer, domChecker, StartUri));
            }
            catch(Exception)
            {
                if(driver != null) { driver.Quit(); }

                throw;
            }
        }

        public void Dispose()
        {
            foreach (var c in Contexts)
            {
                c.Value.Driver.Manage().Cookies.DeleteAllCookies();
            }
        }
    }
}