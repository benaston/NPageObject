namespace NPageObject.Selenium
{
    using System;

    public class DelegateInvocationTimeoutException : Exception
    {
        private new const string Message = "Repeated delegate invocations resulted in no successful result.";

        public DelegateInvocationTimeoutException() : base(Message) { }
    }
}