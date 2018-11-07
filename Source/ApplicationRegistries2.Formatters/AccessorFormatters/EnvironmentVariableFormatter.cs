using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    class EnvironmentVariableFormatter: IPropertyFormatter
    {
        public string Key => BuiltInAccessors.EnvironmenetVariable;
        public string Title => Properties.Resources.EnvironmentVariableFormatter_Title;
        public IAccessor LoadAccessor()
        {
            return new EnvironmentVariableAccessor();
        }

        public string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration)
        {
            var data = EnvironmentVariableAccessor.GetPropertyData(typeDeclaration, fieldDeclaration);

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
  <pre><code> set {data.EnvironmentVariableName}={exampleValue} </code></pre>
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
                var data = EnvironmentVariableAccessor.GetPropertyData(typeDeclaration, propertyReportData.FieldDeclaration);

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
