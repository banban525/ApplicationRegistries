using System;
using System.Collections.Generic;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Interface)]
    public class ApplicationRegistryAttribute : Attribute
    {
        public ApplicationRegistryAttribute(params string[] keys)
        {
            if (keys.Length == 0)
            {
                keys = GetDefaultKeys();
            }
            Keys = keys;
        }

        public IEnumerable<string> Keys { get; }

        public static bool IsDefined(Type type)
        {
            return Attribute.IsDefined(type, typeof(ApplicationRegistryAttribute));
        }

        public static ApplicationRegistryAttribute Get(Type type)
        {
            return (ApplicationRegistryAttribute)GetCustomAttribute(type, typeof(ApplicationRegistryAttribute),
                true);
        }

        public static string[] GetDefaultKeys()
        {
            return new[]
            {
                BuiltInAccessors.CommandlineArguments,
                BuiltInAccessors.EnvironmenetVariable,
                BuiltInAccessors.UserRegistry,
                BuiltInAccessors.XmlFile,
                BuiltInAccessors.MachineRegistry,
                BuiltInAccessors.DefaultValue,
            };
        }
    }
}
