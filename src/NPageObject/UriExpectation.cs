using NPageObject.Enumerations;

namespace NPageObject
{
    /// <summary>
    /// Wraps up everything identifying a URI: a string 
    /// associated with the URI and a specification of 
    /// how strict to be when performing matching (regex, 
    /// exact, partial).
    /// </summary>
    public class UriExpectation
    {
        public UriMatch UriMatch { get; set; }

        public string UriContentsRelativeToRoot { get; set; }
    }
}