using System;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Specify the type of Properties.Settings
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class PropertiesSettingsTypeAttribute : Attribute
    {
        /// <summary>
        /// Specify the type of Properties.Settings
        /// </summary>
        /// <param name="parent">Properties.Settings type</param>
        public PropertiesSettingsTypeAttribute(Type parent = null)
        {
            Parent = parent;
        }

        /// <summary>
        /// Properties.Settings type
        /// </summary>
        public Type Parent { get; }
    }
}