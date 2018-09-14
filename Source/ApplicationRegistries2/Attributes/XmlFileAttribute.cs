using System;

namespace ApplicationRegistries2.Attributes
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Interface)]
    public class XmlFileAttribute : Attribute
    {
        public XmlFileAttribute(string filePath = null, string xRootPath = null)
        {
            FilePath = filePath;
            XRootPath = xRootPath;
        }

        public string FilePath { get; }
        public string XRootPath { get; }
    }
}