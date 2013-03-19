using System;

namespace NPageObject.PageObject
{
    public class DefaultPage : PageObject<DefaultPage>
    {
        public override UriExpectation UriExpectation
        {
            get { throw new NotImplementedException(); }
        }

        public override string IdentifyingText
        {
            get { throw new NotImplementedException(); }
        }
    }
}