using System.Text;

namespace ApiUtils.Extensions
{
    /// <summary>
    /// <see cref="byte"/> extensions' functions class
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts any <see cref="byte"/> array to <see cref="string"/> value
        /// </summary>
        /// <param name="value">Value to convert to <see cref="string"/></param>
        /// <returns>New <see cref="byte"/> array from <paramref name="value"/></returns>
        /// <exception cref="DecoderFallbackException">If <paramref name="value"/> could not be converted to <see cref="string"/> value</exception>
        public static string ToStringValue(this byte[]? value) => value is null ? string.Empty : Encoding.UTF8.GetString(bytes: value!);

    }
}
