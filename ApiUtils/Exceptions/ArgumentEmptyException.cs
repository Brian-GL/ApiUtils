using ApiUtils.Extensions;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ApiUtils.Exceptions
{
    /// <summary>
    /// Exception to generate when <paramref name="paramName"/> has an empty value
    /// </summary>
    /// <param name="paramName">Param name witch generates exception</param>
    [Serializable]
    public class ArgumentEmptyException(string? paramName) : ArgumentException(message: "Empty value", paramName: paramName)
    {
        /// <summary>Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null.</summary>
        /// <param name="argument">The reference type argument to validate as non-null.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
        public static void ThrowIfEmpty(object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
                Throw(paramName: paramName);

            if (argument is string s && s.IsNullEmptyOrBlank())
                Throw(paramName: paramName);

            if (argument is ICollection col && col.IsEmpty())
                Throw(paramName: paramName);

            if (argument is IEnumerable e && e.IsEmpty())
                Throw(paramName: paramName);

            if(EqualityComparer<object>.Default.Equals(x: argument, y: default))
                Throw(paramName: paramName);
        }

        /// <summary>
        /// Generates <see cref="ArgumentEmptyException"/> throw exception
        /// </summary>
        /// <param name="paramName">Param name witch generates exception</param>
        /// <exception cref="ArgumentEmptyException">Exception to generate when <paramref name="paramName"/> has an empty value</exception>
        [DoesNotReturn]
        internal static void Throw(string? paramName) => throw new ArgumentEmptyException(paramName: paramName);

    }
}
