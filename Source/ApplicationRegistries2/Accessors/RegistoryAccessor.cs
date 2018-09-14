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

        public object Read(Type returnType, AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition)
        {
            var assemblyName = accessorDefinition.TargetInterfaceType.Assembly.GetName().Name;
            var interfaceName = accessorDefinition.TargetInterfaceType.Name;

            var registoKeyAttribute = accessorDefinition.GetAttribute<RegistryKeyAttribute>();
            var key = registoKeyAttribute?.Key ?? $@"Software\ApplicationRegistries\{assemblyName}\{interfaceName}";

            var registoNameAttribute = accessorFieldDefinition.GetAttribute<RegistryNameAttribute>();
            var name = registoNameAttribute?.Name ?? accessorFieldDefinition.Name;
            using (var registrykey = _registryRoot == RegistryRoot.LocalMachine
                ? Registry.LocalMachine.OpenSubKey(key)
                : Registry.CurrentUser.OpenSubKey(key))
            {
                if (registrykey?.GetValueNames().All(_ => _ != name) ?? true)
                {
                    return null;
                }
                
                return Convert.ChangeType(registrykey.GetValue(name), returnType);
            }
        }

        public bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var assemblyName = accessorDefinition.TargetInterfaceType.Assembly.GetName().Name;
            var interfaceName = accessorDefinition.TargetInterfaceType.Name;

            var registoKeyAttribute = accessorDefinition.GetAttribute<RegistryKeyAttribute>();
            var key = registoKeyAttribute?.Key ?? $@"Software\ApplicationRegistries\{assemblyName}\{interfaceName}";

            var registoNameAttribute = field.GetAttribute<RegistryNameAttribute>();
            var name = registoNameAttribute?.Name ?? field.Name;
            using (var registrykey = _registryRoot == RegistryRoot.LocalMachine
                ? Registry.LocalMachine.OpenSubKey(key)
                : Registry.CurrentUser.OpenSubKey(key))
            {

                if (registrykey?.GetValueNames().All(_ => _ != name) ?? true)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
