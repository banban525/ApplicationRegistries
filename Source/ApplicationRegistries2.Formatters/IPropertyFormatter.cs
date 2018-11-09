using System.Collections.Generic;

namespace ApplicationRegistries2.Formatters
{
    public interface IPropertyFormatter
    {
        string Key { get; }
        string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration);

        string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection);

        string Title { get; }
    }
}