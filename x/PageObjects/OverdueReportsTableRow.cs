using Tests.Common.PageObject;

namespace DnR.Editorial.App.Tests.Acceptance.PageObjects
{
    public class OverdueReportsTableRow : ElementOn<ReportsDuePage>
    {
        private readonly string _templateId;
        private readonly string _issueId;

        public OverdueReportsTableRow(ITestContext<ReportsDuePage> context, ElementOn<ReportsDuePage> parentElement, string templateId, string issueId)
            : base(context, string.Format(".productIssue[data-productIssueId*={0}]", templateId), parentElement: parentElement)
        {
            _templateId = templateId;
            _issueId = issueId;
        }

        public IElementOn<ReportsDuePage> FavouriteStar
        {
            get
            {
                return new ElementOn<ReportsDuePage>(Context, selector: ".isFavouriteIndicator", parentElement: this);
            }
        }

        public string TemplateId
        {
            get { return _templateId; }
        }

        public string IssueId
        {
            get { return _issueId; }
        }
    }
}