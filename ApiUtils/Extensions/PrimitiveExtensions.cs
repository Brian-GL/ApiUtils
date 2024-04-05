using ApiUtils.Exceptions;
using Cysharp.Text;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiUtils.Extensions
{
    /// <summary>
    /// Primitive data type extensions class
    /// </summary>
    public static partial class PrimitiveExtensions
    {
        #region Byte Array

        /// <summary>
        /// Converts any <see cref="byte"/> array to <see cref="string"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="string"/></param>
        /// <returns>New <see cref="byte"/> array from <paramref name="value"/></returns>
        /// <exception cref="DecoderFallbackException">If <paramref name="value"/> could not be converted to <see cref="string"/> value</exception>
        public static string ToStringValue(this byte[]? value) => value is null ? string.Empty : Encoding.UTF8.GetString(bytes: value!);

        #endregion

        #region String

        /// <summary>
        /// Returns new non nullable <see cref="string"/> value from any nullable <see cref="string"/>
        /// </summary>
        /// <param name="value">Value to get non nullable value</param>
        /// <param name="replacement">Value to return in case <paramref name="value"/> is null, empty or full with white spaces</param>
        /// <returns>New non nullable <see cref="string"/> value</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="replacement"/> is null</exception>
        public static string Available(this string? value, string replacement = "")
        {
            ArgumentNullException.ThrowIfNull(argument: replacement, paramName: nameof(replacement));
            return value.IsNullEmptyOrBlank() ? replacement : value!;
        } 

        /// <summary>
        /// Capitalizes nullable <see cref="string"/> value
        /// </summary>
        /// <param name="value">Value to capitalize</param>
        /// <returns>New capitalized <see cref="string"/> value. Otherwise <see cref="string.Empty"/> if <paramref name="value"/> is null, empty or full with white spaces</returns>
        public static string Capitalize(this string? value) => value.IsNullOrEmpty() ? string.Empty : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str: value!.ToLower());

        /// <summary>
        /// Hash any <see cref="string"/> value using <see cref="SHA512"/> algorithm
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>Hased string if <paramref name="value"/> is not null, empty or full with white spaces. Otherwise <see cref="string.Empty"/></returns>
        /// <exception cref="TargetInvocationException">If <see cref="SHA512"/> algorithm could not be created well</exception>
        public async static Task<string> HashAsync(this string? value)
        {
            if (value.IsNullOrEmpty()) 
                return string.Empty;

            string hashString = string.Empty;

            using(SHA512 shaAlgorithm = SHA512.Create())
            {
                using MemoryStream memoryStream = new(buffer: value.ToByteArray());
                byte[] hashArray = await shaAlgorithm.ComputeHashAsync(inputStream: memoryStream);
                hashString = hashArray.ToStringValue();
            }

            return hashString;
        }

        /// <summary>
        /// Validates if any <see cref="string"/> is match from regex pattern
        /// </summary>
        /// <param name="value">Value to validate match</param>
        /// <param name="pattern">Regex pattern</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is match from regex <paramref name="pattern"/>, <see cref="false"/> otherwise</returns>
        /// <exception cref="ArgumentException">If <paramref name="pattern"/> has invalid regex pattern format</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="pattern"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="pattern"/> has invalid regex pattern format</exception>
        public static bool IsMatch(this string? value, string pattern)
        {
            ArgumentNullException.ThrowIfNull(argument: pattern, paramName: nameof(pattern));

            Regex regularExpression = new(pattern: pattern, options: RegexOptions.Compiled);
            return !value.IsNullOrEmpty() && regularExpression.IsMatch(input: value!);
        }

        /// <summary>
        /// Validates if any <see cref="string"/> is match from regex
        /// </summary>
        /// <param name="value">Value to validate match</param>
        /// <param name="regularExpression">Regex</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is match from <paramref name="regularExpression"/>, <see cref="false"/> otherwise</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="pattern"/> is null</exception>
        public static bool IsMatch(this string? value, Regex regularExpression)
        {
            ArgumentNullException.ThrowIfNull(argument: regularExpression, paramName: nameof(regularExpression));
            return !value.IsNullOrEmpty() && regularExpression.IsMatch(input: value!);
        }

        /// <summary>
        /// Validates if any <see cref="string"/> is null, empty or full with white spaces
        /// </summary>
        /// <param name="value">Value to validate</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is is null, empty or full with white spaces, <see cref="false"/> otherwise</returns>
        public static bool IsNullEmptyOrBlank(this string? value)
        {
            if (value is null)
                return true;

            foreach (char c in value!)
            {
                if (!char.IsWhiteSpace(c))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Validates if any <see cref="string"/> is null or empty
        /// </summary>
        /// <param name="value">Value to validate</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is is null or empty, <see cref="false"/> otherwise</returns>
        public static bool IsNullOrEmpty(this string? value)
        {
            if (value is null)
                return true;

            return value!.Length < 1;
        }

        /// <summary>
        /// Defines a SQL <a href="https://www.w3schools.com/sql/sql_like.asp">like</a> function to search for a specified <paramref name="pattern"/> in <paramref name="value"/>
        /// </summary>
        /// <param name="value">Value to find <paramref name="pattern"/> coincidence inside it</param>
        /// <param name="pattern">Coincidence to find. To find you can use the character '*' to specify any caracter previous or next from the coincidence to find</param>
        /// <returns><see cref="true"/> if <paramref name="pattern"/> coincidence is found in <paramref name="value"/>. <see cref="false"/> otherwise</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="pattern"/> is null</exception>
        /// <exception cref="ArgumentFormatException">If <paramref name="pattern"/> has an invalid format value</exception>
        /// <exception cref="ArgumentException">If <paramref name="pattern"/> could not be used for like operation</exception>
        /// <exception cref="RegexMatchTimeoutException">If <paramref name="pattern"/> could not be used for match operation</exception>
        public static bool Like(this string? value, string pattern)
        {
            ArgumentNullException.ThrowIfNull(argument: pattern, paramName: nameof(pattern));
            ArgumentFormatException.ThrowIfInvalid(argument: pattern, regularExpression: LikeRegex());

            if (value.IsNullOrEmpty())
                return false;

            // Generate the new regex expression to make like function.

            GroupCollection groupCollection = LikeGroupRegex().Match(input: pattern).Groups;
            IEnumerable<string> keys = groupCollection.Keys;
            using Utf8ValueStringBuilder stringBuilder = new();

            foreach(string key in keys)
            {
                switch (key)
                {
                    case "first":
                    case "second":
                        stringBuilder.Append(value: ".+");
                        break;
                    case "coincidence":
                        stringBuilder.Append(value: groupCollection["coincidence"]);
                        break;
                    default:
                        break;
                }
            }

            Regex regularExpression = new(pattern: stringBuilder.ToString(), options: RegexOptions.Compiled);
            return regularExpression.IsMatch(input: value!);
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="Array"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="Array"/> value</param>
        /// <returns>New <see cref="Array"/> of <see cref="char"/> values from <paramref name="value"/>. If <paramref name="value"/> is empty or null it will be returned and empty collection</returns>
        public static char[] ToArray(this string? value)
        {
            if (!value.IsNullOrEmpty())
            {
                int index = 0;
                char[] array = new char[value!.Length];

                foreach (char c in value!)
                {
                    array[index] = c;
                    index++;
                }

                return array;
            }

            return [];
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="byte"/> array
        /// </summary>
        /// <param name="value">Value to convert to <see cref="byte"/> array</param>
        /// <returns>New <see cref="byte"/> array from <paramref name="value"/></returns>
        /// <exception cref="EncoderFallbackException">If <paramref name="value"/> could not be converted to <see cref="byte"/> array</exception>
        public static byte[] ToByteArray(this string? value) => value.IsNullOrEmpty() ? [] : Encoding.UTF8.GetBytes(s: value!);

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="DateTime"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="DateTime"/> value</param>
        /// <param name="dateTimeFormat">Datetime format. Example:  dd/MM/yyyy HH:mm:ss</param>
        /// <returns>New <see cref="DateTime"/> value from <see cref="string"/> <paramref name="value"/> if it has a valid date time format. <see cref="DateTime.MinValue"/> otherwise</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="dateTimeFormat"/> is null</exception>
        /// <exception cref="ArgumentException">If <paramref name="dateTimeFormat"/> has invalid format</exception>
        public static DateTime ToDateTime(this string? value, string dateTimeFormat = "dd/MM/yyyy HH:mm:ss")
        {
            ArgumentNullException.ThrowIfNull(argument: dateTimeFormat, paramName: nameof(dateTimeFormat));

            if (value.IsNullEmptyOrBlank())
                return DateTime.MinValue;

            bool isCasted = DateTime.TryParseExact(s: value, format: dateTimeFormat, provider: CultureInfo.InvariantCulture, style: DateTimeStyles.None, result: out DateTime dateTimeValue);
            return isCasted ? dateTimeValue : DateTime.MinValue;
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="decimal"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="decimal"/> value</param>
        /// <returns>New <see cref="int"/> value from <see cref="string"/> <paramref name="value"/> if it has a valid number format. <see cref="decimal.MinValue"/> otherwise</returns>
        public static decimal ToDecimal(this string? value)
        {
            if (value.IsNullEmptyOrBlank())
                return decimal.MinValue;

            bool isCasted = decimal.TryParse(s: value, result: out decimal decimalValue);
            return isCasted ? decimalValue : decimal.MinValue;
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="double"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="double"/> value</param>
        /// <returns>New <see cref="int"/> value from <see cref="string"/> <paramref name="value"/> if it has a valid number format. <see cref="double.MinValue"/> otherwise</returns>
        public static double ToDouble(this string? value)
        {
            if (value.IsNullEmptyOrBlank())
                return double.MinValue;

            bool isCasted = double.TryParse(s: value, result: out double doubleValue);
            return isCasted ? doubleValue : double.MinValue;
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="float"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="float"/> value</param>
        /// <returns>New <see cref="int"/> value from <see cref="string"/> <paramref name="value"/> if it has a valid number format. <see cref="float.MinValue"/> otherwise</returns>
        public static float ToFloat(this string? value)
        {
            if (value.IsNullEmptyOrBlank())
                return float.MinValue;

            bool isCasted = float.TryParse(s: value, result: out float floatValue);
            return isCasted ? floatValue : float.MinValue;
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="int"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="int"/> value</param>
        /// <returns>New <see cref="int"/> value from <see cref="string"/> <paramref name="value"/> if it has a valid number format. <see cref="int.MinValue"/> otherwise</returns>
        public static int ToInt(this string? value)
        {
            if (value.IsNullEmptyOrBlank())
                return int.MinValue;

            bool isCasted = int.TryParse(s: value, result: out int intValue);
            return isCasted ? intValue : int.MinValue;
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="LinkedList{T}"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="LinkedList{T}"/> value</param>
        /// <returns>New <see cref="LinkedList{T}"/> of <see cref="char"/> values from <paramref name="value"/>. If <paramref name="value"/> is empty or null it will be returned and empty collection</returns>
        public static LinkedList<char> ToLinkedList(this string? value)
        {
            LinkedList<char> linkedList = new();

            if (!value.IsNullEmptyOrBlank())
            {
                foreach (char c in value!)
                    linkedList.AddLast(value: c);
            }

            return linkedList;
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="List{T}"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="List{T}"/> value</param>
        /// <returns>New <see cref="List{T}"/> of <see cref="char"/> values from <paramref name="value"/>. If <paramref name="value"/> is empty or null it will be returned and empty collection</returns>
        public static List<char> ToList(this string? value)
        {
            List<char> list = [];

            if (!value.IsNullOrEmpty())
            {
                foreach (char c in value!)
                    list.Add(item: c);
            }

            return list;
        }

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="long"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="long"/> value</param>
        /// <returns>New <see cref="long"/> value from <see cref="string"/> <paramref name="value"/> if it has a valid number format. <see cref="long.MinValue"/> otherwise</returns>
        public static long ToLong(this string? value)
        {
            if (value.IsNullEmptyOrBlank())
                return long.MinValue;

            bool isCasted = long.TryParse(s: value, result: out long longValue);
            return isCasted ? longValue : long.MinValue;
        }

        /// <summary>
        /// Creates a copy of this <see cref="string"/> in lower case. The culture is set by culture.
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>New lowercase <see cref="string"/> value</returns>
        public static string ToLowercase(this string? value) => value.IsNullOrEmpty() ? string.Empty : value!.ToLower();

        /// <summary>
        /// Converts any <see cref="string"/> to <see cref="short"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="short"/> value</param>
        /// <returns>New <see cref="short"/> value from <see cref="string"/> <paramref name="value"/> if it has a valid number format. <see cref="short.MinValue"/> otherwise</returns>
        public static short ToShort(this string? value)
        {
            if (value.IsNullEmptyOrBlank())
                return short.MinValue;

            bool isCasted = short.TryParse(s: value, result: out short shortValue);
            return isCasted ? shortValue : short.MinValue;
        }

        /// <summary>
        /// Creates a copy of this <see cref="string"/> in upper case. The culture is set by culture.
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>New uppercase <see cref="string"/> value</returns>
        public static string ToUppercase(this string? value) => value.IsNullOrEmpty() ? string.Empty : value!.ToUpper();

        /// <summary>
        /// Removes diacriticts from any <see cref="string"/> value
        /// </summary>
        /// <param name="value">Value to remove all diacritics</param>
        /// <returns>New <see cref="string"/> value without diacritics from <paramref name="value"/></returns>
        public static string WithoutDiacritics(this string? value)
        {
            if (value.IsNullOrEmpty())
                return string.Empty;

            string normalize = value!.Normalize(normalizationForm: NormalizationForm.FormD);
            using Utf8ValueStringBuilder builder = new();

            foreach(char c in normalize)
            {
                if (!CharUnicodeInfo.GetUnicodeCategory(ch: c).Equals(obj: UnicodeCategory.NonSpacingMark))
                    builder.Append(value: c);
            }

            string builderResult = builder.ToString();

            normalize = builderResult.Normalize(normalizationForm: NormalizationForm.FormC);

            return normalize;

        }



        #endregion

        #region Generated Regex

        [GeneratedRegex(pattern: @"^((?<first>[*]{0,1}))?((?<coincidence>.+))?((?<second>[*]{0,1}))?$", options: RegexOptions.Compiled)]
        private static partial Regex LikeGroupRegex();


        [GeneratedRegex(pattern: @"^[*]{0,1}.+[*]{0,1}$", options: RegexOptions.Compiled)]
        private static partial Regex LikeRegex();

        #endregion

    }
}
