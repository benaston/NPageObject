namespace NPageObject
{
    public enum PageIdentityVerificationType
    {
        Normal,
        SuppressElementRetrieval, //can be used when selenium is unable to parse a page (is this ever the case any more?)
    }
}