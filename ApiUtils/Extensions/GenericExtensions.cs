namespace ApiUtils.Extensions
{
    /// <summary>
    /// Extensions' functions for generic or template  types
    /// </summary>
    public static class GenericExtensions
    {

        /// <summary>
        /// Defines if <paramref name="value"/> is equal to any value in <paramref name="values"/>
        /// </summary>
        /// <typeparam name="T"><paramref name="value"/> data type</typeparam>
        /// <param name="value">Value to find in <paramref name="values"/></param>
        /// <param name="values">Seach values</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is equal to any value in <paramref name="values"/>. <see cref="false"/> otherwise</returns>
        public static bool In<T>(this T? value, params T?[]? values)
        {
            if (value is not null && values is not null)
            {
                foreach (T? val in values)
                {
                    if (value!.Equals(obj: val))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Defines if any array of <typeparamref name="T"/> if null or empty
        /// </summary>
        /// <typeparam name="T">Array data type</typeparam>
        /// <param name="value">Array to validate if is null or empty</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is null or empty. <see cref="false"/> otherwise</returns>
        public static bool IsNullOrEmpty<T>(this T?[]? value) => value is null || value?.Length < 1;
    }
}
