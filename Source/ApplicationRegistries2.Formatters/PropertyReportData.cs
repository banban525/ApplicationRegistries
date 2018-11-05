using System.Collections.Generic;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters
{
    public class PropertyReportData
    {
        public PropertyReportData(AccessorTypeDeclaration accessorTypeDeclaration ,AccessorFieldDeclaration accessorFieldDeclaration, string description)
        {
            AccessorTypeDeclaration = accessorTypeDeclaration;
            FieldDeclaration = accessorFieldDeclaration;
            Description = description;
        }

        private AccessorTypeDeclaration AccessorTypeDeclaration { get; }
        public AccessorFieldDeclaration FieldDeclaration { get; }
        public IEnumerable<string> Keys => AccessorTypeDeclaration.Keys;
        public string Description { get; }

        public string DefaultValue
        {
            get
            {
                var defaultValueReportData = DefaultValueAccessor.GetPropertyData(AccessorTypeDeclaration, FieldDeclaration);
                return defaultValueReportData?.Value.ToString() ?? "";
            }
        }
    }
}