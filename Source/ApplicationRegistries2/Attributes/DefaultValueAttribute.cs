using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Default value, when other values ​​can not be accessed
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultValueAttribute : Attribute
    {
        /// <summary>
        /// Default value, when other values ​​can not be accessed
        /// </summary>
        /// <param name="defaultValue">Default value</param>
        public DefaultValueAttribute([CanBeNull]object defaultValue)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// Default value
        /// </summary>
        [CanBeNull]
        public object DefaultValue { get; }
    }
}