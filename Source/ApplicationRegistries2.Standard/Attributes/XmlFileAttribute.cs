using System;
using JetBrains.Annotations;

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
        /// <param name="filePath">xml file path. Relative path from folder with ApplicationRegistries2.dll. If empty, use .\ApplicationRegisties.xml</param>
        /// <param name="xRootPath">XPath of parent node in Xml file. If empty string, use /ApplicationRegisties/(Interface Name)</param>
        public XmlFileAttribute([NotNull]string filePath = "", [NotNull]string xRootPath = "")
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            XRootPath = xRootPath ?? throw new ArgumentNullException(nameof(xRootPath));
        }

        /// <summary>
        /// xml file path. Relative path from folder with ApplicationRegistries2.dll. If empty, use .\ApplicationRegisties.xml
        /// </summary>
        [NotNull]
        public string FilePath { get; }
        /// <summary>
        /// XPath of parent node in Xml file.If empty string, use /ApplicationRegisties/
        /// </summary>
        [NotNull]
        public string XRootPath { get; }
    }
}