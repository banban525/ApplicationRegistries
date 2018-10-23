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
        public object Read(Type returnType, AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var propertyData = (PropertiesSettingsPropertyData)GetPropertyData(accessorTypeDeclaration, accessorFieldDeclaration);


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

        public bool Exists(Type fieldType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var propertyData = (PropertiesSettingsPropertyData)GetPropertyData(accessorTypeDeclaration, accessorFieldDeclaration);

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

        public IPropertyAccessorReportData GetPropertyData(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var appConfigTypeAttribute = accessorTypeDeclaration.GetAttribute<PropertiesSettingsTypeAttribute>();
            if (appConfigTypeAttribute == null)
            {
                throw new DataNotFoundException();
            }
            var appConfigNameAttribute = accessorFieldDeclaration.GetAttribute<PropertiesSettingsNameAttribute>();
            var name = appConfigNameAttribute?.Name ?? accessorFieldDeclaration.Name;

            return new PropertiesSettingsPropertyData(BuiltInAccessors.PropertiesSettings,
                appConfigTypeAttribute.Parent,
                name);
        }

        public IInterfaceAccessorReportData GetInterfaceData(AccessorTypeDeclaration accessorTypeDeclaration)
        {
            var appConfigTypeAttribute = accessorTypeDeclaration.GetAttribute<PropertiesSettingsTypeAttribute>();
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