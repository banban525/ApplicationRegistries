using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertiesSettingsNameAttribute : Attribute
    {
        public string Name { get; }

        public PropertiesSettingsNameAttribute(string name)
        {
            Name = name;
        }
    }
}