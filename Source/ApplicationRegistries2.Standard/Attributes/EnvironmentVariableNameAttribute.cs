using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Change environment variable name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EnvironmentVariableNameAttribute : Attribute
    {
        /// <summary>
        /// Change environment variable name
        /// </summary>
        /// <param name="name">new environment variable name. if empty string, use property name.</param>
        public EnvironmentVariableNameAttribute([NotNull]string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// new environment variable name. if empty string, use property name.
        /// </summary>
        [NotNull]
        public string Name { get; }
    }
}