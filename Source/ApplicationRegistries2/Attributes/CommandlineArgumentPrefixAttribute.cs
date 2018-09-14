using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Interface)]
    public class CommandlineArgumentPrefixAttribute : Attribute
    {
        public CommandlineArgumentPrefixAttribute(string prefix = null)
        {
            Prefix = prefix;
        }

        public string Prefix { get; }
    }
}