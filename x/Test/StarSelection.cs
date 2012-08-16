// ReSharper disable InconsistentNaming

using System.Linq;
using DnR.Editorial.App.Tests.Acceptance.PageObjects;
using DnR.Editorial.App.Tests.Acceptance.Support;
using DnR.ProductService.Domain;
using DnR.ProductService.Domain.Enums;
using DnR.ProductService.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.Features.Favourites
{
    /// <summary>
    /// NOTE 1: we reset preferences because favourites information is stored there.
    /// </summary>
    [Binding]
    public class StarSelection : Scenario
    {
        private static readonly Editor TestUser = UserProvider.TestEditor1;
        private static TestIssue TestIssue;

        [BeforeScenario("StarSelection")]
        public static void Setup()
        {
            //see note 1
            TestIssue = new TestIssue().Emtcl()
                .WithFrequency("Weekly", "3", "02", "35")
                .WithDeadDeadlineDateThreeDaysAgo()
                .WithWorkFlowStatus(WorkflowStatus.InProgress)
                .CreateIssueInMarkLogic();
            new PreferencesRepository().SetPreferences(new Preferences(), TestUser.UserName);
        }

        [Given(@"an editor is on the Reports Due page")]
        public void GivenAnEditorIsOnTheReportsDuePage()
        {
            var page = Context.NavigateTo<LoginPage>()
                              .LogIn(TestUser);

            Assert.IsTrue(page.MatchesActualBrowserLocation());
        }

        [When(@"they click on a hollow star next to a report issue")]
        public void WhenTheyClickOnAHollowStarNextToAReportIssue()
        {
            Context.ExpectedPageIs<ReportsDuePage>()
                   .OverdueReportsTable
                   .OverdueReportForMethyleneChlorideTest
                   .FavouriteStar
                   .Click().AndWaitFor(2.Seconds(), "wait for javascript on client");
        }

        [Then(@"the star is filled in with a yellow colour")]
        public void ThenTheStarIsFilledInWithAYellowColour()
        {
            var favouriteStar =
                Context.ExpectedPageIs<ReportsDuePage>()
                       .OverdueReportsTable
                       .OverdueReportForMethyleneChlorideTest
                       .FavouriteStar;

            Assert.IsTrue(favouriteStar.HasClass("isFavourite"));
        }

        [Then(@"the report \(not just the issue\) is marked as a favourite")]
        public void ThenTheReportNotJustTheIssueIsMarkedAsAFavourite()
        {
            Assert.IsTrue(new PreferencesRepository().GetPreferences(TestUser.UserName)
                        .FavouriteProductTemplates
                        .Any(t => t == TestIssue.Issue.TemplateId));
        }

        [AfterScenario("StarSelection")]
        public static void AfterScenario()
        {
            Context.Driver.Manage().Cookies.DeleteAllCookies();
            TestIssue.TearDown();
        }
    }
}

// ReSharper restore InconsistentNaming