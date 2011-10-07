// ReSharper disable InconsistentNaming
namespace NPageObject.NUnitConstraints
{
    using NUnit.Framework.Constraints;

    public abstract class UITestConstraintBase<T> : Constraint
        where T : IPageObject<T>, new()
    {
        protected IUITestContext<T> UITestContext { get; set; }

        public abstract override bool Matches(object actual);

        public abstract override void WriteDescriptionTo(MessageWriter writer);
    }
}
// ReSharper restore InconsistentNaming