using System;

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
        /// <param name="prefix">new command line argument prefix. If an empty string is specified, there is no prefix</param>
        public CommandlineArgumentPrefixAttribute(string prefix = null)
        {
            Prefix = prefix;
        }

        /// <summary>
        /// new command line argument prefix
        /// </summary>
        public string Prefix { get; }
    }
}