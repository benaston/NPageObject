namespace NPageObject
{
    /// <summary>
    /// TDriver for selenium is IWebDriver.
    /// </summary>
    public interface ITestContext<out TDriver>
    {
        TDriver Driver { get; }

        void ExecuteScript(string scriptToExecute);
    }
}