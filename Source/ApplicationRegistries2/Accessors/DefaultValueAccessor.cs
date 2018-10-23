using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class DefaultValueAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var defaultValueAttribute = accessorFieldDeclaration.GetAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute == null)
            {
                throw new DataNotFoundException();
            }

            return Convert.ChangeType(defaultValueAttribute.DefaultValue, returnType);
        }

        public bool Exists(Type fieldType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var defaultValueAttribute = accessorFieldDeclaration.GetAttribute<DefaultValueAttribute>();
            return defaultValueAttribute != null;
        }

        public IPropertyAccessorReportData GetPropertyData(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var defaultValueAttribute = accessorFieldDeclaration.GetAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute == null)
            {
                throw new DataNotFoundException();
            }

            return new DefaultValueAccessorReportData(BuiltInAccessors.DefaultValue, defaultValueAttribute.DefaultValue);
        }
        public IInterfaceAccessorReportData GetInterfaceData(AccessorTypeDeclaration accessorTypeDeclaration)
        {
            return new EmptyInterfaceAccessorReportData(BuiltInAccessors.CommandlineArguments);
        }


        public class DefaultValueAccessorReportData : IPropertyAccessorReportData
        {
            public DefaultValueAccessorReportData(string accessorKey, object value)
            {
                AccessorKey = accessorKey;
                Value = value;
            }

            public object Value { get; }
            public string AccessorKey { get; }
        }
    }
}