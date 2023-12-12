
namespace doot_gen.util
{
    public static class EnumExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> func)
        {
            foreach (var item in values) { func(item); }
        }

        public static IEnumerable<T> WrapInEnumrable<T>(this T self)
        {
            yield return self;
        }
    }
}