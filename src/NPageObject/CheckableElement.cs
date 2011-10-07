using Bjma.Test.UI.NUnitConstraints;

namespace Bjma.Test.UI
{
    /// <summary>
    ///   Responsible for encapsulating extension methods 
    ///   for performing assertions on PageObjectElements 
    ///   that can be checked (check-boxes, radio buttons).
    /// </summary>
    public static class CheckableElement
    {
        public static CheckableElementIsCheckedConstraint<TPage> IsChecked<TPage>()
            where TPage : IPageObject<TPage>, new()
        {
            return new CheckableElementIsCheckedConstraint<TPage>();
        }

        public static CheckableElementIsNotCheckedConstraint<TPage> IsNotChecked<TPage>()
            where TPage : IPageObject<TPage>, new()
        {
            return new CheckableElementIsNotCheckedConstraint<TPage>();
        }
    }
}