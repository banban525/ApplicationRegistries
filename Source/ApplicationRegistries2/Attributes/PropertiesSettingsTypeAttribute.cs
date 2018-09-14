using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Interface)]
    public class PropertiesSettingsTypeAttribute : Attribute
    {
        public PropertiesSettingsTypeAttribute(Type parent = null)
        {
            Parent = parent;
        }

        public Type Parent { get; }
    }
}