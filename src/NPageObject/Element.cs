using NPageObject.NUnitConstraints;

namespace NPageObject
{
    public static class Element
    {
        public static ElementTextContainsConstraint<TPage> TextContains<TPage>(string value)
            where TPage : IPageObject<TPage>, new()
        {
            return new ElementTextContainsConstraint<TPage>(value);
        }

        public static SelectedOptionIsConstraint<TPage> SelectedOptionIs<TPage>(string value)
            where TPage : IPageObject<TPage>, new()
        {
            return new SelectedOptionIsConstraint<TPage>(value);
        }

        public static TextFieldTextIsConstraint<TPage> TextIs<TPage>(string value)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldTextIsConstraint<TPage>(value);
        }

        public static TextFieldValueIsConstraint<TPage> ValueIs<TPage>(string value)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldValueIsConstraint<TPage>(value);
        }

        public static TextFieldValueIsNotConstraint<TPage> ValueIsNot<TPage>(string value)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldValueIsNotConstraint<TPage>(value);
        }

        public static TextFieldValueIsEmptyConstraint<TPage> ValueIsEmpty<TPage>()
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldValueIsEmptyConstraint<TPage>();
        }

        public static TextFieldValueIsNotEmptyConstraint<TPage> ValueIsNotEmpty<TPage>()
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldValueIsNotEmptyConstraint<TPage>();
        }

        public static TextFieldValueContainsConstraint<TPage> ValueContains<TPage>(string value)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldValueContainsConstraint<TPage>(value);
        }

        public static TextFieldValueDoesNotContainConstraint<TPage> ValueDoesNotContain<TPage>(string value)
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldValueDoesNotContainConstraint<TPage>(value);
        }

        public static TextFieldIsEnabledConstraint<TPage> IsEnabled<TPage>() where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldIsEnabledConstraint<TPage>();
        }

        public static TextFieldIsNotEnabledConstraint<TPage> IsNotEnabled<TPage>()
            where TPage : IPageObject<TPage>, new()
        {
            return new TextFieldIsNotEnabledConstraint<TPage>();
        }

        public static ElementIsVisibleConstraint<TPage> IsVisible<TPage>() where TPage : IPageObject<TPage>, new()
        {
            return new ElementIsVisibleConstraint<TPage>();
        }

        public static ElementIsNotVisibleConstraint<TPage> IsNotVisible<TPage>() where TPage : IPageObject<TPage>, new()
        {
            return new ElementIsNotVisibleConstraint<TPage>();
        }
    }
}