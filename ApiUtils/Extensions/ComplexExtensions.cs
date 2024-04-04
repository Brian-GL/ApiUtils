using System.Collections;

namespace ApiUtils.Extensions
{
    /// <summary>
    /// Complex data type extensions class
    /// </summary>
    public static class ComplexExtensions
    {

        #region ICollection

        /// <summary>
        /// Defines if any <see cref="ICollection"/> is empty or null
        /// </summary>
        /// <param name="value">Value to validate if is empty or null</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is empty or null. <see cref="false"/> otherwise</returns>
        public static bool IsEmpty(this ICollection? value)
        {
            if (value is null)
                return true;

            return value!.Count.Equals(obj: 0);
        }

        #endregion

        #region IEnumerable

        /// <summary>
        /// Defines if any <see cref="IEnumerable"/> is empty or null
        /// </summary>
        /// <param name="value">Value to validate if is empty or null</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is empty or null. <see cref="false"/> otherwise</returns>
        public static bool IsEmpty(this IEnumerable? value)
        {
            if (value is null)
                return true;

            IEnumerator enumerator = value!.GetEnumerator();
            using (enumerator as IDisposable)
                return !enumerator.MoveNext();
        }

        #endregion
    }
}
