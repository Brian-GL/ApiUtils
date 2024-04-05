using ApiUtils.Extensions;
using Cysharp.Text;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ApiUtils.Exceptions
{
    /// <summary>
    /// Exception to generate when <paramref name="paramName"/> has an invalid format
    /// </summary>
    /// <param name="paramName">Param name witch generates exception</param>
    public class ArgumentFormatException(string? paramName) : SystemException(message: "Invalid value format")
    {
        public virtual string? ParamName => paramName;

        public override string Message
        {
            get
            {
                string? s = base.Message;
                return !s.IsNullEmptyOrBlank() ? ZString.Concat(s, Environment.NewLine, "Param name: ", paramName.Available(replacement: "Unknown")) : s;
            }
        }

        /// <summary>Throws an <see cref="ArgumentFormatException"/> if <paramref name="argument"/> has an invalid format based on <paramref name="pattern"/>.</summary>
        /// <param name="argument">The reference type argument to validate as non-null.</param>
        /// <param name="pattern">Regex pattern</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
        public static void ThrowIfInvalid(string? argument, string pattern, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
                Throw(paramName: paramName);

            if (pattern is null)
                Throw(paramName: nameof(pattern));

            Regex regularExpression = new(pattern: pattern, options: RegexOptions.Compiled);

            if(!regularExpression.IsMatch(input: argument))
                Throw(paramName: paramName);
        }

        /// <summary>Throws an <see cref="ArgumentFormatException"/> if <paramref name="argument"/> has an invalid format based on <paramref name="regularExpression"/>.</summary>
        /// <param name="argument">The reference type argument to validate as non-null.</param>
        /// <param name="regularExpression">Regular expression</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
        public static void ThrowIfInvalid(string? argument, Regex regularExpression, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
                Throw(paramName: paramName);

            if (regularExpression is null)
                Throw(paramName: nameof(regularExpression));

            if (!regularExpression.IsMatch(input: argument))
                Throw(paramName: paramName);
        }

        /// <summary>
        /// Generates <see cref="ArgumentFormatException"/> throw exception
        /// </summary>
        /// <param name="paramName">Param name witch generates exception</param>
        /// <exception cref="ArgumentFormatException">Exception to generate when <paramref name="paramName"/> has an in valid value format</exception>
        [DoesNotReturn]
        internal static void Throw(string? paramName) => throw new ArgumentFormatException(paramName: paramName);
    }
}
