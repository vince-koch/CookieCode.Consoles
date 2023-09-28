namespace CookieCode.Consoles
{
    public static partial class ExtensionMethods
    {
        public static void ForEach<TItem>(this IEnumerable<TItem> enumerable, Action<TItem> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static void ForEach<TItem>(this IEnumerable<TItem> enumerable, Action<TItem, int> action)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                action(item, index);
                index++;
            }
        }
    }
}
