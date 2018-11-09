using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    class PropertiesSettingsFormatter : IPropertyFormatter
    {
        public string Key => BuiltInAccessors.PropertiesSettings;
        public string Title => "Properties.Settings";

        public string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration)
        {
            var data = PropertiesSettingsAccessor.GetPropertyData(typeDeclaration, fieldDeclaration);

            var exampleValue = "";
            if (fieldDeclaration.Type == typeof(int))
            {
                exampleValue = "(integer)";
            }
            else if (fieldDeclaration.Type == typeof(string))
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

                var typeData = PropertiesSettingsAccessor.GetInterfaceData(interfaceReportData.TypeDeclaration);
                
                var fieldReports = interfaceReportData.Properties.Select(propertyReportData =>
                {
                    var reportData = PropertiesSettingsAccessor.GetPropertyData(interfaceReportData.TypeDeclaration, propertyReportData.FieldDeclaration);

                    return $@"
    <tr>
        <td>{reportData.Name}</td>
        <td>{propertyReportData.FieldDeclaration.Type.Name}</td>
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
