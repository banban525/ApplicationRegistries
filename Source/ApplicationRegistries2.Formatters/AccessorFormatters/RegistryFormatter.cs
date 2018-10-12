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
            "レジストリ(HKLM)":"レジストリ(HKCU)";

        public string Format(AccessorDefinition definition, AccessorFieldDefinition field, IPropertyAccessorReportData reportData)
        {
            var data = (RegistoryAccessor.RegistryAccessorReportData)reportData;

            var title = _root == RegistoryAccessor.RegistryRoot.LocalMachine ? "レジストリ(HKLM)" : "レジストリ(HKCU)";

            var exampleValue = "";
            if (field.Type == typeof(int))
            {
                exampleValue = "dword:(数値)";
            }
            else if (field.Type == typeof(string))
            {
                exampleValue = "text:(文字列)";
            }


            var result = $@"
<h3>{title}</h3>
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
                        (RegistoryAccessor.RegistryAccessorReportData) propertyReportData.PropertyReportData;
                    key = reportData.Key;

                    var typeDescription = "";
                    if (propertyReportData.FildDefinition.Type == typeof(int))
                    {
                        typeDescription = "dword";
                    }
                    else if (propertyReportData.FildDefinition.Type == typeof(string))
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