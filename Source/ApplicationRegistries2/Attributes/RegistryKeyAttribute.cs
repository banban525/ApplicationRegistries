using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Change registry key
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class RegistryKeyAttribute : Attribute
    {
        /// <summary>
        /// Change registry key
        /// </summary>
        /// <param name="key">new registry key. if empty string, use Software\ApplicationRegistries\{assembly name}\{interface name}.</param>
        public RegistryKeyAttribute([NotNull]string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        /// <summary>
        /// new registry key.if empty string, use Software\ApplicationRegistries\{assembly name}\{interface name}.
        /// </summary>
        [NotNull]
        public string Key { get; }
    }
}