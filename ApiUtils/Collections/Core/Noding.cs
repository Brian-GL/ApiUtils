using ApiUtils.Collections.Abstract;

namespace ApiUtils.Collections.Core
{
    /// <summary>
    /// Node used for List collections, it defines a completed element for unimensional collections
    /// </summary>
    /// <typeparam name="T">Noding data type value</typeparam>
    public class Noding<T> : INoding<T>, IEquatable<Noding<T>>
    {
        /// <summary>
        /// The <see cref="Noding{T}"/> value
        /// </summary>
        private T? _value;

        /// <summary>
        /// The previous <see cref="Noding{T}"/> reference value
        /// </summary>
        private Noding<T>? _previous;

        /// <summary>
        /// The next <see cref="Noding{T}"/> reference value
        /// </summary>
        private Noding<T>? _next;

        /// <summary>
        /// Creates new node used for List collections and references it with null previous and next nodes
        /// </summary>
        /// <param name="value">The <see cref="Noding{T}"/> value</param>
        public Noding(T? value) => (_previous, _value, _next) = (null, value, null);

        /// <summary>
        /// Creates new node used for List collections
        /// </summary>
        /// <param name="value">The <see cref="Noding{T}"/> value</param>
        /// <param name="previous">The previous <see cref="Noding{T}"/> reference value</param>
        /// <param name="next">The next <see cref="Noding{T}"/> reference value</param>
        public Noding(T? value, Noding<T>? previous, Noding<T>? next) => (_previous, _value, _next) = (previous, value, next);

        public bool Equals(Noding<T>? other)
        {
            if (other is null)
                return false;

            return HasValue ? _value!.Equals(obj: other!.Value) : !other.HasValue;
        }

        public override bool Equals(object? obj) => Equals(obj as Noding<T>);

        public override int GetHashCode() => HasValue ? _value!.GetHashCode() : 0;

        public T GetValueOrDefault() => HasValue ? _value! : default!;

        public T GetValueOrDefault(T value)
        {
            ArgumentNullException.ThrowIfNull(argument: value, paramName: nameof(value));
            return HasValue ? _value! : value;
        }

        public bool HasNext => _next is not null;

        public bool HasPrevious => _previous is not null;

        public bool HasValue => _value is not null;

        public T? Value { get => _value; set => _value = value; }

        public Noding<T>? Next { get => _next; set => _next = value; }

        public Noding<T>? Previous { get => _previous; set => _previous = value; }
    }
}
