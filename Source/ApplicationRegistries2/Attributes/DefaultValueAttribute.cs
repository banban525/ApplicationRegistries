using System;

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
        public DefaultValueAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// Default value
        /// </summary>
        public object DefaultValue { get; }
    }
}