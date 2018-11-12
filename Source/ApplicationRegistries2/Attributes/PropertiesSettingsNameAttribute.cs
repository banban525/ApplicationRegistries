using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Change property name of Properties.Settings
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertiesSettingsNameAttribute : Attribute
    {
        /// <summary>
        /// new property name of Properties.Settings. If empty string, use property name.
        /// </summary>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Change property name of Properties.Settings
        /// </summary>
        /// <param name="name">new property name of Properties.Settings. If empty string, use property name.</param>
        public PropertiesSettingsNameAttribute([NotNull]string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}