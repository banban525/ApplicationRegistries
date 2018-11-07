using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    public class CommandlineArgumentFormatter: IPropertyFormatter
    {
        public string Key => BuiltInAccessors.CommandlineArguments;
        public string Title => Properties.Resources.CommandlineArgumentFormatter_Title;
        public IAccessor LoadAccessor()
        {
            return new CommandlineArgumentsAccessor();
        }

        public string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration)
        {
            var data = CommandlineArgumentsAccessor.GetPropertyData(typeDeclaration, fieldDeclaration);

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
<div class=""commandline"">
  <pre><code> {data.CommandlineArgumentName}={exampleValue} </code></pre>
</div>";
            return result;
        }

        public string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection)
        {
            var list = typeReportCollection
                .SelectMany(_ => _.Properties.Select(prop=>new{_.TypeDeclaration, PropertyReportData=prop}))
                .Select(_ =>
            {
                var typeDeclaration = _.TypeDeclaration;
                var propertyReportData = _.PropertyReportData;

                var data = CommandlineArgumentsAccessor.GetPropertyData(typeDeclaration,
                    propertyReportData.FieldDeclaration);

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
