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

        public string FormatProperty(AccessorDefinition definition, AccessorFieldDefinition field, IPropertyAccessorReportData propertyReportData)
        {
            if (propertyReportData.AccessorKey == BuiltInAccessors.DefaultValue)
            {
                return "";
            }
            var formatter = PropertyFormatters.First(_=>_.Key == propertyReportData.AccessorKey);
            return formatter.Format(definition, field, propertyReportData);
        }

        public string FormatSummary(string key)
        {
            if (key == BuiltInAccessors.DefaultValue)
            {
                return "";
            }

            var summaryReportDataCollection = Interfaces
                .Where(typeReport => typeReport.Definition.Keys.Contains(key))
                .Select(typeReport => new SummaryInterfaceReportData(typeReport.Definition,
                    typeReport.Description,
                    _repository.GetAccessor(key).GetInterfaceData(typeReport.Definition),
                    typeReport.Properties.Select(prop =>
                        CreateSummaryPropertyReports(key, typeReport, prop, _repository)))).ToArray();
            
            var formatter = PropertyFormatters.FirstOrDefault(_ => _.Key == key);
            

            return formatter.FormatSummary(summaryReportDataCollection);
        }


        private static SummaryPropertyReportData CreateSummaryPropertyReports(string key, InterfaceReportData parent, PropertyReportData propertyReportData, AccessorRepository repository)
        {
            return new SummaryPropertyReportData(propertyReportData.FildDefinition,
                propertyReportData.Description,
                repository.GetAccessor(key).GetPropertyData(parent.Definition, propertyReportData.FildDefinition)
                );
        }

        public IEnumerable<string> Keys => Interfaces.SelectMany(_ => _.Definition.Keys).Distinct();

        public string GetAccessorTitle(string key)
        {
            return PropertyFormatters.FirstOrDefault(_ => _.Key == key)?.Title ?? "";
        }
    }
}