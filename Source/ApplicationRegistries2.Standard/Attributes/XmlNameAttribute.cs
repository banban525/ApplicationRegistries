using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Rename the value node in the Xml file
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class XmlNameAttribute : Attribute
    {
        /// <summary>
        /// Rename the value node in the Xml file
        /// </summary>
        /// <param name="xPath">XPath from parent node to value node. If empty string, use PropertyName</param>
        public XmlNameAttribute([NotNull]string xPath)
        {
            XPath = xPath ?? throw new ArgumentNullException(nameof(xPath));
        }

        /// <summary>
        /// XPath from parent node to value node. If empty string, use PropertyName
        /// </summary>
        [NotNull]
        public string XPath { get; }
    }
}