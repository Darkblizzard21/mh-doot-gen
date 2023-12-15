namespace doot_gen
{
    public static class Version
    {
        public const int VERSION_MAJOR = 1;
        public const int VERSION_MINOR = 1;
        public const int VERSION_PATCH = 0;

        public static string String()
        {
            return VERSION_MAJOR + "." + VERSION_MINOR + "." + VERSION_PATCH;
        }

    }
}