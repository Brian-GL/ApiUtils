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
        /// <returns><see cref="true"/> if <paramref name="value"/> is equal to any value in <paramref name="values"/></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="values"/> is null, considering <paramref name="value"/> is not null. Otherwise <see cref="false"/></exception>
        public static bool In<T>(this T? value, params T?[] values)
        {
            ArgumentNullException.ThrowIfNull(argument: values, paramName: nameof(values));

            if (value is not null)
            {
                foreach (T? val in values)
                {
                    if (value!.Equals(obj: val))
                        return true;
                }
            }

            return false;

        }

    }
}
