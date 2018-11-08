using System;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Change prefix of environment variable name
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class EnvironmentVariablePrefixAttribute : Attribute
    {
        /// <summary>
        /// Change prefix of environment variable name
        /// </summary>
        /// <param name="prefix">new prefix of environment variable name</param>
        public EnvironmentVariablePrefixAttribute(string prefix = null)
        {
            Prefix = prefix;
        }

        /// <summary>
        /// new prefix of environment variable name
        /// </summary>
        public string Prefix { get; }
    }
}