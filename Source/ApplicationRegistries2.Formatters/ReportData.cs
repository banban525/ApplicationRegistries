using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;
using ApplicationRegistries2.Formatters.AccessorFormatters;

namespace ApplicationRegistries2.Formatters
{
    public class ReportData
    {
        internal ReportData(
            IEnumerable<InterfaceReportData> interfaces, 
            IEnumerable<IPropertyFormatter> propertyFormatters,
            AccessorRepository repository)
        {
            Interfaces = interfaces;
            PropertyFormatters = propertyFormatters;
            _repository = repository;
        }

        public IEnumerable<InterfaceReportData> Interfaces { get; }
        private readonly AccessorRepository _repository;

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
                    typeReport.Properties.Select(prop =>
                        CreateSummaryPropertyReports(key, typeReport, prop, _repository)))).ToArray();
            
            var formatter = PropertyFormatters.FirstOrDefault(_ => _.Key == key);
            

            return formatter.FormatSummary(summaryReportDataCollection);
        }


        private static SummaryPropertyReportData CreateSummaryPropertyReports(string key, InterfaceReportData parent, PropertyReportData propertyReportData, AccessorRepository repository)
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