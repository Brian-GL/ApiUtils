namespace ApiUtils.Collections.Core
{
    /// <summary>
    /// Node used for List collections, it defines a completed element for unimensional collections
    /// </summary>
    /// <typeparam name="T">Noding data type value</typeparam>
    public class Noding<T> : IEquatable<Noding<T>>
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

        /// <summary>
        /// Gets the default value from <see cref="Noding{T}.Value"/> if this is null
        /// </summary>
        /// <returns><see cref="default"/> value of <typeparamref name="T"/> if <see cref="Noding{T}.Value"/> is null. Otherwise <see cref="Noding{T}.Value"/></returns>
        public T GetValueOrDefault() => HasValue ? _value! : default!;

        /// <summary>
        /// Gets the default value from <see cref="Noding{T}.Value"/> if this is null
        /// </summary>
        /// <param name="value">Value to get if <see cref="Noding{T}.Value"/> is null</param>
        /// <returns><paramref name="value"/> of <typeparamref name="T"/> if <see cref="Noding{T}.Value"/> is null. Otherwise <see cref="Noding{T}.Value"/></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is null</exception>
        public T GetValueOrDefault(T value)
        {
            ArgumentNullException.ThrowIfNull(argument: value, paramName: nameof(value));
            return HasValue ? _value! : value;
        }

        /// <summary>
        /// Defines if this <see cref="Noding{T}"/> has a next <see cref="Noding{T}"/> reference value
        /// </summary>
        public bool HasNext => _next is not null;

        /// <summary>
        /// Defines if this <see cref="Noding{T}"/> has a previous <see cref="Noding{T}"/> reference value
        /// </summary>
        public bool HasPrevious => _previous is not null;

        /// <summary>
        /// Defines if this <see cref="Noding{T}"/> has a not null value 
        /// </summary>
        public bool HasValue => _value is not null;

        /// <summary>
        /// Gets and sets the <see cref="Noding{T}"/> value
        /// </summary>
        public T? Value { get => _value; set => _value = value; }

        /// <summary>
        /// Gets and sets the next <see cref="Noding{T}"/> reference value
        /// </summary>
        public Noding<T>? Next { get => _next; set => _next = value; }

        /// <summary>
        /// Gets and sets the previous <see cref="Noding{T}"/> reference value
        /// </summary>
        public Noding<T>? Previous { get => _previous; set => _previous = value; }

    }
}
