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
            var appConfigTypeAttribute = accessorDefinition.GetAttribute<PropertiesSettingsTypeAttribute>();

            if (appConfigTypeAttribute == null)
            {
                throw new DataNotFoundException();
            }

            var settings =
                (ApplicationSettingsBase)Activator.CreateInstance(appConfigTypeAttribute.Parent);
            SettingsBase.Synchronized(settings);

            var appConfigNameAttribute = accessorFieldDefinition.GetAttribute<PropertiesSettingsNameAttribute>();
            var name = appConfigNameAttribute?.Name ?? accessorFieldDefinition.Name;

            var propValue = settings.Properties.Cast<SettingsProperty>()
                .FirstOrDefault(_ => _.Name == name);
            if (propValue == null)
            {
                throw new DataNotFoundException();
            }

            var val = settings[name] ?? throw new DataNotFoundException();
            return Convert.ChangeType(val, returnType);
        }

        public bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var appConfigTypeAttribute = accessorDefinition.GetAttribute<PropertiesSettingsTypeAttribute>();

            if (appConfigTypeAttribute == null)
            {
                return false;
            }

            var settings =
                (ApplicationSettingsBase)Activator.CreateInstance(appConfigTypeAttribute.Parent);
            SettingsBase.Synchronized(settings);

            var appConfigNameAttribute = field.GetAttribute<PropertiesSettingsNameAttribute>();
            var name = appConfigNameAttribute?.Name ?? field.Name;

            var propValue = settings.Properties.Cast<SettingsProperty>()
                .FirstOrDefault(_ => _.Name == name);
            if (propValue == null)
            {
                return false;
            }

            return true;
        }
    }
}