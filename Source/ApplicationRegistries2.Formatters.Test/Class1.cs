using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Formatters.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void FormatNormalReport()
        {
            var formatter = new Formatter();
            var reportContent = formatter.Format(ReportTemplate.Default, new[] {typeof(ITest)});

            StringAssert.Contains(" --ITest_PortNo=", reportContent);
            StringAssert.Contains(" --ITest_SettingsFolder=", reportContent);
            StringAssert.Contains(" set ApplicationRegistries2.Formatters.Test_ITest_PortNo=", reportContent);
            StringAssert.Contains(" set ApplicationRegistries2.Formatters.Test_ITest_SettingsFolder=", reportContent);
            StringAssert.Contains(@"[Software\ApplicationRegistries\ApplicationRegistries2.Formatters.Test\ITest]", reportContent);
            StringAssert.Contains("\"PortNo\"=dword:", reportContent);
            StringAssert.Contains("\"SettingsFolder\"=text:", reportContent);
        }



        /// <summary>
        /// システム設定用のパラメータ
        /// </summary>
        [ApplicationRegistry(
            BuiltInAccessors.CommandlineArguments,
            BuiltInAccessors.EnvironmenetVariable,
            BuiltInAccessors.UserRegistry,
            BuiltInAccessors.MachineRegistry
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

        [Test]
        public void FormatReportWithUserDefinedFormatter()
        {
            var formatter = new Formatter();
            formatter.AddFormatter(new CustomFormatter());
            var reportContent = formatter.Format(ReportTemplate.Default, new[] { typeof(ICustomTest) });

            StringAssert.Contains("<h3>Custom Settings</h3>", reportContent);
            StringAssert.Contains("Summary Custom Settings", reportContent);
            StringAssert.Contains("Custom External Setting", reportContent);

        }

        [ApplicationRegistry(
            "COSTOM"
        )]
        public interface ICustomTest
        {
            /// <summary>
            /// ポートNo
            /// </summary>
            [DefaultValue(80)]
            int PortNo { get; }
        }


        public class CustomFormatter : IPropertyFormatter
        {
            public string Key => "COSTOM";
            public string Format(AccessorTypeDeclaration typeDeclaration, AccessorFieldDeclaration fieldDeclaration)
            {
                return "<h3>Custom Settings</h3>";
            }

            public string FormatSummary(IEnumerable<SummaryInterfaceReportData> typeReportCollection)
            {
                return "Summary Custom Settings";
            }

            public string Title => "Custom External Setting";
            public IAccessor LoadAccessor()
            {
                return new CustomAccessor();
            }
        }

        public class CustomAccessor : IAccessor
        {
            public object Read(Type returnType, AccessorTypeDeclaration accessorDeclaration,
                AccessorFieldDeclaration accessorFieldDeclaration)
            {
                throw new NotImplementedException();
            }

            public bool Exists(Type fieldType, AccessorTypeDeclaration accessorDeclaration,
                AccessorFieldDeclaration accessorFieldDeclaration)
            {
                throw new NotImplementedException();
            }
        }
    }
}
