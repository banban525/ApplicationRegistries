using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters
{
    public class SummaryPropertyReportData
    {
        public SummaryPropertyReportData(AccessorFieldDefinition fildDefinition, string description, IPropertyAccessorReportData propertyReportData)
        {
            FildDefinition = fildDefinition;
            Description = description;
            PropertyReportData = propertyReportData;
        }

        public AccessorFieldDefinition FildDefinition { get; }
        public IPropertyAccessorReportData PropertyReportData { get; }
        public string Description { get; }
    
    }
}