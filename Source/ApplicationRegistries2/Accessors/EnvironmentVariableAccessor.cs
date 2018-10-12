using System;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class EnvironmentVariableAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition)
        {
            var environmentVariableName = GetEnvironmentVariableName(accessorDefinition, accessorFieldDefinition);
            var val = Environment.GetEnvironmentVariable(environmentVariableName);

            if (val == null)
            {
                throw new DataNotFoundException();
            }

            return Convert.ChangeType(val, returnType);
        }

        public bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var environmentVariableName = GetEnvironmentVariableName(accessorDefinition, field);
            var val = Environment.GetEnvironmentVariable(environmentVariableName);

            return val != null;
        }

        public IPropertyAccessorReportData GetPropertyData(AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            return new EnvironmentVariableAccessorReportData(BuiltInAccessors.EnvironmenetVariable,
                GetEnvironmentVariableName(accessorDefinition, field));
        }
        public IInterfaceAccessorReportData GetInterfaceData(AccessorDefinition accessorDefinition)
        {
            return new EmptyInterfaceAccessorReportData(BuiltInAccessors.CommandlineArguments);
        }


        private string GetEnvironmentVariableName(AccessorDefinition accessorDefinition,
            AccessorFieldDefinition field)
        {
            var assemblyName = accessorDefinition.TargetInterfaceType.Assembly.GetName().Name;
            var interfaceName = accessorDefinition.TargetInterfaceType.Name;

            var environmentVariablePrefixAttribute =
                accessorDefinition.GetAttribute<EnvironmentVariablePrefixAttribute>();
            var prefix = environmentVariablePrefixAttribute?.Prefix ?? $@"{assemblyName}_{interfaceName}";

            var environmentVariableNameAttribute = field.GetAttribute<EnvironmentVariableNameAttribute>();
            var name = environmentVariableNameAttribute?.Name ?? field.Name;

            return $"{prefix}_{name}";

        }


        public class EnvironmentVariableAccessorReportData : IPropertyAccessorReportData
        {
            public EnvironmentVariableAccessorReportData(string accessorKey, string environmentVariableName)
            {
                AccessorKey = accessorKey;
                EnvironmentVariableName = environmentVariableName;
            }

            public string EnvironmentVariableName { get; }
            public string AccessorKey { get; }
        }

    }
}