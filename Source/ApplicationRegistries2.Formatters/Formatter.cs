using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using ApplicationRegistries2.Accessors;
using ApplicationRegistries2.Formatters.AccessorFormatters;
using RazorEngine.Templating;

namespace ApplicationRegistries2.Formatters
{
    public class Formatter
    {
        private readonly ApplicationRegistryManager _applicationRegistryManager;

        private readonly List<IPropertyFormatter> _propertyFormatters;

        public Formatter(ApplicationRegistryManager applicationRegistryManager)
        {
            _applicationRegistryManager = applicationRegistryManager;
            _propertyFormatters = new List<IPropertyFormatter>
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

        public void AddFormatter(IPropertyFormatter customFomatter)
        {
            _propertyFormatters.Insert(0, customFomatter);
        }

        public void AddRangeFormatters(IEnumerable<IPropertyFormatter> customFomatters)
        {
            var propertyFormatters = customFomatters as IPropertyFormatter[] ?? customFomatters.ToArray();

            _propertyFormatters.InsertRange(0, propertyFormatters);

            propertyFormatters
                .Where(_ => _applicationRegistryManager.AccessorRepository.ExistsKey(_.Key) == false)
                .ToList()
                .ForEach(_ =>
                    _applicationRegistryManager.RegistCustomAccessor(_.Key, _.LoadAccessor()));

        }

        public void AddAccessor(string key, IAccessor customAccessor)
        {
            _applicationRegistryManager.RegistCustomAccessor(key, customAccessor);
        }

        public string Format(ReportTemplate template, Type[] interfaceTypes)
        {
            var repository = _applicationRegistryManager.AccessorRepository;
            var accessorTypeBuilder = new AccessorTypeBuilder();

            var interfaceReportDataCollection = interfaceTypes
                .Select(_ => accessorTypeBuilder.Parse(_, repository))
                .Select(CreateReportData).ToArray();

            var reportData = new ReportData(interfaceReportDataCollection, _propertyFormatters);

            return RazorEngine.Engine.Razor.RunCompile(template.TemplateRawText, "templateKey", typeof(ReportData), reportData);
        }

        private static InterfaceReportData CreateReportData(AccessorTypeDeclaration accessorTypeDeclaration)
        {
            XElement xDoc = null;
            var typeDescription = "";
            var xmlCommentFilePath = Path.ChangeExtension(accessorTypeDeclaration.TargetInterfaceType.Assembly.Location, ".xml");
            if (File.Exists(xmlCommentFilePath))
            {
                xDoc = XElement.Load(xmlCommentFilePath);
                var summaryElements = xDoc.XPathSelectElements(
                    "/members/member/summary");
                typeDescription = summaryElements.FirstOrDefault(_ =>
                                      _?.Parent?.Attribute("name")?.Value ==
                                      "T:" + accessorTypeDeclaration.TargetInterfaceType.FullName)?.Value ?? "";
            }

            var propertyReportDataList = accessorTypeDeclaration.Fields.Select(_ => new PropertyReportData(accessorTypeDeclaration, _,
                    GetDescription(accessorTypeDeclaration, _, xDoc)))
                .ToArray();

            return new InterfaceReportData(accessorTypeDeclaration, typeDescription, propertyReportDataList);
        }

        private static string GetDescription(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration, XElement xDoc)
        {
            if (xDoc == null)
            {
                return "";
            }
            var summaryElements = xDoc.XPathSelectElements(
                "/members/member/summary");
            return summaryElements.FirstOrDefault(_ =>
                _?.Parent?.Attribute("name")?.Value == $"P:{accessorTypeDeclaration.TargetInterfaceType.FullName}.{accessorFieldDeclaration.Name}")?.Value ?? "";

        }
    }
}
