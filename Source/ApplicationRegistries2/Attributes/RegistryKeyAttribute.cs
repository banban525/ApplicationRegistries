using System;

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
        /// <param name="key">new registry key</param>
        public RegistryKeyAttribute(string key = null)
        {
            Key = key;
        }

        /// <summary>
        /// new registry key
        /// </summary>
        public string Key { get; }
    }
}