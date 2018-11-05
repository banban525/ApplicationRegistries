namespace ApplicationRegistries2.Formatters
{
    public class SummaryPropertyReportData
    {
        public SummaryPropertyReportData(AccessorFieldDeclaration fieldDeclaration, string description)
        {
            FieldDeclaration = fieldDeclaration;
            Description = description;
        }

        public AccessorFieldDeclaration FieldDeclaration { get; }
        public string Description { get; }
    
    }
}