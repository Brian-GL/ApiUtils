using ApiUtils.Resources;
using FluentValidation;
using FluentValidation.Validators;
using GTranslate;

namespace ApiUtils.RuleValidators
{
    /// <summary>
    /// Creates new rule validator for validations on target language to translate
    /// </summary>
    /// <typeparam name="T">Input model data type</typeparam>
    public class LanguageRuleValidator<T> : PropertyValidator<T, string?>
    {
        public override string Name => RuleValidatorNameResource.LanguageRuleValidator;

        public override bool IsValid(ValidationContext<T> context, string? value)
            => value is null || Language.TryGetLanguage(code: value!, out _);

        protected override string GetDefaultMessageTemplate(string errorCode)
            => ErrorMessageRuleValidatorResource.LanguageRuleValidator;

    }
}
