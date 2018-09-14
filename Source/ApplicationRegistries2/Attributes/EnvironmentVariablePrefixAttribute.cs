using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Interface)]
    public class EnvironmentVariablePrefixAttribute : Attribute
    {
        public EnvironmentVariablePrefixAttribute(string prefix = null)
        {
            Prefix = prefix;
        }

        public string Prefix { get; }
    }
}