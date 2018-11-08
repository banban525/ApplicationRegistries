using System;

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
        /// <param name="xPath">XPath from parent node to value node</param>
        public XmlNameAttribute(string xPath = null)
        {
            XPath = xPath;
        }

        /// <summary>
        /// XPath from parent node to value node
        /// </summary>
        public string XPath { get; }
    }
}