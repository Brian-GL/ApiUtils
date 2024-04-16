using ApiUtils.RuleValidators;
using FluentValidation;

namespace ApiUtils.Extensions
{
    /// <summary>
    /// Fluent validation rule validators extensions
    /// </summary>
    public static class RuleValidatorExtensions
    {
        /// <summary>
        /// Defines a language rule validator. Validation will fail if language code does not exists
        /// </summary>
        /// <typeparam name="T">Input model data type</typeparam>
        /// <param name="ruleBuilder">Rule builder</param>
        /// <returns>Rule builder new options from <see cref="LanguageRuleValidator{T}"/></returns>
        public static IRuleBuilderOptions<T, string?> NotLanguage<T>(this IRuleBuilder<T, string?> ruleBuilder)
            => ruleBuilder.SetValidator(validator: new LanguageRuleValidator<T>());
    }
}
