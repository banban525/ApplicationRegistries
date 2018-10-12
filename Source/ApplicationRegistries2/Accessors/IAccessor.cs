using System;

namespace ApplicationRegistries2.Accessors
{
    public interface IAccessor
    {
        object Read(Type returnType, AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition);

        bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field);

        IPropertyAccessorReportData GetPropertyData(AccessorDefinition accessorDefinition,
            AccessorFieldDefinition field);

        IInterfaceAccessorReportData GetInterfaceData(AccessorDefinition accessorDefinition);
    }
}