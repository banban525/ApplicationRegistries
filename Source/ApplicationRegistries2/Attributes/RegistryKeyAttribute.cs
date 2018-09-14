using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Interface)]
    public class RegistryKeyAttribute : Attribute
    {
        public RegistryKeyAttribute(string key = null)
        {
            Key = key;
        }

        public string Key { get; }
    }
}