using System.Collections;

namespace ApiUtils.Extensions
{
    /// <summary>
    /// <see cref="IEnumerable"/> extensions' functions class
    /// </summary>
    public static class IEnumerableExtensions
    {

        /// <summary>
        /// Defines if any <see cref="IEnumerable"/> is empty or null
        /// </summary>
        /// <param name="value">Value to validate if is empty or null</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is empty or null. <see cref="false"/> otherwise</returns>
        public static bool IsNullOrEmpty(this IEnumerable? value)
        {
            if (value is null)
                return true;

            IEnumerator enumerator = value!.GetEnumerator();
            using (enumerator as IDisposable)
                return !enumerator.MoveNext();
        }
    }
}
