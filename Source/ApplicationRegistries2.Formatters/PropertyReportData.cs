using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters
{
    public class PropertyReportData
    {
        public PropertyReportData(AccessorFieldDefinition fildDefinition,string description, IEnumerable<IPropertyAccessorReportData> propertyAccessors)
        {
            FildDefinition = fildDefinition;
            Description = description;
            PropertyAccessors = propertyAccessors;
        }

        public AccessorFieldDefinition FildDefinition { get; }
        public IEnumerable<IPropertyAccessorReportData> PropertyAccessors { get; }
        public string Description { get; }
        public bool ExistsAccessor(string key)
        {
            return PropertyAccessors.Any(_ => _.AccessorKey == key);
        }

        public IPropertyAccessorReportData GetAccessor(string key)
        {
            return PropertyAccessors.FirstOrDefault(_ => _.AccessorKey == key);
        }

        public string DefaultValue
        {
            get
            {
                var defaultValueReportData = (DefaultValueAccessor.DefaultValueAccessorReportData)PropertyAccessors.FirstOrDefault(_ => _.AccessorKey == BuiltInAccessors.DefaultValue);
                return defaultValueReportData?.Value.ToString() ?? "";
            }
        }
    }
}