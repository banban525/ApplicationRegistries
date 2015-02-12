using CommandLine;
using CommandLine.Text;

namespace ApplicationRegistries.Generator
{
    class Options
    {
        [Option('m', "mode", DefaultValue = Mode.Code, HelpText="Select Mode for generation type.", Required = true)]
        public Mode Mode { get; set; }
        [Option('o', "output", HelpText = "Output file path.", Required = true)]
        public string Output { get; set; }
        [Option('i', "input", HelpText = "Input file path.", Required = true)]
        public string DefineFile { get; set; }
        [Option('c', "classname", DefaultValue = "Registries", HelpText = "class name for *.cs Code")]
        public string ClassName { get; set; }
        [Option('n', "namespace", DefaultValue = "ApplicationRegistries", HelpText = "namespace for *.cs Code.")]
        public string Namespace { get; set; }
        [Option('t', "template", DefaultValue = "", HelpText = "template name for --Mode Other")]
        public string TemplateName { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText { AddDashesToOption = true };

            help.AddPreOptionsLine("Usage: ApplicationRegistries.Generator.exe <options> ");
            help.AddPreOptionsLine("");
            help.AddPreOptionsLine("options:");
            help.AddOptions(this);

            return help;
        }
    }
}