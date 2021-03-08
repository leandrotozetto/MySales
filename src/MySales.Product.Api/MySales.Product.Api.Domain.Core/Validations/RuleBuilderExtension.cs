using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using MySales.Product.Api.Domain.Core.Validations.Validators;

namespace MySales.Product.Api.Domain.Core.Validations
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T> Date<T>(this IRuleBuilder<T> ruleBuilder) where T : class
        {
            ruleBuilder.AddRule(DateValidator<T>.New(ruleBuilder));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> Email<T>(this IRuleBuilder<T> ruleBuilder) where T : class
        {
            ruleBuilder.AddRule(EmailValidator<T>.New(ruleBuilder));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> GreaterThanOrEqual<T>(this IRuleBuilder<T> ruleBuilder, dynamic valueToCompare) where T : class
        {
            ruleBuilder.AddRule(GreaterThanOrEqualValidator<T>.New(ruleBuilder, valueToCompare));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> GreaterThan<T>(this IRuleBuilder<T> ruleBuilder, dynamic valueToComare) where T : class
        {
            ruleBuilder.AddRule(GreaterThanValidator<T>.New(ruleBuilder, valueToComare));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> MaxLength<T>(this IRuleBuilder<T> ruleBuilder, int maxLength) where T : class
        {
            ruleBuilder.AddRule(MaxLengthValidator<T>.New(ruleBuilder, maxLength));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> Numeric<T>(this IRuleBuilder<T> ruleBuilder) where T : class
        {
            ruleBuilder.AddRule(NumericValidator<T>.New(ruleBuilder));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> Password<T>(this IRuleBuilder<T> ruleBuilder, bool hasNumber, bool hasUperCase, bool hasSpecialChar) where T : class
        {
            ruleBuilder.AddRule(PasswordValidator<T>.New(ruleBuilder, hasNumber, hasUperCase, hasSpecialChar));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> Required<T>(this IRuleBuilder<T> ruleBuilder) where T : class
        {
            ruleBuilder.AddRule(RequiredValidator<T>.New(ruleBuilder));

            return ruleBuilder;
        }

        public static IRuleBuilder<T> NotNull<T>(this IRuleBuilder<T> ruleBuilder) where T : class
        {
            ruleBuilder.AddRule(NotNullValidator<T>.New(ruleBuilder));

            return ruleBuilder;
        }
    }
}
