using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Property)]
    public class XmlNameAttribute : Attribute
    {
        public XmlNameAttribute(string xPath = null)
        {
            XPath = xPath;
        }

        public string XPath { get; }
    }
}