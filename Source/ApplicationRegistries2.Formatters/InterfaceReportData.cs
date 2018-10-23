using System;
using System.Collections.Generic;

namespace ApplicationRegistries2.Formatters
{
    public class InterfaceReportData
    {
        public InterfaceReportData(AccessorTypeDeclaration accessorTypeDeclaration, string description, IEnumerable<PropertyReportData> properties)
        {
            Properties = properties;
            Description = description;
            TypeDeclaration = accessorTypeDeclaration;
        }

        public Type InterfaceType => TypeDeclaration.TargetInterfaceType;

        public AccessorTypeDeclaration TypeDeclaration { get; }

        public string Name => InterfaceType.Name;

        public string FullName => InterfaceType.FullName;

        public string AssemblyName => InterfaceType.Assembly.FullName;

        public IEnumerable<PropertyReportData> Properties { get; }

        public string Description { get; }
    }
}