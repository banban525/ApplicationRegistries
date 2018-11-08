using System;
using System.Collections.Generic;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Attributes to be assigned to the external configuration interface
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class ApplicationRegistryAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keys">Priority for Accessor Keys</param>
        public ApplicationRegistryAttribute(params string[] keys)
        {
            if (keys.Length == 0)
            {
                keys = GetDefaultKeys();
            }
            Keys = keys;
        }

        /// <summary>
        /// Priority for Accessor Keys.
        /// </summary>
        public IEnumerable<string> Keys { get; }

        /// <summary>
        /// Check interface is defined.
        /// </summary>
        /// <param name="type">target interface type</param>
        /// <returns>true:this attribute is defined for target interface, false: not defined.</returns>
        public static bool IsDefined(Type type)
        {
            return Attribute.IsDefined(type, typeof(ApplicationRegistryAttribute));
        }

        /// <summary>
        /// Get attribute from interface type
        /// </summary>
        /// <param name="type">target interface type</param>
        /// <returns>if defined, return attribute object, other is returned null.</returns>
        public static ApplicationRegistryAttribute Get(Type type)
        {
            return (ApplicationRegistryAttribute)GetCustomAttribute(type, typeof(ApplicationRegistryAttribute),
                true);
        }

        /// <summary>
        /// Get Default Settuings of Keys
        /// </summary>
        /// <returns>Default Settuings os Keys</returns>
        public static string[] GetDefaultKeys()
        {
            return new[]
            {
                BuiltInAccessors.CommandlineArguments,
                BuiltInAccessors.EnvironmenetVariable,
                BuiltInAccessors.UserRegistry,
                BuiltInAccessors.XmlFile,
                BuiltInAccessors.MachineRegistry,
            };
        }
    }
}
