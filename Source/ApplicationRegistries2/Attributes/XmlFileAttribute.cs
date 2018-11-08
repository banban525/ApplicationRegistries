using System;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Change the path and parent node of the Xml file
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class XmlFileAttribute : Attribute
    {
        /// <summary>
        /// Change the path and parent node of the Xml file
        /// </summary>
        /// <param name="filePath">xml file path. Relative path from folder with ApplicationRegistries2.dll</param>
        /// <param name="xRootPath">XPath of parent node in Xml file</param>
        public XmlFileAttribute(string filePath = null, string xRootPath = null)
        {
            FilePath = filePath;
            XRootPath = xRootPath;
        }

        /// <summary>
        /// xml file path. Relative path from folder with ApplicationRegistries2.dll
        /// </summary>
        public string FilePath { get; }
        /// <summary>
        /// XPath of parent node in Xml file
        /// </summary>
        public string XRootPath { get; }
    }
}