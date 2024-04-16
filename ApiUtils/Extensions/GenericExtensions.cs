using ApiUtils.Resources;
using FluentValidation;
using FluentValidation.Results;

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

        /// <summary>
        /// Generates prevalidation of input models to define if are null at the start
        /// </summary>
        /// <typeparam name="T">Model data type</typeparam>
        /// <param name="value">Validation context</param>
        /// <param name="validationResult">Validation result to add erros if model instance to validate is null</param>
        /// <returns><see cref="true"/> is model instance to validate is null, <see cref="false"/> otherwise</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="value"/> or <paramref name="validationResult"/> are null</exception>
        public static bool Prevalidate<T>(this ValidationContext<T>? value, ValidationResult? validationResult)
        {
            ArgumentNullException.ThrowIfNull(argument: value, paramName: nameof(value));
            ArgumentNullException.ThrowIfNull(argument: validationResult, paramName: nameof(validationResult));

            if (value.InstanceToValidate is null)
            {
                validationResult.Errors.Add(item: new ValidationFailure("Input model", ErrorMessageRuleValidatorResource.PreValidate));
                return false;
            }

            return true;

        }

    }
}
