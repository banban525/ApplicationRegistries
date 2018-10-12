using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    class EnvironmentVariableFormatter: IPropertyFormatter
    {
        public string Key => BuiltInAccessors.EnvironmenetVariable;
        public string Title => "環境変数";

        public string Format(AccessorDefinition definition, AccessorFieldDefinition field, IPropertyAccessorReportData reportData)
        {
            var data = (EnvironmentVariableAccessor.EnvironmentVariableAccessorReportData)reportData;

            var exampleValue = "";
            if (field.Type == typeof(int))
            {
                exampleValue = "(数値)";
            }
            else if (field.Type == typeof(string))
            {
                exampleValue = "(文字列)";
            }

            var result = $@"
<div class=""commandline"">
  <pre><code> set {data.EnvironmentVariableName}={exampleValue} </code></pre>
</div>";
            return result;
        }

        public string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection)
        {
            var list = typeReportCollection
                .SelectMany(_ => _.Properties)
                .Select(propertyReportData =>
            {
                var data =
                    (EnvironmentVariableAccessor.EnvironmentVariableAccessorReportData)propertyReportData.PropertyReportData;

                return $"<tr><td>{data.EnvironmentVariableName}</td><td>{propertyReportData.Description}</td></tr>\r\n";

            });

            var result = $@"
    <table class=""registry"">
        <tbody>
        <tr>
            <th>Name</th>
            <th>description</th>
        </tr>
        {string.Join("", list)}
        </tbody>
    </table>";
            return result;

        }

    }
}
