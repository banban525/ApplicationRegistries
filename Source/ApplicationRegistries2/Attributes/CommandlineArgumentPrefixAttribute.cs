using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Change command line argument prefix
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class CommandlineArgumentPrefixAttribute : Attribute
    {
        /// <summary>
        /// Change command line argument prefix
        /// </summary>
        /// <param name="prefix">new command line argument prefix. If empty string, no prefix</param>
        public CommandlineArgumentPrefixAttribute([NotNull]string prefix)
        {
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        }

        /// <summary>
        /// new command line argument prefix
        /// </summary>
        [NotNull]
        public string Prefix { get; }
    }
}