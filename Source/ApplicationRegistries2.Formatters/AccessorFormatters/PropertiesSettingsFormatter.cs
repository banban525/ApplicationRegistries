using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    class PropertiesSettingsFormatter : IPropertyFormatter
    {
        public string Key => BuiltInAccessors.PropertiesSettings;
        public string Title => "Properties.Settings";

        public string Format(AccessorDefinition definition, AccessorFieldDefinition field, IPropertyAccessorReportData reportData)
        {
            var data = (PropertiesSettingsAccessor.PropertiesSettingsPropertyData)reportData;

            var exampleValue = "";
            if (field.Type == typeof(int))
            {
                exampleValue = "(integer)";
            }
            else if (field.Type == typeof(string))
            {
                exampleValue = "(string)";
            }

            var result = $@"
<h3>{Title}</h3>
<div class=""registry"">
  <pre><code>{data.Parent.FullName}.{data.Name} = {exampleValue};</code></pre>
</div>";
            return result;
        }


        public string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection)
        {

            var result = "";
            foreach (var interfaceReportData in typeReportCollection)
            {
                var typeData =
                    (PropertiesSettingsAccessor.PropertiesSettingsnterfaceData) interfaceReportData.ReportData;
                
                var fieldReports = interfaceReportData.Properties.Select(propertyReportData =>
                {
                    var reportData =
                        (PropertiesSettingsAccessor.PropertiesSettingsPropertyData)propertyReportData.PropertyReportData;

                    return $@"
    <tr>
        <td>{reportData.Name}</td>
        <td>{propertyReportData.FildDefinition.Type.Name}</td>
        <td>{propertyReportData.Description}</td>
    </tr>";
                }).ToArray();

                result += $@"
<h2>{typeData.Parent.FullName}</h2>

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
