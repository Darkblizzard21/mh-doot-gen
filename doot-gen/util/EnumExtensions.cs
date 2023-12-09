
namespace doot_gen.util
{
    public static class EnumExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> func)
        {
            foreach (var item in values) { func(item); }
        }
    }
}