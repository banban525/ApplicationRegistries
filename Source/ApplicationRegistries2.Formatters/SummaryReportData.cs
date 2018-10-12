using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistries2.Formatters
{
    public class SummaryReportData
    {
        public SummaryReportData(string key, IReadOnlyCollection<SummaryInterfaceReportData> interfaceReportDataCollection)
        {
            Key = key;
            InterfaceReportDataCollection = interfaceReportDataCollection;
        }

        public string Key { get; }

        public IReadOnlyCollection<SummaryInterfaceReportData> InterfaceReportDataCollection { get; }
    }
}
