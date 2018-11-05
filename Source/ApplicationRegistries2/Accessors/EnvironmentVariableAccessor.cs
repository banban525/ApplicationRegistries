using System;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class EnvironmentVariableAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var environmentVariableName = GetEnvironmentVariableName(accessorTypeDeclaration, accessorFieldDeclaration);
            var val = Environment.GetEnvironmentVariable(environmentVariableName);

            if (val == null)
            {
                throw new DataNotFoundException();
            }

            return Convert.ChangeType(val, returnType);
        }

        public bool Exists(Type fieldType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var environmentVariableName = GetEnvironmentVariableName(accessorTypeDeclaration, accessorFieldDeclaration);
            var val = Environment.GetEnvironmentVariable(environmentVariableName);

            return val != null;
        }

        public static EnvironmentVariableAccessorReportData GetPropertyData(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            return new EnvironmentVariableAccessorReportData(GetEnvironmentVariableName(accessorTypeDeclaration, accessorFieldDeclaration));
        }


        private static string GetEnvironmentVariableName(AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var assemblyName = accessorTypeDeclaration.TargetInterfaceType.Assembly.GetName().Name;
            var interfaceName = accessorTypeDeclaration.TargetInterfaceType.Name;

            var environmentVariablePrefixAttribute =
                accessorTypeDeclaration.GetAttribute<EnvironmentVariablePrefixAttribute>();
            var prefix = environmentVariablePrefixAttribute?.Prefix ?? $@"{assemblyName}_{interfaceName}";

            var environmentVariableNameAttribute = accessorFieldDeclaration.GetAttribute<EnvironmentVariableNameAttribute>();
            var name = environmentVariableNameAttribute?.Name ?? accessorFieldDeclaration.Name;

            return $"{prefix}_{name}";

        }


        public class EnvironmentVariableAccessorReportData
        {
            public EnvironmentVariableAccessorReportData(string environmentVariableName)
            {
                EnvironmentVariableName = environmentVariableName;
            }

            public string EnvironmentVariableName { get; }
        }

    }
}