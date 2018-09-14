using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Property)]
    public class EnvironmentVariableNameAttribute : Attribute
    {
        public EnvironmentVariableNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}