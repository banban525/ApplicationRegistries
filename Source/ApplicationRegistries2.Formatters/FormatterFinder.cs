using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Formatters
{
    public static class FormatterFinder
    {

        public static Type[] GetApplicationRegistriesInterfaceTypes(IEnumerable<Assembly> targetAssemblies)
        {
            return targetAssemblies.SelectMany(_ =>
                    _.GetTypes()
                        .Where(ApplicationRegistryAttribute.IsDefined)
                        .Where(type => type.IsInterface))
                .ToArray();
        }

        public static IEnumerable<IPropertyFormatter> GetFormatters(IEnumerable<Assembly> targetAssemblies)
        {
            var assemblies = targetAssemblies as Assembly[] ?? targetAssemblies.ToArray();

            var results = assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterfaces().Any(_ => _ == typeof(IPropertyFormatter)))
                .Where(type => type.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .Cast<IPropertyFormatter>()
                .ToList();

            var formatterFactories = assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterfaces().Any(_ => _ == typeof(IPropertyFormatterFactory)))
                .Where(type => type.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .Cast<IPropertyFormatterFactory>()
                .ToArray();

            results.AddRange(formatterFactories.SelectMany(_=>_.Create()));
            return results;
        }
    }
}
