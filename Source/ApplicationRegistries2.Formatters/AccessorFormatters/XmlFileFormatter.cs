﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    class XmlFileFormatter: IPropertyFormatter
    {
        public string Key => BuiltInAccessors.XmlFile;
        public string Title => Properties.Resources.XmlFileFormatter_Title;

        public string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration, IPropertyAccessorReportData reportData)
        {
            var data = (XmlFileAccessor.XmlFileAccessorReportData)reportData;

            var exampleValue = "";
            if (fieldDeclaration.Type == typeof(int))
            {
                exampleValue = "integer";
            }
            else if (fieldDeclaration.Type == typeof(string))
            {
                exampleValue = "string";
            }

            var result = $@"
<h3>{Title}</h3>
<div class=""registry"">
  <pre><code>{data.FilePath}
XPath: {data.XmlRootPath}
ValuePath: {data.XmlValuePath}
Type: {exampleValue}
</code></pre>
</div>";
            return result;
        }


        public string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection)
        {

            var result = "";
            foreach (var interfaceReportData in typeReportCollection)
            {
                var typeData =
                    (XmlFileAccessor.XmlFileInterfaceAccessorReportData)interfaceReportData.ReportData;

                var fieldReports = interfaceReportData.Properties.Select(propertyReportData =>
                {
                    var reportData =
                        (XmlFileAccessor.XmlFileAccessorReportData)propertyReportData.PropertyReportData;

                    return $@"
    <tr>
        <td>{reportData.XmlValuePath}</td>
        <td>{propertyReportData.FieldDeclaration.Type.Name}</td>
        <td>{propertyReportData.Description}</td>
    </tr>";
                }).ToArray();

                result += $@"
<h2>{typeData.FilePath}</h2>

<p>XPath: {typeData.XmlRootPath}</p>

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
