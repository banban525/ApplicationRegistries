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

        public static DefaultValueAccessorReportData GetPropertyData(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var defaultValueAttribute = accessorFieldDeclaration.GetAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute == null)
            {
                throw new DataNotFoundException();
            }

            return new DefaultValueAccessorReportData(defaultValueAttribute.DefaultValue);
        }

        public class DefaultValueAccessorReportData
        {
            public DefaultValueAccessorReportData(object value)
            {
                Value = value;
            }

            public object Value { get; }
        }
    }
}