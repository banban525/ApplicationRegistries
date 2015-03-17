using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using CommandLine;

namespace ApplicationRegistries.Generator
{
    class Program
    {
        private static int Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options) == false)
            {
                return 1;
            }

            var defineXmlPath = new FileInfo(options.DefineFile).FullName;
            var entries = Entries.Parse(defineXmlPath);

            if (options.EscapeBackSlash)
            {
                entries = entries.ReplaceAll("\\", "\\\\");
            }

            var outputObject = new OutputObject()
            {
                ClassName = options.ClassName,
                Namespace = options.Namespace,
                Entries = entries,
                InputXml = File.ReadAllText(defineXmlPath).Replace("\"","\"\""),
            };

            TemplateInfo templateInfo = null;
            if (options.Mode == Mode.Code)
            {
                var templateText = LoadCsCodeTemplate();
                var lastWriteTime = (new FileInfo(Assembly.GetExecutingAssembly().Location)).LastWriteTime;
                templateInfo = new TemplateInfo(templateText, lastWriteTime);
            }
            else if(options.Mode == Mode.Rst)
            {
                templateInfo = LoadTemplateFromFolder("rst", null);
            }
            else if (options.Mode == Mode.md)
            {
                templateInfo = LoadTemplateFromFolder("md", null);
            }
            else if(options.Mode == Mode.Other)
            {
                templateInfo = LoadTemplateFromFolder(options.TemplateName, options.TemplateFilePath);
            }
            else
            {
                throw new ArgumentException();
            }
            if (templateInfo == null)
            {
                throw new FileNotFoundException();
            }

            var outputFilePath = new FileInfo(options.Output).FullName;

            if (NeedsGenerateOutput(outputFilePath, defineXmlPath, templateInfo) == false)
            {
                Console.WriteLine("Not output becuase template file is older than output file.");
                return 0;
            }

            var template = new Antlr4.StringTemplate.Template(templateInfo.Content, '$', '$');
            template.Add("_output", outputObject);
            var output = template.Render();
            File.WriteAllText(outputFilePath, output, Encoding.UTF8);

            return 0;
        }

        private static bool NeedsGenerateOutput(string outputFilePath, string defineXmlPath, TemplateInfo templateInfo)
        {
            if (File.Exists(outputFilePath) == false)
            {
                return true;
            }
            var outputLastWriteTime = (new FileInfo(outputFilePath)).LastWriteTime;
            
            if (outputLastWriteTime < templateInfo.LastUpdated)
            {
                return true;
            }

            var defineLastWriteTime = (new FileInfo(defineXmlPath)).LastWriteTime;
            if (outputLastWriteTime < defineLastWriteTime)
            {
                return true;
            }
            
            var assemblyLastWriteTime = (new FileInfo(Assembly.GetExecutingAssembly().Location)).LastWriteTime;
            if (outputLastWriteTime < assemblyLastWriteTime)
            {
                return true;
            }
            return false;
        }

        class TemplateInfo
        {
            public TemplateInfo(string content, DateTime lastUpdated)
            {
                Content = content;
                LastUpdated = lastUpdated;
            }

            public string Content { get; private set; }
            public DateTime LastUpdated { get; private set; }
        }

        private static TemplateInfo LoadTemplateFromFolder(string key, string templateFilePath)
        {
            if (string.IsNullOrEmpty(templateFilePath) == false && File.Exists(templateFilePath))
            {
                return new TemplateInfo(File.ReadAllText(templateFilePath), new FileInfo(templateFilePath).LastWriteTime);
            }

            var templatesFolders = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyTemplates")
            };

            foreach (var templatesFolder in templatesFolders)
            {
                var templatePath = Path.Combine(templatesFolder, key + ".st");
                if (File.Exists(templatePath) == false)
                {
                    throw new FileNotFoundException();
                }
                var lastUdpated = new FileInfo(templatePath).LastWriteTime;

                return new TemplateInfo(File.ReadAllText(templatePath), lastUdpated);
            }
            throw new FileNotFoundException();
        }

        private static string LoadCsCodeTemplate()
        {
            string templateCsCodeText;
            using (var templateStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                typeof (Program).Namespace + ".CsCode.st"))
            {
                templateCsCodeText = (new StreamReader(templateStream)).ReadToEnd();
            }
            return templateCsCodeText;
        }
    }
}
