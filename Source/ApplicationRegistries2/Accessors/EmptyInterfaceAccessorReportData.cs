namespace ApplicationRegistries2.Accessors
{
    public class EmptyInterfaceAccessorReportData : IInterfaceAccessorReportData
    {
        public EmptyInterfaceAccessorReportData(string accessorKey)
        {
            AccessorKey = accessorKey;
        }

        public string AccessorKey { get; }
    }
}