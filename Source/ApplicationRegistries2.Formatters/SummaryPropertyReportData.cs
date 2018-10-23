using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters
{
    public class SummaryPropertyReportData
    {
        public SummaryPropertyReportData(AccessorFieldDeclaration fieldDeclaration, string description, IPropertyAccessorReportData propertyReportData)
        {
            FieldDeclaration = fieldDeclaration;
            Description = description;
            PropertyReportData = propertyReportData;
        }

        public AccessorFieldDeclaration FieldDeclaration { get; }
        public IPropertyAccessorReportData PropertyReportData { get; }
        public string Description { get; }
    
    }
}