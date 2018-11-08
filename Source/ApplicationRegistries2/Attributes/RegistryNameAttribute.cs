using System;

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
        /// <param name="name">new registry value name</param>
        public RegistryNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// new registry value name
        /// </summary>
        public string Name { get; }
    }
}