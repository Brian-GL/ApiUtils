using ApiUtils.Enums;
using ApiUtils.Extensions;
using ApiUtils.Strructs;
using GTranslate;
using GTranslate.Results;
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
                ILanguage detectedLanguage = await translator.DetectLanguageAsync(text: value!);
                return detectedLanguage.Name;
            }

            return "Unknown";
        }

        /// <summary>
        /// Gets the text speech <see cref="Stream"/> value from any <see cref="string"/> value using <a href="https://translate.google.com/">Google Translator</a>
        /// </summary>
        /// <param name="value">Value to get speech</param>
        /// <param name="toLanguage">Target language.You can choose one using <see cref="TextTranslationLanguage"/></param>
        /// <param name="translatorType">Translator type to use. In this case do not use <see cref="TextTranslatorType.Bing"/></param>
        /// <returns><see cref="Stream"/> speech. Otherwise <see cref="Stream.Null"/></returns>
        /// <remarks>DO NOT USE  <see cref="TextTranslatorType.Bing"/> for this method. If you use it anyways it will return <see cref="Stream.Null"/></remarks>
        /// <exception cref="ObjectDisposedException">Thrown when this translator has been disposed</exception>
        /// <exception cref="ArgumentNullException">Thrown when text, toLanguage or translator type are null</exception>
        /// <exception cref="ArgumentException">Thrown when a GTranslate.Language could not be obtained from toLanguage</exception>
        /// <exception cref="TranslatorException">Thrown when language is not supported or an error occurred during the operation</exception>
        public static async Task<Stream> TextToSpeechAsync(string? value, string? toLanguage, TextTranslatorType translatorType = TextTranslatorType.Google)
        {
            ArgumentNullException.ThrowIfNull(argument: translatorType, paramName: nameof(translatorType));

            if (!value.IsNullEmptyOrBlank() && toLanguage.IsNullEmptyOrBlank())
            {
                Stream speechStream;
                switch (translatorType)
                {
                    case TextTranslatorType.Google:
                        {
                            using GoogleTranslator2 translator = new();
                            speechStream = await translator.TextToSpeechAsync(text: value!, language: toLanguage!);
                        }
                        break;
                    case TextTranslatorType.Microsoft:
                        {
                            using MicrosoftTranslator translator = new();
                            speechStream = await translator.TextToSpeechAsync(text: value!, language: toLanguage!);
                        }
                        break;
                    case TextTranslatorType.Yandex:
                        {
                            using YandexTranslator translator = new();
                            speechStream = await translator.TextToSpeechAsync(text: value!, language: toLanguage!);
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

            return Stream.Null;
        }

        /// <summary>
        /// Translates any <see cref="string"/> value
        /// </summary>
        /// <param name="value">Value to translate</param>
        /// <param name="toLanguage">Target language.You can choose one using <see cref="TextTranslationLanguage"/></param>
        /// <param name="translatorType">Translator type to use</param>
        /// <returns>New <see cref="string"/> value translated</returns>
        /// <exception cref="ObjectDisposedException">Thrown when this translator has been disposed</exception>
        /// <exception cref="ArgumentNullException">Thrown when text, toLanguage or translator type are null</exception>
        /// <exception cref="ArgumentException">Thrown when a GTranslate.Language could not be obtained from toLanguage</exception>
        /// <exception cref="TranslatorException">Thrown when toLanguage is not supported or an error occurred during the operation</exception>
        public static async Task<string> TranslationAsync(string? value, string? toLanguage, TextTranslatorType translatorType = TextTranslatorType.Google)
        {
            ArgumentNullException.ThrowIfNull(argument: translatorType, paramName: nameof(translatorType));

            if (!value.IsNullEmptyOrBlank() && toLanguage.IsNullEmptyOrBlank())
            {
                ITranslator translator = GetTranslator(translatorType: translatorType);
                ITranslationResult result = await translator.TranslateAsync(text: value!, toLanguage: toLanguage!);
                return result.Translation;
            }

            return string.Empty;
        }

        /// <summary>
        /// Transliterates any <see cref="string"/> value using <a href="https://translate.google.com/">Google Translator</a>
        /// </summary>
        /// <param name="value">Value to transliterate</param>
        /// <param name="toLanguage">Target language.You can choose one using <see cref="TextTranslationLanguage"/></param>
        /// <returns>New <see cref="string"/> value transliterated</returns>
        /// <exception cref="ObjectDisposedException">Thrown when this translator has been disposed</exception>
        /// <exception cref="ArgumentNullException">Thrown when text or toLanguage are null</exception>
        /// <exception cref="ArgumentException">Thrown when a GTranslate.Language could not be obtained from toLanguage or fromLanguage</exception>
        /// <exception cref="GTranslate.TranslatorException">Thrown when toLanguage or fromLanguage are not supported, or an error occurred during the operation</exception>
        public static async Task<string> TransliterationGoogleAsync(string? value, string? toLanguage)
        {
            if (!value.IsNullEmptyOrBlank() && toLanguage.IsNullEmptyOrBlank())
            {
                using GoogleTranslator2 translator = new();
                GoogleTransliterationResult result = await translator.TransliterateAsync(text: value!, toLanguage: toLanguage!);
                return result.Transliteration;
            }

            return string.Empty;
        }
    }

}
