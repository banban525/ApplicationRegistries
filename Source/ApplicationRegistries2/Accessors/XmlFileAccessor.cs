using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class XmlFileAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition)
        {
            var xmlFileAttribute = accessorDefinition.GetAttribute<XmlFileAttribute>();
            var filePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                xmlFileAttribute?.FilePath ?? @".\ApplicationRegisties.xml");
            var xrootPath = xmlFileAttribute?.XRootPath ?? "/ApplicationRegisties/" + accessorDefinition.Name;

            var xmlNameAttribute = accessorFieldDefinition.GetAttribute<XmlNameAttribute>();
            var xPath = xmlNameAttribute?.XPath ?? accessorFieldDefinition.Name;

            var doc = XDocument.Load(filePath);
            var root = doc.XPathSelectElement(xrootPath);
            var element = root?.XPathSelectElement(xPath);

            if (element == null)
            {
                return null;
            }

            return Convert.ChangeType(element.Value, returnType);
        }

        public bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var xmlFileAttribute = accessorDefinition.GetAttribute<XmlFileAttribute>();

            var filePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                xmlFileAttribute?.FilePath ?? @".\ApplicationRegisties.xml");
            var xrootPath = xmlFileAttribute?.XRootPath ?? "/ApplicationRegisties/" + accessorDefinition.Name;

            if (File.Exists(filePath) == false)
            {
                return false;
            }

            var xmlNameAttribute = field.GetAttribute<XmlNameAttribute>();
            var xPath = xmlNameAttribute?.XPath ?? field.Name;

            var doc = XDocument.Load(filePath);
            var root = doc.XPathSelectElement(xrootPath);
            var element = root?.XPathSelectElement(xPath);

            return element != null;
        }

    }
}