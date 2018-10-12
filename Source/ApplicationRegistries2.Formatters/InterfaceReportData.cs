using System;
using System.Collections.Generic;

namespace ApplicationRegistries2.Formatters
{
    public class InterfaceReportData
    {
        public InterfaceReportData(AccessorDefinition definition, string description, IEnumerable<PropertyReportData> properties)
        {
            Properties = properties;
            Description = description;
            Definition = definition;
        }

        public Type InterfaceType => Definition.TargetInterfaceType;

        public AccessorDefinition Definition { get; }

        public string Name => InterfaceType.Name;

        public string FullName => InterfaceType.FullName;

        public string AssemblyName => InterfaceType.Assembly.FullName;

        public IEnumerable<PropertyReportData> Properties { get; }

        public string Description { get; }
    }
}