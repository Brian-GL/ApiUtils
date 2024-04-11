using ApiUtils.Collections.Core;

namespace ApiUtils.Collections.Abstract
{
    /// <summary>
    /// Node interface used for List collections, it defines a completed element for unimensional collections
    /// </summary>
    /// <typeparam name="T">Noding data type value</typeparam>
    public interface INoding<T>
    {
        /// <summary>
        /// Gets a default Noding value if is null. Otherwise the non nullable Noding value
        /// </summary>
        /// <returns><see cref="default"/> value of <typeparamref name="T"/> if value is null. Otherwise the non nullable Noding value</returns>
        T GetValueOrDefault();

        /// <summary>
        /// Gets a default Noding value if is null. Otherwise the non nullable Noding value
        /// </summary>
        /// <param name="value">Value to get if Noding value is null</param>
        /// <returns><paramref name="value"/> of <typeparamref name="T"/> type if Noding value is null. Otherwise the non nullable Noding value</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is null</exception>
        T GetValueOrDefault(T value);

        /// <summary>
        /// Defines if this Noding has a next reference value
        /// </summary>
        bool HasNext { get; }

        /// <summary>
        /// Defines if this Noding has a previous reference value
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        /// Defines if this Noding has a not null value 
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        /// Gets and sets the Noding value
        /// </summary>
        T? Value { get; set; }

        /// <summary>
        /// Gets and sets the next Noding reference value
        /// </summary>
        Noding<T>? Next { get; set; }

        /// <summary>
        /// Gets and sets the previous Noding reference value
        /// </summary>
        Noding<T>? Previous { get; set; }

    }
}
