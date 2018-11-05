using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Formatters.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test()
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
}
