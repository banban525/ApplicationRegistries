using System;
using JetBrains.Annotations;

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
        /// <param name="prefix">new prefix of environment variable name. If empty string, prefix is empty.</param>
        public EnvironmentVariablePrefixAttribute([NotNull]string prefix)
        {
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        }

        /// <summary>
        /// new prefix of environment variable name. If empty string, prefix is empty.
        /// </summary>
        [NotNull]
        public string Prefix { get; }
    }
}