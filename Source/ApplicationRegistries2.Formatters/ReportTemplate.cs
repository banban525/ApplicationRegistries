using System.IO;
using System.Reflection;
using System.Text;

namespace ApplicationRegistries2.Formatters
{
    public class ReportTemplate
    {
        public ReportTemplate(string type, string templateRawText)
        {
            Type = type;
            TemplateRawText = templateRawText;
        }

        public string Type { get; }
        public string TemplateRawText { get; }


        public class Types
        {
            public const string Razor = "Razor";
        }

        static ReportTemplate()
        {
            string template;
            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(typeof(ReportTemplate).Namespace + ".DefaultTemplate.cshtml"))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    template = streamReader.ReadToEnd();
                }
            }
            Default = new ReportTemplate(Types.Razor, template);
        }

        public static readonly ReportTemplate Default;
    }
}
