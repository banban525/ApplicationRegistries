using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    public static class ApplicationRegistry
    {
        private static readonly ApplicationRegistryManager ApplicationRegistryManager = new ApplicationRegistryManager();
        public static T Get<T>() where T : class
        {
            return ApplicationRegistryManager.Get<T>();
        }

        public static void RegistCustomAccessor(string key, IAccessor accessor)
        {
            ApplicationRegistryManager.RegistCustomAccessor(key, accessor);
        }
    }
}