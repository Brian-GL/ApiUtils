using System.Collections;

namespace ApiUtils.Extensions
{
    /// <summary>
    /// <see cref="ICollection"/> extensions' functions class
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Defines if any <see cref="ICollection"/> is empty or null
        /// </summary>
        /// <param name="value">Value to validate if is empty or null</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is empty or null. <see cref="false"/> otherwise</returns>
        public static bool IsNullOrEmpty(this ICollection? value)
        {
            if (value is null)
                return true;

            return value!.Count.Equals(obj: 0);
        }

    }
}
