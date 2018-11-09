using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace ApplicationRegistries2.Formatters.App
{
    class Program
    {
        static int Main(string[] args)
        {
            Options options;
            try
            {
                options = ParseCommandlineOptions(args);
            }
            catch (ApplicationExitException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ex.ExitCode;
            }

            if (options.Mode == Options.ExecutionMode.OutputReport)
            {
                var targetAssemblies = options.TargetAssemblies.Select(Assembly.LoadFrom).ToArray();


                var fomatter = new Formatter();
                fomatter.RegistLogger(Console.Error);

                fomatter.AddRangeFormatters(FormatterFinder.GetFormatters(targetAssemblies));

                var targetTypes = FormatterFinder.GetApplicationRegistriesInterfaceTypes(targetAssemblies);

                var reportFormatContent = ReportTemplate.Default;
                if (string.IsNullOrEmpty(options.FormatFilePath) == false)
                {
                    reportFormatContent = new ReportTemplate(
                        ReportTemplate.Types.Razor,
                        File.ReadAllText(options.FormatFilePath, Encoding.UTF8));
                }


                var content = fomatter.Format(reportFormatContent, targetTypes);

                File.WriteAllText(options.OutputPath, content, Encoding.UTF8);
            }

            if (options.Mode == Options.ExecutionMode.OutputTemplate)
            {
                File.WriteAllText(options.TemplateOutputPath, ReportTemplate.Default.TemplateRawText);
            }

            return 0;
        }

        private static Options ParseCommandlineOptions(string[] args)
        {
            Options options;

            var exeName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            var usage = Options.GetUsageHelpMessage(exeName);
            var commandHelp = Options.GetCommandHelpMessage();

            try
            {
                options = Options.Parse(args);

                if (options.Mode == Options.ExecutionMode.ShowHelp)
                {
                    var exeVersion = Assembly.GetExecutingAssembly().GetName().Version;

                    var message = new StringWriter();
                    message.WriteLine($"{exeName} ver.{exeVersion.Major}.{exeVersion.Minor}");
                    message.WriteLine("");
                    message.WriteLine("Usage:");
                    message.WriteLine(string.Join("\n", usage.Select(_ => "  " + _)));
                    message.WriteLine("");
                    message.WriteLine("Commandline arguments");
                    message.WriteLine(commandHelp);
                    throw new ApplicationExitException(0, message.ToString());
                }
            }
            catch (ArgumentException ex)
            {
                var message = new StringWriter();
                message.WriteLine(ex.Message);
                message.WriteLine("");
                message.WriteLine("Usage:");
                message.WriteLine(string.Join("\n", usage.Select(_ => "  " + _)));
                message.WriteLine("");
                message.WriteLine("Commandline arguments");
                message.WriteLine(commandHelp);
                throw new ApplicationExitException(1, message.ToString());
            }

            if (options.Mode == Options.ExecutionMode.OutputReport)
            {
                if (options.TargetAssemblies == null)
                {
                    throw new ArgumentException("\"-i\" or \"--input\" argument expected.");
                }
                if (options.OutputPath == null)
                {
                    throw new ArgumentException("\"-o\" or \"--output\" argument expected.");
                }

                if (options.TargetAssemblies.All(File.Exists) == false)
                {
                    var message = new StringWriter();
                    message.WriteLine(
                        $"input assemblies are not found: \n{string.Join("\n", options.TargetAssemblies)}");

                    throw new ApplicationExitException(1, message.ToString());
                }

                var outputDir = Path.GetDirectoryName(options.OutputPath);
                if (string.IsNullOrEmpty(outputDir) && Directory.Exists(outputDir) == false)
                {
                    var message = new StringWriter();
                    message.WriteLine($"output directory is not found: {options.OutputPath}");

                    throw new ApplicationExitException(1, message.ToString());
                }


                if (string.IsNullOrEmpty(options.FormatFilePath) == false &&
                    File.Exists(options.FormatFilePath) == false)
                {
                    var message = new StringWriter();
                    message.WriteLine($"report format file is not found: {options.TargetAssemblies}");

                    throw new ApplicationExitException(1, message.ToString());
                }
            }

            if (options.Mode == Options.ExecutionMode.OutputTemplate)
            {
                var outputDir = Path.GetDirectoryName(options.TemplateOutputPath);
                if (string.IsNullOrEmpty(outputDir) && Directory.Exists(outputDir) == false)
                {
                    var message = new StringWriter();
                    message.WriteLine($"output directory is not found: {options.TemplateOutputPath}");

                    throw new ApplicationExitException(1, message.ToString());
                }
            }

            return options;
        }
    }

    [Serializable]
    class ApplicationExitException : Exception
    {
        public int ExitCode { get; set; }
        public ApplicationExitException()
        {
        }
        public ApplicationExitException(int exitCode)
        {
            ExitCode = exitCode;
        }

        public ApplicationExitException(int exitCode,string message) : base(message)
        {
            ExitCode = exitCode;
        }

        public ApplicationExitException(int exitCode,string message, Exception inner) : base(message, inner)
        {
            ExitCode = exitCode;
        }

        protected ApplicationExitException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
            
        }
    }

    class Options
    {
        public Options(string[] targetAssemblies, string outputPath,string formatFilePath, string templateOutputPath, ExecutionMode mode)
        {
            TargetAssemblies = targetAssemblies;
            OutputPath = outputPath;
            FormatFilePath = formatFilePath;
            TemplateOutputPath = templateOutputPath;
            Mode = mode;
        }

        public string[] TargetAssemblies { get; }
        public string OutputPath { get; }
        public string FormatFilePath { get; }
        public string TemplateOutputPath { get; }

        internal enum ExecutionMode
        {
            OutputReport,
            OutputTemplate,
            ShowHelp
        }

        public ExecutionMode Mode { get; }

        public static Options Parse(string[] args)
        {
            string[] targetAssemblies = null;
            string outputPath = null;
            var index = 0;
            string formatFilePath = null;
            string templateOutputPath = null;
            var executionMode = ExecutionMode.OutputReport;

            while (index < args.Length)
            {
                var arg = args[index];

                if (arg.StartsWith("--input="))
                {
                    targetAssemblies = arg.Substring("--input=".Length).Split(',').Select(_ => _.Trim()).ToArray();
                }
                else if (arg == "-i")
                {
                    if (index + 1 < args.Length)
                    {
                        targetAssemblies = args[index + 1].Split(',').Select(_ => _.Trim()).ToArray();
                        index++;
                    }
                    else
                    {
                        throw new ArgumentException("assembly list expected for argument \"-i\"");
                    }
                }
                else if (arg.StartsWith("--output="))
                {
                    outputPath = arg.Substring("--output=".Length).Trim();
                }
                else if (arg == "-o")
                {
                    if (index + 1 < args.Length)
                    {
                        outputPath = args[index + 1].Trim();
                        index++;
                    }
                    else
                    {
                        throw new ArgumentException("output path expected for argument \"-o\"");
                    }
                }
                else if (arg.StartsWith("--template="))
                {
                    templateOutputPath = arg.Substring("--template=".Length).Trim();
                    executionMode = ExecutionMode.OutputTemplate;
                }
                else if (arg.StartsWith("-t"))
                {
                    if (index + 1 < args.Length)
                    {
                        templateOutputPath = args[index + 1].Trim();
                        executionMode = ExecutionMode.OutputTemplate;
                        index++;
                    }
                    else
                    {
                        throw new ArgumentException("template output path expected for argument \"-t\"");
                    }
                }
                else if (arg == "-f")
                {
                    if (index + 1 < args.Length)
                    {
                        formatFilePath = args[index + 1].Trim();
                        index++;
                    }
                    else
                    {
                        throw new ArgumentException("format file path expected for argument \"-f\"");
                    }
                }
                else if (arg == "--format=")
                {
                    formatFilePath = arg.Substring("--format=".Length).Trim();
                }
                else if (arg == "-h" || arg == "-?" || arg == "--help" || arg == "/?")
                {
                    executionMode = ExecutionMode.ShowHelp;
                }

                index++;
            }
            
            return new Options(targetAssemblies, outputPath, formatFilePath,templateOutputPath, executionMode);
        }

        public static string[] GetUsageHelpMessage(string exeName)
        {
            return new[]{
                $@"{exeName} --input=<assembly list> --output=<html file path>",
                $@"{exeName} -i <assembly list> -o <html file path>",
                $@"{exeName} --template=<report template output path>",
            };
        }
        public static string GetCommandHelpMessage()
        {
            return @"
-i,--input    (required) input assembly file paths.(comma separated)
-o,--output   (required) output html file path.
-f,--format   report template file path.
-t,--template export default report template file. specified output path.
-h,-?,--help  show help.
";
        }
    }

}
