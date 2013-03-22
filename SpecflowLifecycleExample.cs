using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace YourNamespace
{
    /// <summary>
    /// NOTE: this has to be in the same DLL as your Specflow 
	/// tests, hence it is out of the src directory here for you 
	/// to copy elsewhere.
    /// </summary>
    [Binding]
    public class SpecflowLifecycle
    {
        [BeforeFeature("SpecflowLifecycle")]
        public static void BeforeFeature()
        {
            Scenario.AddContextWithName(Scenario.DefaultContextName);
        }

		[AfterFeature("SpecflowLifecycle")]
        public static void AfterFeature()
        {
            foreach (var c in Scenario.Contexts)
            {
                c.Value.Driver.Quit();
            }

            Scenario.Contexts.Clear();
        }
		
        [AfterScenario("SpecflowLifecycle")]
        public void AfterScenario()
        {
            foreach (var c in Scenario.Contexts)
            {
                c.Value.Driver.Manage().Cookies.DeleteAllCookies();
            }

            TestIssue.TearDown();
        }

        [AfterStep("SpecflowLifecycle")]
        public static void AfterStep()
        {
            if (ScenarioContext.Current.TestError == null)
            {
                return;
            }

            foreach (var c in Scenario.Contexts)
            {
                var screenShotsFolderPath = ConfigurationManager.AppSettings["WebDriverScreenshotsFolder"];
                ScreenshotCapturingHelpers.CreateDirectoryIfNotExists(screenShotsFolderPath);
                var imagePath = Path.Combine(screenShotsFolderPath, ScenarioContext.Current.ScenarioInfo.Title + ".png");
                    
                if (bool.Parse(ConfigurationManager.AppSettings["WebDriverScreenshotsEnabled"]))
                {
                    var screenShot = ((ITakesScreenshot)c.Value.Driver).GetScreenshot();
                    screenShot.SaveAsFile(imagePath, ImageFormat.Png);
                }
            }
        }
    }
}