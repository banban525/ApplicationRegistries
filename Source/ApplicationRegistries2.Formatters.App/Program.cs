using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Formatters.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputPath = "output.html";
            var targetAssemblyPaths = new string[]
            {
            };
      

            var targetAssemblies = targetAssemblyPaths.Select(Assembly.LoadFrom).ToArray();

            var targetTypes = targetAssemblies.SelectMany(_ =>
                    _.GetTypes()
                        .Where(ApplicationRegistryAttribute.IsDefined)
                        .Where(type => type.IsInterface))
                .ToArray();
            

            var fomatter = new Formatter();
            
            var content = fomatter.Format(ReportTemplate.Default, targetTypes);
            

            File.WriteAllText(outputPath, content, Encoding.UTF8);
        }

        

    }

    class Options
    {
        public Options(string[] targetAssemblies, string outputPath)
        {
            TargetAssemblies = targetAssemblies;
            OutputPath = outputPath;
        }

        public string[] TargetAssemblies { get; }
        public string OutputPath { get; }
    }

}
