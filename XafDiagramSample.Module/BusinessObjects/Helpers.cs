namespace XafDiagramSample.Module.BusinessObjects
{
    public static class Helpers
    {
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                source.Add(item);
            }
        }



        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Argument 'source' must not be null.");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action", "Argument 'action' must not be null.");
            }

            foreach (TSource item in source)
            {
                action(item);
            }
        }
    }
}
