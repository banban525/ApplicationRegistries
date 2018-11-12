using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Rename registry value name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RegistryNameAttribute : Attribute
    {
        /// <summary>
        /// Rename registry value name
        /// </summary>
        /// <param name="name">new registry value name. if empty string, use property name.</param>
        public RegistryNameAttribute([NotNull]string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// new registry value name. if empty string, use property name.
        /// </summary>
        [NotNull]
        public string Name { get; }
    }
}