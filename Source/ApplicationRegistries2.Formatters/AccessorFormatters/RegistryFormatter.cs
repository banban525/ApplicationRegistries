using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    class RegistryFormatter: IPropertyFormatter
    {
        private RegistoryAccessor.RegistryRoot _root;

        public RegistryFormatter(RegistoryAccessor.RegistryRoot root)
        {
            _root = root;
        }

        public string Key => _root == RegistoryAccessor.RegistryRoot.LocalMachine
            ? BuiltInAccessors.MachineRegistry
            : BuiltInAccessors.UserRegistry;
        public string Title => _root == RegistoryAccessor.RegistryRoot.LocalMachine ? 
            Properties.Resources.MachineRegistryFormatter_Title: Properties.Resources.UserRegistryFormatter_Title;

        public string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration)
        {
            var data =
                RegistoryAccessor.GetPropertyData(
                    _root, typeDeclaration, fieldDeclaration);

            var exampleValue = "";
            if (fieldDeclaration.Type == typeof(int))
            {
                exampleValue = "dword:(Value)";
            }
            else if (fieldDeclaration.Type == typeof(string))
            {
                exampleValue = "text:(Value)";
            }


            var result = $@"
<h3>{Title}</h3>
<div class=""registry"">
  <pre><code>[{data.Key}]
""{data.ValueName}""={exampleValue}</code></pre>
</div>";
            return result;
        }


        public string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection)
        {
            var result = "";
            foreach (var interfaceReportData in typeReportCollection)
            {
                var key = "";
                var fieldReports = interfaceReportData.Properties.Select(propertyReportData =>
                {
                    var reportData =
                        RegistoryAccessor.GetPropertyData(
                            _root, interfaceReportData.TypeDeclaration, propertyReportData.FieldDeclaration);
                    key = reportData.Key;

                    var typeDescription = "";
                    if (propertyReportData.FieldDeclaration.Type == typeof(int))
                    {
                        typeDescription = "dword";
                    }
                    else if (propertyReportData.FieldDeclaration.Type == typeof(string))
                    {
                        typeDescription = "text";
                    }

                    return $@"
    <tr>
        <td>{reportData.ValueName}</td>
        <td>{typeDescription}</td>
        <td>{propertyReportData.Description}</td>
    </tr>";
                }).ToArray();

                

                result += $@"
<h2>{key}</h2>

<p>{interfaceReportData.Description}</p>

<table class=""registry"">
    <tbody>
    <tr>
        <th>Name</th>
        <th>type</th>
        <th>description</th>
    </tr>
{string.Join("", fieldReports)}
    </tbody>
</table>

";
            }

            return result;
        }
    }
}