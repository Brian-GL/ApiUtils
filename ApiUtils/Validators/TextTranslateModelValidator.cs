using ApiUtils.Extensions;
using ApiUtils.Models;
using FluentValidation;
using FluentValidation.Results;

namespace ApiUtils.Validators
{
    /// <summary>
    /// Fluent validation model class to validate <see cref="TextTranslateModel"/>
    /// </summary>
    internal class TextTranslateModelValidator : AbstractValidator<TextTranslateModel>
    {
        /// <summary>
        /// Creates new Fluent validation model class to validate <see cref="TextTranslateModel"/>
        /// </summary>
        public TextTranslateModelValidator()
        {
            RuleFor(x => x.Text).Cascade(cascadeMode: CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.ToLanguage).Cascade(cascadeMode: CascadeMode.Stop).NotNull().NotEmpty().NotLanguage();
        }

        protected override bool PreValidate(ValidationContext<TextTranslateModel> context, ValidationResult result) 
            => context.Prevalidate(validationResult: result);
    }
}
