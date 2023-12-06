using Optional;
using Optional.Unsafe;

namespace doot_gen.util
{
    public static class OptionExtensions
    {
        public static void DoIfPresent<T>(this Option<T> opt, Action<T> func)
        {
            if(opt.HasValue) { func(opt.ValueOrFailure()); }
        }
    }
}
