using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    public class CommandlineArgumentFormatter: IPropertyFormatter
    {
        public string Key => BuiltInAccessors.CommandlineArguments;
        public string Title => Properties.Resources.CommandlineArgumentFormatter_Title;

        public string Format(AccessorDefinition definition, AccessorFieldDefinition field, IPropertyAccessorReportData reportData)
        {
            var data = (CommandlineArgumentsAccessor.CommandlineArgumentsAccessorReportData)reportData;

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
<div class=""commandline"">
  <pre><code> {data.CommandlineArgumentName}={exampleValue} </code></pre>
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
                    (CommandlineArgumentsAccessor.CommandlineArgumentsAccessorReportData) propertyReportData
                        .PropertyReportData;

                return $"<tr><td>{data.CommandlineArgumentName}</td><td>{propertyReportData.Description}</td></tr>\r\n";

            });

            var result = $@"
    <table class=""registry"">
        <tbody>
        <tr>
            <th>Name</th>
            <th>description</th>
        </tr>
        {string.Join("",list)}
        </tbody>
    </table>";
            return result;
            
        }

    }
}
