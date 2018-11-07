using System.Collections.Generic;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters.AccessorFormatters
{
    public interface IPropertyFormatter
    {
        string Key { get; }
        string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration);

        string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection);

        string Title { get; }

        IAccessor LoadAccessor();
    }
}