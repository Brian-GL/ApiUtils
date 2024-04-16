using ApiUtils.Enums;
using ApiUtils.Extensions;
using ApiUtils.Models;
using ApiUtils.Strructs;
using GTranslate;
using GTranslate.Results;
using FluentValidation;
using GTranslate.Translators;

namespace ApiUtils.Utilities
{
    /// <summary>
    /// Text translation utilities functions
    /// </summary>
    public static class TranslateUtilities
    {
        /// <summary>
        /// Gets the <see cref="ITranslator"/> from choosed <paramref name="translatorType"/> 
        /// </summary>
        /// <param name="translatorType">Translator type to use</param>
        /// <returns><see cref="ITranslator"/> to operate the translations</returns>
        private static ITranslator GetTranslator(TextTranslatorType translatorType)
            => translatorType switch
            {
                TextTranslatorType.Bing => new BingTranslator(),
                TextTranslatorType.Google => new GoogleTranslator2(),
                TextTranslatorType.Microsoft => new MicrosoftTranslator(),
                TextTranslatorType.Yandex => new YandexTranslator(),
                _ => new AggregateTranslator(),
            };

        /// <summary>
        /// Detects the language of any <see cref="string"/> value 
        /// </summary>
        /// <param name="value">Value to detect language</param>
        /// <param name="translatorType">Translator type to use</param>
        /// <returns>Detected language name from <paramref name="value"/>. Otherwise "Unknown" value</returns>
        /// <exception cref="ObjectDisposedException">Thrown when this translator has been disposed</exception>
        /// <exception cref="ArgumentNullException">Thrown when text or translator type are null</exception>
        /// <exception cref="TranslatorException">An error occurred during the operation</exception>
        public static async Task<string> DetectLanguageAsync(string? value, TextTranslatorType translatorType = TextTranslatorType.Google)
        {
            ArgumentNullException.ThrowIfNull(argument: translatorType, paramName: nameof(translatorType));

            if (!value.IsNullEmptyOrBlank())
            {
                ITranslator translator = GetTranslator(translatorType: translatorType);
                ILanguage detectedLanguage = await translator.DetectLanguageAsync(text: value!.Trim());
                return detectedLanguage.Name;
            }

            return "Unknown";
        }

        /// <summary>
        /// Gets the text speech <see cref="Stream"/> value from any <see cref="string"/> value using <a href="https://translate.google.com/">Google Translator</a>
        /// </summary>
        /// <param name="text">Value to get speech</param>
        /// <param name="toLanguage">Target language.You can choose one using <see cref="TranslateLanguage"/></param>
        /// <param name="translatorType">Translator type to use. In this case do not use <see cref="TextTranslatorType.Bing"/></param>
        /// <returns><see cref="Stream"/> speech. Otherwise <see cref="Stream.Null"/></returns>
        /// <remarks>DO NOT USE  <see cref="TextTranslatorType.Bing"/> for this method. If you use it anyways it will return <see cref="Stream.Null"/></remarks>
        /// <exception cref="ObjectDisposedException">Thrown when this translator has been disposed</exception>
        /// <exception cref="ArgumentNullException">Thrown when translator type is null</exception>
        /// <exception cref="TranslatorException">Thrown when language is not supported or an error occurred during the operation</exception>
        /// <exception cref="ValidationException">Thrown when <paramref name="text"/> or <paramref name="toLanguage"/> has no a valid format</exception>
        public static async Task<Stream> TextToSpeechAsync(string? text, string? toLanguage, TextTranslatorType translatorType = TextTranslatorType.Google)
        {
            ArgumentNullException.ThrowIfNull(argument: translatorType, paramName: nameof(translatorType));
            TextTranslateModel model = await TextTranslateModel.CreateAsync(text: text, toLangugage: toLanguage);

            Stream speechStream;
            switch (translatorType)
            {
                case TextTranslatorType.Google:
                    {
                        using GoogleTranslator2 translator = new();
                        speechStream = await translator.TextToSpeechAsync(text: model.Text, language: model.ToLanguage);
                    }
                    break;
                case TextTranslatorType.Microsoft:
                    {
                        using MicrosoftTranslator translator = new();
                        speechStream = await translator.TextToSpeechAsync(text: model.Text, language: model.ToLanguage);
                    }
                    break;
                case TextTranslatorType.Yandex:
                    {
                        using YandexTranslator translator = new();
                        speechStream = await translator.TextToSpeechAsync(text: model.Text, language: model.ToLanguage);
                    }
                    break;
                default:
                    {
                        speechStream = Stream.Null;
                    }
                    break;
            }

            return speechStream;
        }

        /// <summary>
        /// Translates any <see cref="string"/> value
        /// </summary>
        /// <param name="text">Value to translate</param>
        /// <param name="toLanguage">Target language.You can choose one using <see cref="TranslateLanguage"/></param>
        /// <param name="translatorType">Translator type to use</param>
        /// <returns>New <see cref="string"/> value translated</returns>        
        /// <exception cref="ObjectDisposedException">Thrown when this translator has been disposed</exception>
        /// <exception cref="ArgumentNullException">Thrown when translator type is null</exception>
        /// <exception cref="TranslatorException">Thrown when language is not supported or an error occurred during the operation</exception>
        /// <exception cref="ValidationException">Thrown when <paramref name="text"/> or <paramref name="toLanguage"/> has no a valid format</exception>
        public static async Task<string> TranslationAsync(string? text, string? toLanguage, TextTranslatorType translatorType = TextTranslatorType.Google)
        {
            ArgumentNullException.ThrowIfNull(argument: translatorType, paramName: nameof(translatorType));
            TextTranslateModel model = await TextTranslateModel.CreateAsync(text: text, toLangugage: toLanguage);

            ITranslator translator = GetTranslator(translatorType: translatorType);
            ITranslationResult result = await translator.TranslateAsync(text: model.Text, toLanguage: model.ToLanguage);
            return result.Translation;
        }

        /// <summary>
        /// Transliterates any <see cref="string"/> value using <a href="https://translate.google.com/">Google Translator</a>
        /// </summary>
        /// <param name="text">Value to transliterate</param>
        /// <param name="toLanguage">Target language.You can choose one using <see cref="TranslateLanguage"/></param>
        /// <returns>New <see cref="string"/> value transliterated</returns>
        /// <exception cref="TranslatorException">Thrown when language is not supported or an error occurred during the operation</exception>
        /// <exception cref="ValidationException">Thrown when <paramref name="text"/> or <paramref name="toLanguage"/> has no a valid format</exception>
        public static async Task<string> TransliterationGoogleAsync(string? text, string? toLanguage)
        {
            TextTranslateModel model = await TextTranslateModel.CreateAsync(text: text, toLangugage: toLanguage);

            using GoogleTranslator2 translator = new();
            GoogleTransliterationResult result = await translator.TransliterateAsync(text: model.Text, toLanguage: model.ToLanguage);
            return result.Transliteration;
        }
    }

}
