using System;
using System.Collections.Generic;

namespace NPageObject.x.NPageObject
{
    public abstract class Scenario : IDisposable
    {
        private const string DefaultContextName = "default";
        private static readonly string StartUri = ConfigurationManager.AppSettings["TestUrl"];
        private static Dictionary<string, ITestContext<DefaultPage>> _contexts = new Dictionary<string, ITestContext<DefaultPage>>();

        static Scenario()
        {
            //configure and add a single context to act as the default
            AddContextWithName(DefaultContextName);
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
            var driver = new ChromeDriver();
            var domChecker = new SeleniumDomChecker(driver, TimeSpan.FromSeconds(5));
            var browserActionPerformer = new SeleniumBrowserActionPerformer(driver,
                                                                            domChecker,
                                                                            StartUri,
                                                                            TimeSpan.FromSeconds(5));
            Contexts.Add(DefaultContextName, new SeleniumTestContext<DefaultPage>(driver, browserActionPerformer, domChecker, StartUri));
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