using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters
{
    public class SummaryInterfaceReportData
    {
        public SummaryInterfaceReportData(AccessorTypeDeclaration accessorTypeDeclaration, string description, IInterfaceAccessorReportData reportData, IEnumerable<SummaryPropertyReportData> properties)
        {
            Properties = properties;
            Description = description;
            TypeDeclaration = accessorTypeDeclaration;
            ReportData = reportData;
        }

        public Type InterfaceType => TypeDeclaration.TargetInterfaceType;

        public AccessorTypeDeclaration TypeDeclaration { get; }

        public string Name => InterfaceType.Name;

        public string FullName => InterfaceType.FullName;

        public string AssemblyName => InterfaceType.Assembly.FullName;

        public IEnumerable<SummaryPropertyReportData> Properties { get; }

        public string Description { get; }

        public IInterfaceAccessorReportData ReportData { get; } 
    }
}
