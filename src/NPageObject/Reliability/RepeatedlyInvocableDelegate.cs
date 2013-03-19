using NPageObject.Enumerations;

namespace NPageObject.Reliability
{
    public delegate ShouldRepeatDelegateInvocation RepeatedlyInvocableDelegate
        <in TDelegateDto, TOutputValue>(TDelegateDto dto, out TOutputValue returnValue);
}