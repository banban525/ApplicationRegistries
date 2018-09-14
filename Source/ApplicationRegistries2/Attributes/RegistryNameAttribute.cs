using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Property)]
    public class RegistryNameAttribute : Attribute
    {
        public RegistryNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}