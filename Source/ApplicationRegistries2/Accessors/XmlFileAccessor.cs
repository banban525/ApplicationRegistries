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
        public object Read(Type returnType, AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var propertyData = (XmlFileAccessorReportData)GetPropertyData(accessorTypeDeclaration, accessorFieldDeclaration);
            if (File.Exists(propertyData.FilePath) == false)
            {
                return false;
            }

            var doc = XDocument.Load(propertyData.FilePath);
            var root = doc.XPathSelectElement(propertyData.XmlRootPath);
            var element = root?.XPathSelectElement(propertyData.XmlValuePath);

            return element == null ? null : Convert.ChangeType(element.Value, returnType);
        }

        public bool Exists(Type fieldType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var propertyData = (XmlFileAccessorReportData)GetPropertyData(accessorTypeDeclaration, accessorFieldDeclaration);
            if (File.Exists(propertyData.FilePath) == false)
            {
                return false;
            }

            var doc = XDocument.Load(propertyData.FilePath);
            var root = doc.XPathSelectElement(propertyData.XmlRootPath);
            var element = root?.XPathSelectElement(propertyData.XmlValuePath);

            return element != null;
        }

        public IPropertyAccessorReportData GetPropertyData(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var xmlFileAttribute = accessorTypeDeclaration.GetAttribute<XmlFileAttribute>();

            var filePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                xmlFileAttribute?.FilePath ?? @".\ApplicationRegisties.xml");
            var xrootPath = xmlFileAttribute?.XRootPath ?? "/ApplicationRegisties/" + accessorTypeDeclaration.Name;

            var xmlNameAttribute = accessorFieldDeclaration.GetAttribute<XmlNameAttribute>();
            var xPath = xmlNameAttribute?.XPath ?? accessorFieldDeclaration.Name;

            return new XmlFileAccessorReportData(BuiltInAccessors.XmlFile, 
                filePath,
                xrootPath,
                xPath);
        }

        public IInterfaceAccessorReportData GetInterfaceData(AccessorTypeDeclaration accessorTypeDeclaration)
        {
            var xmlFileAttribute = accessorTypeDeclaration.GetAttribute<XmlFileAttribute>();

            var filePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                xmlFileAttribute?.FilePath ?? @".\ApplicationRegisties.xml");
            var xrootPath = xmlFileAttribute?.XRootPath ?? "/ApplicationRegisties/" + accessorTypeDeclaration.Name;

            
            return new XmlFileInterfaceAccessorReportData(BuiltInAccessors.XmlFile,
                filePath,
                xrootPath);
        }

        public class XmlFileInterfaceAccessorReportData : IInterfaceAccessorReportData
        {
            public XmlFileInterfaceAccessorReportData(string accessorKey, string filePath, string xmlRootPath)
            {
                AccessorKey = accessorKey;
                FilePath = filePath;
                XmlRootPath = xmlRootPath;
            }

            public string AccessorKey { get; }
            public string FilePath { get; }
            public string XmlRootPath { get; }
        }

        public class XmlFileAccessorReportData : IPropertyAccessorReportData
        {
            public XmlFileAccessorReportData(string accessorKey, string filePath, string xmlRootPath, string xmlValuePath)
            {
                AccessorKey = accessorKey;
                FilePath = filePath;
                XmlRootPath = xmlRootPath;
                XmlValuePath = xmlValuePath;
            }

            public string FilePath { get; }
            public string XmlRootPath { get; }
            public string XmlValuePath { get; }
            public string AccessorKey { get; }
        }
    }
}