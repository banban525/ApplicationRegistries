using JetBrains.Annotations;

namespace ApplicationRegistries2
{
    /// <summary>
    /// The static class to access external settings
    /// </summary>
    public static class ApplicationRegistry
    {
        internal static readonly ApplicationRegistryManager ApplicationRegistryManager = new ApplicationRegistryManager();

        /// <summary>
        /// Get external setting proxy
        /// </summary>
        /// <typeparam name="T">external settings interface type</typeparam>
        /// <returns>external setting proxy</returns>
        /// <exception cref="DataNotFoundException">no matched data</exception>
        public static T Get<T>() where T : class
        {
            return ApplicationRegistryManager.Get<T>();
        }

        /// <summary>
        /// regist custom accessor
        /// </summary>
        /// <param name="key">accessor key</param>
        /// <param name="accessor">custom accessor</param>
        public static void RegistCustomAccessor([NotNull]string key, [NotNull]IAccessor accessor)
        {
            ApplicationRegistryManager.RegistCustomAccessor(key, accessor);
        }
    }
}