namespace ApiUtils.Collections.Abstract
{
    /// <summary>
    /// Interface for optimized list classes
    /// </summary>
    /// <typeparam name="T">Listing data types</typeparam>
    public interface IListing<T>
    {
        /// <summary>
        /// Adds new items from the front
        /// </summary>
        /// <param name="items">Items to add</param>
        void AddToFront(params T?[]? items);

        /// <summary>
        /// Adds new items from the front and applies them a predicate function
        /// </summary>
        /// <param name="items">Items to add</param>
        /// <param name="func">Predicate function to apply</param>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> or <paramref name="func"/> is null or empty</exception>
        void AddToFront(Func<T?, T?>? func, params T?[] items);

        /// <summary>
        /// Adds new items from the front using an <see cref="IEnumerable{T}"/> source
        /// </summary>
        /// <param name="items">Items to add</param>
        void AddToFront(IEnumerable<T>? items);

        /// <summary>
        /// Adds new item from the front and applies it a predicate function
        /// </summary>
        /// <param name="item"></param>
        /// <param name="func">Predicate function to apply</param>
        void AddToFront(T? item, Func<T?, T?> func);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void AddToBack(T? item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="func"></param>
        void AddToBack(T? item, Func<T?, T?> func);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void AddToCenter(T? item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="func"></param>
        void AddToCenter(T? item, Func<T?, T?> func);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T? Dequeue();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Func<T?, bool> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        void ForEach(Action<T?> action);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int? IndexAt(T? item);

        /// <summary>
        /// 
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// 
        /// </summary>
        int MiddleIndex { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T? this[int index] {get; set;}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool RemoveFirst();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool RemoveLast();

        /// <summary>
        /// 
        /// </summary>
        void Reverse();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T? Unstack();

    }
}
