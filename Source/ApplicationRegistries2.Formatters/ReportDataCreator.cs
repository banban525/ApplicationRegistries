using System;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2.Formatters
{
    //public class ReportDataCreator
    //{
    //    public ReportData Create(Type[] interfaceTypes)
    //    {
    //        var accessorDefinitions  = interfaceTypes.Select(_ =>
    //            new AccessorTypeBuilder().Parse(_, _accessorRepository));


    //        return new ReportData(
    //            accessorDefinitions.Select(definition => new InterfaceReportData(
    //                definition,
    //                definition.Fields.Select(field => new PropertyReportData(
    //                    field, 
    //                    definition.AccessToList.Select(accessor=>accessor.GetPropertyData(definition, field)).ToArray()
    //                )).ToArray()
    //            )).ToArray()
    //        );
    //    }

    //    public void RegistCustomAccessor(string key, IAccessor accessor)
    //    {
    //        _accessorRepository.RegistCustomAccessor(key, accessor);
    //    }

    //    private readonly AccessorRepository _accessorRepository = new AccessorRepository();
    //    private readonly RepositoryAccessorCache _cache = new RepositoryAccessorCache();
    //}
}