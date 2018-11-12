using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using ApplicationRegistries2.Accessors;
using ApplicationRegistries2.Formatters.AccessorFormatters;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace ApplicationRegistries2.Formatters
{
    public class Formatter
    {
        private readonly List<IPropertyFormatter> _propertyFormatters;
        private TextWriter _logWriter = TextWriter.Null;

        public Formatter()
        {
            _propertyFormatters = new List<IPropertyFormatter>
            {
                new CommandlineArgumentFormatter(),
                new EnvironmentVariableFormatter(),
                new RegistryFormatter(RegistoryAccessor.RegistryRoot.CurrentUser),
                new RegistryFormatter(RegistoryAccessor.RegistryRoot.LocalMachine),
                new XmlFileFormatter(),
            };
        }

        public void RegistLogger(TextWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void AddFormatter(IPropertyFormatter customFomatter)
        {
            _propertyFormatters.Insert(0, customFomatter);
        }

        public void AddRangeFormatters(IEnumerable<IPropertyFormatter> customFomatters)
        {
            var propertyFormatters = customFomatters as IPropertyFormatter[] ?? customFomatters.ToArray();

            _propertyFormatters.InsertRange(0, propertyFormatters);

        }
        
        public string Format(ReportTemplate template, Type[] interfaceTypes)
        {
            var accessorTypeBuilder = new AccessorTypeBuilder();

            var interfaceReportDataCollection = interfaceTypes
                .Select(_ => accessorTypeBuilder.Parse(_))
                .Select(CreateReportData).ToArray();

            var reportData = new ReportData(interfaceReportDataCollection, _propertyFormatters);

            var tempFolders = new List<string>();
            try
            {
                // https://github.com/Antaris/RazorEngine/issues/244
                var config = new TemplateServiceConfiguration
                {
                    DisableTempFileLocking = true,
                    CachingProvider = new DefaultCachingProvider(t =>
                    {
                        tempFolders.Add(t);
                    })
                };
                RazorEngine.Engine.Razor = RazorEngineService.Create(config);
                return RazorEngine.Engine.Razor.RunCompile(template.TemplateRawText, "templateKey", typeof(ReportData),
                    reportData);
            }
            finally
            {
                foreach (var folder in tempFolders)
                {
                    try
                    {
                        Directory.Delete(folder, true);
                    }
                    catch
                    {
                        _logWriter.WriteLine($"Failed to delete temporary folder:{folder}");
                    }
                }
            }
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
