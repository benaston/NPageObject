using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.PageObjects
{
    public class OverdueReportsTableElement : ElementOn<ReportsDuePage>
    {
        public OverdueReportsTableElement(ITestContext<ReportsDuePage> context)
            : base(context, selector: "#attentionList") { }

        public OverdueReportsTableRow OverdueReportForMethyleneChlorideTest
        {
            get
            {
                return new OverdueReportsTableRow(Context, this, templateId: "E-MTCLTest", issueId: "E-MTCL_201205230245");
            }
        }

        public OverdueReportsTableRow OverdueReportForTolueneDailyTest
        {
            get
            {
                return new OverdueReportsTableRow(Context, this, templateId: "F-TOLDTest", issueId: "F-TOLD_201205230245");
            }
        }
    }
}