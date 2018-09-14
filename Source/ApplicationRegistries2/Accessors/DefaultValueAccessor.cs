using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class DefaultValueAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition)
        {
            var defaultValueAttribute = accessorFieldDefinition.GetAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute == null)
            {
                throw new DataNotFoundException();
            }

            return Convert.ChangeType(defaultValueAttribute.DefaultValue, returnType);
        }

        public bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var defaultValueAttribute = field.GetAttribute<DefaultValueAttribute>();
            return defaultValueAttribute != null;
        }

    }
}