using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Formatters.AccessorFormatters;

namespace ApplicationRegistries2.Formatters
{
    public interface IPropertyFormatterFactory
    {
        IEnumerable<IPropertyFormatter> Create();
    }
}
