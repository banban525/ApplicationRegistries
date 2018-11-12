using System;
using JetBrains.Annotations;

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
        /// <param name="name">new name. if empty string, use property name.</param>
        public CommandlineArgumentNameAttribute([NotNull]string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// command line argument name. if empty string, use property name.
        /// </summary>
        [NotNull]
        public string Name { get; }
    }
}