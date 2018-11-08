using System;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Rename command line arguments
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandlineArgumentNameAttribute : Attribute
    {
        /// <summary>
        /// Rename command line arguments
        /// </summary>
        /// <param name="name">new name</param>
        public CommandlineArgumentNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// command line argument name
        /// </summary>
        public string Name { get; }
    }
}