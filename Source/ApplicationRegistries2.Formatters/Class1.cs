using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;
using Microsoft.Win32;

namespace ApplicationRegistries2.Formatters
{
    public class Formatter
    {
        public string Format(string template, Type[] interfaceTypes)
        {
            throw new NotImplementedException();
        }
    }

    public class ReportDataCreator
    {
        public ReportData Create(Type[] interfaceTypes)
        {
            var accessorDefinitions  = interfaceTypes.Select(_ =>
                new AccessorTypeBuilder().Parse(_, _accessorRepository));


            return new ReportData(
                accessorDefinitions.Select(definition => new InterfaceReportData(
                    definition,
                    definition.Fields.Select(field => new PropertyReportData(
                        field, 
                        definition.AccessToList.Select(accessor=>accessor.GetPropertyData(definition, field)).ToArray()
                        )).ToArray()
                )).ToArray()
            );
        }

        public void RegistCustomAccessor(string key, IAccessor accessor)
        {
            _accessorRepository.RegistCustomAccessor(key, accessor);
        }

        private readonly AccessorRepository _accessorRepository = new AccessorRepository();
        private readonly RepositoryAccessorCache _cache = new RepositoryAccessorCache();
    }

    public class ReportData
    {
        public ReportData(IEnumerable<InterfaceReportData> interfaces)
        {
            Interfaces = interfaces;
        }

        public IEnumerable<InterfaceReportData> Interfaces { get; }
    }

    public class InterfaceReportData
    {
        public InterfaceReportData(AccessorDefinition definition, IEnumerable<PropertyReportData> properties)
        {
            Properties = properties;
            Definition = definition;
        }

        public Type InterfaceType => Definition.TargetInterfaceType;

        public AccessorDefinition Definition { get; }

        public string Name => InterfaceType.Name;

        public string FullName => InterfaceType.FullName;

        public string AssemblyName => InterfaceType.Assembly.FullName;

        public IEnumerable<PropertyReportData> Properties { get; }
    }

    public class PropertyReportData
    {
        public PropertyReportData(AccessorFieldDefinition fildDefinition, IEnumerable<IPropertyAccessorReportData> propertyAccessors)
        {
            FildDefinition = fildDefinition;
            PropertyAccessors = propertyAccessors;
        }

        public AccessorFieldDefinition FildDefinition { get; }
        public IEnumerable<IPropertyAccessorReportData> PropertyAccessors { get; }
    }


    public interface IPropertyAccessorReportData
    {
        string AccessorKey { get; }
    }


    public class RegistryAccessorReportData : IPropertyAccessorReportData
    {
        
        public string Key { get; }
        public string ValueName { get; }
        public RegistryValueKind ValueKind { get;}
        public string AccessorKey { get; }
    }

    public class DefaultValueAccessorReportData : IPropertyAccessorReportData
    {
        public object Value{ get; }
        public string AccessorKey { get; }
    }

    public class CommandlineArgumentsAccessorReportData : IPropertyAccessorReportData
    {
        public string CommandlineArgumentName { get; }
        public string AccessorKey { get; }
    }

    public class EnvironmentVariableAccessorReportData : IPropertyAccessorReportData
    {
        public string EnvironmentVariableName { get; }
        public string AccessorKey { get; }
    }

    public class XmlFileAccessorReportData : IPropertyAccessorReportData
    {
        public string FilePath { get; }
        public string XmlRootPath { get; }
        public string XmlValuePath { get; }
        public string AccessorKey { get; }
    }


}
