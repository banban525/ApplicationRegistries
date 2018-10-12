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
            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                         throw new InvalidOperationException();

            var outputPath = "output.html";
            var targetAssemblyPaths = new string[]
            {
                Assembly.GetExecutingAssembly().Location
            };


            var defaultTemplateFilePath = Path.Combine(exeDir,
                "DefaultTemplate.cshtml");

            var defaultTemplate = File.ReadAllText(defaultTemplateFilePath, Encoding.UTF8);

            var targetAssemblies = targetAssemblyPaths.Select(Assembly.LoadFrom).ToArray();

            var targetTypes = targetAssemblies.SelectMany(_ =>
                    _.GetTypes()
                        .Where(ApplicationRegistryAttribute.IsDefined)
                        .Where(type => type.IsInterface))
                .ToArray();
            

            var fomatter = new Formatter();
            
            var content = fomatter.Format(defaultTemplate, targetTypes);
            

            File.WriteAllText(outputPath, content, Encoding.UTF8);
        }
    }

    /// <summary>
    /// システム設定用のパラメータ
    /// </summary>
    [ApplicationRegistry(
        BuiltInAccessors.CommandlineArguments,
        BuiltInAccessors.EnvironmenetVariable,
        BuiltInAccessors.UserRegistry,
        BuiltInAccessors.MachineRegistry,
        BuiltInAccessors.DefaultValue
    )]
    public interface ITest
    {
        /// <summary>
        /// ポートNo
        /// </summary>
        [DefaultValue(80)]
        int PortNo { get; }

        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        [DefaultValue(@"c:\ProgramData\ApplicationRegistries2")]
        string SettingsFolder { get; }
    }

}
