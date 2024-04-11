using ApiUtils.Collections.Abstract;
using ApiUtils.Collections.Core;
using System.Collections;

namespace ApiUtils.Collections
{
    /// <summary>
    /// Optimized list class
    /// </summary>
    /// <typeparam name="T">Listing data types</typeparam>
    public class Listing<T> : IListing<T>, ICollection<T?>, IEquatable<Listing<T>>
    {
        /// <summary>
        /// Number of elements in <see cref="Listing{T}"/>
        /// </summary>
        private int _count;

        /// <summary>
        /// First <see cref="Listing{T}"/> <see cref="Noding{T}"/> (HEAD)
        /// </summary>
        private Noding<T>? _front;

        /// <summary>
        /// Middle <see cref="Listing{T}"/> <see cref="Noding{T}"/> (CENTER)
        /// </summary>
        private Noding<T>? _middle;

        /// <summary>
        /// Last <see cref="Listing{T}"/> <see cref="Noding{T}"/> (TAIL)
        /// </summary>
        private Noding<T>? _back;

        /// <summary>
        /// Creates new empty <see cref="Listing{T}"/>
        /// </summary>
        public Listing() => (_count, _front, _middle, _back) = (0, null, null, null);

        public void Add(T? item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T? item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T?[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count => _count;

        public bool Equals(Listing<T>? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj) => Equals(obj as Listing<T>);


        public bool IsReadOnly => false;

        public bool IsEmpty => throw new NotImplementedException();

        public int MiddleIndex => throw new NotImplementedException();

        public T? this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
            
        }

        public bool Remove(T? item)
        {
            throw new NotImplementedException();
        }

        public void AddToFront(T? item)
        {
            throw new NotImplementedException();
        }

        public void AddToFront(T? item, Func<T?, T?> func)
        {
            throw new NotImplementedException();
        }

        public void AddToBack(T? item)
        {
            throw new NotImplementedException();
        }

        public void AddToBack(T? item, Func<T?, T?> func)
        {
            throw new NotImplementedException();
        }

        public void AddToCenter(T? item)
        {
            throw new NotImplementedException();
        }

        public void AddToCenter(T? item, Func<T?, T?> func)
        {
            throw new NotImplementedException();
        }

        public T? Dequeue()
        {
            throw new NotImplementedException();
        }

        public bool Exists(Func<T?, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void ForEach(Action<T?> action)
        {
            throw new NotImplementedException();
        }

        public int? IndexAt(T? item)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFirst()
        {
            throw new NotImplementedException();
        }

        public bool RemoveLast()
        {
            throw new NotImplementedException();
        }

        public void Reverse()
        {
            throw new NotImplementedException();
        }

        public T? Unstack()
        {
            throw new NotImplementedException();
        }

        public void AddToFront(params T?[]? items)
        {
            throw new NotImplementedException();
        }

        public void AddToFront(Func<T?, T?>? func, params T?[] items)
        {
            throw new NotImplementedException();
        }

        public void AddToFront(IEnumerable<T>? items)
        {
            throw new NotImplementedException();
        }
    }
}
