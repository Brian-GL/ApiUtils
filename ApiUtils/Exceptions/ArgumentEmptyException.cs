using ApiUtils.Extensions;
using Cysharp.Text;
using System.Collections;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ApiUtils.Exceptions
{
    /// <summary>
    /// Exception to generate when <paramref name="paramName"/> has an empty value
    /// </summary>
    /// <param name="paramName">Param name witch generates exception</param>
    [Serializable]
    public class ArgumentEmptyException(string? paramName) : SystemException(message: "Empty value")
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

        /// <summary>Throws an <see cref="ArgumentEmptyException"/> if <paramref name="argument"/> is empty.</summary>
        /// <param name="argument">The reference type argument to validate as non-null.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
        public static void ThrowIfEmpty(object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
                Throw(paramName: paramName);

            if (argument is string s && s.IsNullEmptyOrBlank())
                Throw(paramName: paramName);

            if (argument is IEnumerable e && e.IsNullOrEmpty())
                Throw(paramName: paramName);

            if(argument is DataTable d && d.IsNullOrEmpty())
                Throw(paramName: paramName);

            if (EqualityComparer<object>.Default.Equals(x: argument, y: default))
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
