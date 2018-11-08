using System;

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
        /// <param name="name">new environment variable name</param>
        public EnvironmentVariableNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// new environment variable name
        /// </summary>
        public string Name { get; }
    }
}