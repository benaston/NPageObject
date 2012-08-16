using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.PageObjects
{
    public class ReportsDuePage : DarwinEditorialPage<ReportsDuePage>
    {
        public override UriExpectation UriExpectation
        {
            get { return new UriExpectation { UriContentsRelativeToRoot = "Product/Due", UriMatch = UriMatch.Partial }; }
        }

        public override string IdentifyingText
        {
            get { return "Preferences"; }
        }

        public OverdueReportsTableElement OverdueReportsTable
        {
            get { return new OverdueReportsTableElement(Context); }
        }
    }
}