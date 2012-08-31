namespace NPageObject
{
    public delegate ShouldRepeatDelegateInvocation RepeatedlyInvocableDelegate
        <in TDelegateDto, TOutputValue>(TDelegateDto dto, out TOutputValue returnValue);
}