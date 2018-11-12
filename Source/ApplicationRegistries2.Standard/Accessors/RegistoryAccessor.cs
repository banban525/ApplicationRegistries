using System;
using System.Linq;
using ApplicationRegistries2.Attributes;
using Microsoft.Win32;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class RegistoryAccessor : IAccessor
    {
        internal enum RegistryRoot
        {
            LocalMachine,
            CurrentUser
        }

        internal RegistoryAccessor(RegistryRoot registryRoot)
        {
            _registryRoot = registryRoot;
        }

        private readonly RegistryRoot _registryRoot;

        public object Read(Type returnType, AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var data = GetPropertyData(_registryRoot, accessorTypeDeclaration, accessorFieldDeclaration);
            using (var registrykey = _registryRoot == RegistryRoot.LocalMachine
                ? Registry.LocalMachine.OpenSubKey(data.Key)
                : Registry.CurrentUser.OpenSubKey(data.Key))
            {
                if (registrykey?.GetValueNames().All(_ => _ != data.ValueName) ?? true)
                {
                    return null;
                }
                
                return Convert.ChangeType(registrykey.GetValue(data.ValueName), returnType);
            }
        }

        public bool Exists(Type fieldType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var data = GetPropertyData(_registryRoot, accessorTypeDeclaration, accessorFieldDeclaration);
            using (var registrykey = _registryRoot == RegistryRoot.LocalMachine
                ? Registry.LocalMachine.OpenSubKey(data.Key)
                : Registry.CurrentUser.OpenSubKey(data.Key))
            {

                if (registrykey?.GetValueNames().All(_ => _ != data.ValueName) ?? true)
                {
                    return false;
                }
            }

            return true;
        }

        public static RegistryAccessorReportData GetPropertyData(RegistryRoot registryRoot, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var assemblyName = accessorTypeDeclaration.TargetInterfaceType.Assembly.GetName().Name;
            var interfaceName = accessorTypeDeclaration.TargetInterfaceType.Name;

            var registoKeyAttribute = accessorTypeDeclaration.GetAttribute<RegistryKeyAttribute>();
            var key = string.IsNullOrEmpty(registoKeyAttribute?.Key)
                ? $@"Software\ApplicationRegistries\{assemblyName}\{interfaceName}"
                : registoKeyAttribute.Key;

            var registoNameAttribute = accessorFieldDeclaration.GetAttribute<RegistryNameAttribute>();
            var name = string.IsNullOrEmpty(registoNameAttribute?.Name)
                ? accessorFieldDeclaration.Name
                : registoNameAttribute.Name;

            return new RegistryAccessorReportData(key,
                name
                );
        }
        

        public class RegistryInterfaceAccessorReportData
        {
            public RegistryInterfaceAccessorReportData(string key)
            {
                Key = key;
            }

            public string Key { get; }
        }


        public class RegistryAccessorReportData
        {
            public RegistryAccessorReportData(string key, string valueName)
            {
                Key = key;
                ValueName = valueName;
            }

            public string Key { get; }
            public string ValueName { get; }
        }


    }
}
