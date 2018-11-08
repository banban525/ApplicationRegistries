using System;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Change property name of Properties.Settings
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertiesSettingsNameAttribute : Attribute
    {
        /// <summary>
        /// new property name of Properties.Settings
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Change property name of Properties.Settings
        /// </summary>
        /// <param name="name">new property name of Properties.Settings</param>
        public PropertiesSettingsNameAttribute(string name)
        {
            Name = name;
        }
    }
}