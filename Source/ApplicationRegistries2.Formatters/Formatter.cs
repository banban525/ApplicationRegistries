using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using ApplicationRegistries2.Accessors;
using ApplicationRegistries2.Formatters.AccessorFormatters;
using Microsoft.Win32;
using RazorEngine.Templating;

namespace ApplicationRegistries2.Formatters
{
    public class Formatter
    {
        private readonly ApplicationRegistryManager _applicationRegistryManager;

        private readonly IEnumerable<IPropertyFormatter> _propertyFormatters;

        public Formatter(ApplicationRegistryManager applicationRegistryManager)
        {
            _applicationRegistryManager = applicationRegistryManager;
            _propertyFormatters = new IPropertyFormatter[]
            {
                new CommandlineArgumentFormatter(),
                new EnvironmentVariableFormatter(),
                new PropertiesSettingsFormatter(),
                new RegistryFormatter(RegistoryAccessor.RegistryRoot.CurrentUser),
                new RegistryFormatter(RegistoryAccessor.RegistryRoot.LocalMachine),
                new XmlFileFormatter(),
            };
        }

        public Formatter()
        :this(ApplicationRegistry.ApplicationRegistryManager)
        {
        }

        public string Format(string razorTemplate, Type[] interfaceTypes)
        {
            var repository = _applicationRegistryManager.AccessorRepository;
            var accessorTypeBuilder = new AccessorTypeBuilder();

            var interfaceReportDataCollection = interfaceTypes
                .Select(_ => accessorTypeBuilder.Parse(_, repository))
                .Select(CreateReportData).ToArray();

            var reportData = new ReportData(interfaceReportDataCollection, _propertyFormatters, repository);

            return RazorEngine.Engine.Razor.RunCompile(razorTemplate, "templateKey", typeof(ReportData), reportData);
        }

        //private static IEnumerable<SummaryReportData> CreateSummaryReportData(InterfaceReportData interfaceReportData, AccessorRepository repository)
        //{
        //    return interfaceReportData.Definition.Keys.Select(key=>
        //        new SummaryReportData(key,
        //        CreateSummaryPropertyReportData(key, interfaceReportData, repository)));
        //}

        //private static SummaryInterfaceReportData CreateSummaryPropertyReportData(string key, InterfaceReportData interfaceReportData, AccessorRepository repository)
        //{
        //    return new SummaryInterfaceReportData(interfaceReportData.Definition, interfaceReportData.Description, repository.GetAccessor(key).GetInterfaceData(interfaceReportData.Definition), interfaceReportData.Properties.Select(prop => new SummaryPropertyReportData(prop.FildDefinition, prop.Description, prop.PropertyAccessors.First(_ => _.AccessorKey == key))));
        //}

        private static InterfaceReportData CreateReportData(AccessorDefinition definition)
        {
            XElement xDoc = null;
            var typeDescription = "";
            var xmlCommentFilePath = Path.ChangeExtension(definition.TargetInterfaceType.Assembly.Location, ".xml");
            if (File.Exists(xmlCommentFilePath))
            {
                xDoc = XElement.Load(xmlCommentFilePath);
                var summaryElements = xDoc.XPathSelectElements(
                    "/members/member/summary");
                typeDescription = summaryElements.FirstOrDefault(_ =>
                    _.Parent.Attribute("name").Value == "T:" + definition.TargetInterfaceType.FullName).Value;
            }

            var propertyReportDataList = definition.Fields.Select(_ => new PropertyReportData(_,
                    GetDescription(definition, _, xDoc),
                    definition.AccessToList.Select(accessor => accessor.GetPropertyData(definition, _))))
                .ToArray();

            return new InterfaceReportData(definition, typeDescription, propertyReportDataList);
        }

        private static string GetDescription(AccessorDefinition definition, AccessorFieldDefinition field, XElement xDoc)
        {
            var summaryElements = xDoc.XPathSelectElements(
                "/members/member/summary");
            return summaryElements.FirstOrDefault(_ =>
                _.Parent.Attribute("name").Value == $"P:{definition.TargetInterfaceType.FullName}.{field.Name}").Value;

        }
    }
}
