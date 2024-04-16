using ApiUtils.Abstractions;
using ApiUtils.Validators;
using FluentValidation;

namespace ApiUtils.Models
{
    /// <summary>
    /// Model which defines text translation input params
    /// </summary>
    internal class TextTranslateModel : AbstractModel<TextTranslateModel>
    {
        /// <summary>
        /// Text to translate
        /// </summary>
        private readonly string? _text;

        /// <summary>
        /// Target language to translate
        /// </summary>
        private readonly string? _toLanguage;

        /// <summary>
        /// Creates new model which defines text translation input params
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <param name="toLangugage">Target language to translate</param>
        private TextTranslateModel(string? text, string? toLangugage)
            => (_text, _toLanguage) = (text, toLangugage);

        /// <summary>
        /// Gets the text to translate
        /// </summary>
        public string Text => _text!;

        /// <summary>
        /// Gets the Target language to translate
        /// </summary>
        public string ToLanguage => _toLanguage!;

        /// <summary>
        /// Validates and creates new model which defines text translation input params.  
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <param name="toLangugage">Target language to translate</param>
        /// <returns>New model</returns>
        /// <exception cref="ValidationException">If params are not valid using <see cref="TextTranslateModelValidator"/> validator</exception>
        public static async Task<TextTranslateModel> CreateAsync(string? text, string? toLangugage)
        {
            TextTranslateModel model = new(text: text, toLangugage: toLangugage);
            TextTranslateModelValidator validator = new();
            await validator.ValidateAndThrowAsync(instance: model);
            Trimming(instance: ref model);

            return model;
        }
    }
}
