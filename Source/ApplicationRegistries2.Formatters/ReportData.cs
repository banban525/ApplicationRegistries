using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Formatters.AccessorFormatters;

namespace ApplicationRegistries2.Formatters
{
    public class ReportData
    {
        internal ReportData(
            IEnumerable<InterfaceReportData> interfaces, 
            IEnumerable<IPropertyFormatter> propertyFormatters)
        {
            Interfaces = interfaces;
            PropertyFormatters = propertyFormatters;
        }

        public IEnumerable<InterfaceReportData> Interfaces { get; }

        public IEnumerable<IPropertyFormatter> PropertyFormatters { get; }

        public string FormatProperty(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration, string key)
        {
            if (key == BuiltInAccessors.DefaultValue)
            {
                return "";
            }
            var formatter = PropertyFormatters.First(_=>_.Key == key);
            return formatter.Format(accessorTypeDeclaration, accessorFieldDeclaration);
        }

        public string FormatSummary(string key)
        {
            if (key == BuiltInAccessors.DefaultValue)
            {
                return "";
            }

            var summaryReportDataCollection = Interfaces
                .Where(typeReport => typeReport.TypeDeclaration.Keys.Contains(key))
                .Select(typeReport => new SummaryInterfaceReportData(typeReport.TypeDeclaration,
                    typeReport.Description,
                    typeReport.Properties.Select(CreateSummaryPropertyReports))).ToArray();
            
            var formatter = PropertyFormatters.FirstOrDefault(_ => _.Key == key);
            



            return formatter.FormatSummary(summaryReportDataCollection);
        }


        private static SummaryPropertyReportData CreateSummaryPropertyReports(PropertyReportData propertyReportData)
        {
            return new SummaryPropertyReportData(propertyReportData.FieldDeclaration,
                propertyReportData.Description
                );
        }

        public IEnumerable<string> Keys => Interfaces.SelectMany(_ => _.TypeDeclaration.Keys).Distinct();

        public string GetAccessorTitle(string key)
        {
            return PropertyFormatters.FirstOrDefault(_ => _.Key == key)?.Title ?? "";
        }
    }
}