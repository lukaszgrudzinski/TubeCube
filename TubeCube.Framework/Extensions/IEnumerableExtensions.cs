using System.Collections.ObjectModel;

namespace TubeCube.Framework.Extensions;

public static class IEnumerableExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems, bool clearFirst = false)
    {
        if (clearFirst)
            collection.Clear();

        newItems.ForEach(item => collection.Add(item));
    }

    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        Random random = new();

        return source.OrderBy(r => random.Next());
    }

    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }
}
