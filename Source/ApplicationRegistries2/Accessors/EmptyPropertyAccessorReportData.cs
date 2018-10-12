using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistries2.Accessors
{
    public class EmptyPropertyAccessorReportData : IPropertyAccessorReportData
    {
        public EmptyPropertyAccessorReportData(string accessorKey)
        {
            AccessorKey = accessorKey;
        }

        public string AccessorKey { get; }
    }
}
