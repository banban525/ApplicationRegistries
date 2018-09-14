using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandlineArgumentNameAttribute : Attribute
    {
        public CommandlineArgumentNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}