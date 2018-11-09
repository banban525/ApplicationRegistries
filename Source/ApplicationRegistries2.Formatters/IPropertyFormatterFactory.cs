using System.Collections.Generic;

namespace ApplicationRegistries2.Formatters
{
    public interface IPropertyFormatterFactory
    {
        IEnumerable<IPropertyFormatter> Create();
    }
}
