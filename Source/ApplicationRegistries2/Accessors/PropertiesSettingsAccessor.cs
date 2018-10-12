using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class PropertiesSettingsAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition)
        {
            var propertyData = (PropertiesSettingsPropertyData)GetPropertyData(accessorDefinition, accessorFieldDefinition);


            var settings =
                (ApplicationSettingsBase)Activator.CreateInstance(propertyData.Parent);
            SettingsBase.Synchronized(settings);
            
            var propValue = settings.Properties.Cast<SettingsProperty>()
                .FirstOrDefault(_ => _.Name == propertyData.Name);
            if (propValue == null)
            {
                throw new DataNotFoundException();
            }

            var val = settings[propertyData.Name] ?? throw new DataNotFoundException();
            return Convert.ChangeType(val, returnType);
        }

        public bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var propertyData = (PropertiesSettingsPropertyData)GetPropertyData(accessorDefinition, field);

            var settings =
                (ApplicationSettingsBase)Activator.CreateInstance(propertyData.Parent);
            SettingsBase.Synchronized(settings);

            var propValue = settings.Properties.Cast<SettingsProperty>()
                .FirstOrDefault(_ => _.Name == propertyData.Name);
            if (propValue == null)
            {
                return false;
            }

            return true;
        }

        public IPropertyAccessorReportData GetPropertyData(AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var appConfigTypeAttribute = accessorDefinition.GetAttribute<PropertiesSettingsTypeAttribute>();
            if (appConfigTypeAttribute == null)
            {
                throw new DataNotFoundException();
            }
            var appConfigNameAttribute = field.GetAttribute<PropertiesSettingsNameAttribute>();
            var name = appConfigNameAttribute?.Name ?? field.Name;

            return new PropertiesSettingsPropertyData(BuiltInAccessors.PropertiesSettings,
                appConfigTypeAttribute.Parent,
                name);
        }

        public IInterfaceAccessorReportData GetInterfaceData(AccessorDefinition accessorDefinition)
        {
            var appConfigTypeAttribute = accessorDefinition.GetAttribute<PropertiesSettingsTypeAttribute>();
            if (appConfigTypeAttribute == null)
            {
                throw new DataNotFoundException();
            }

            return new PropertiesSettingsnterfaceData(BuiltInAccessors.PropertiesSettings,
                appConfigTypeAttribute.Parent);
        }

        public class PropertiesSettingsnterfaceData : IInterfaceAccessorReportData
        {
            public PropertiesSettingsnterfaceData(string accessorKey, Type parent)
            {
                AccessorKey = accessorKey;
                Parent = parent;
            }

            public Type Parent { get; }
            public string AccessorKey { get; }
        }


        public class PropertiesSettingsPropertyData : IPropertyAccessorReportData
        {
            public PropertiesSettingsPropertyData(string accessorKey, Type parent, string name)
            {
                AccessorKey = accessorKey;
                Parent = parent;
                Name = name;
            }

            public string AccessorKey { get; }

            public Type Parent { get; }

            public string Name { get; }
        }

    }
}