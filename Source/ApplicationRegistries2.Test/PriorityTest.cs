using System;
using System.IO;
using System.Reflection;
using System.Text;
using ApplicationRegistries2.Attributes;
using Microsoft.Win32;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class PriorityTest
    {
        private string _defaultXmlFilePath;
        [SetUp]
        public void SetUp()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dir == null)
            {
                throw new InvalidOperationException();
            }
            _defaultXmlFilePath = Path.Combine(dir,"ApplicationRegisties.xml");
            Environment.SetEnvironmentVariable("ApplicationRegistries2.Test_IDefaultAttributeRegistry_ListenPortNo", null);
            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(null);

        }
        [TearDown]
        public void TearDown()
        {
            File.Delete(_defaultXmlFilePath);
            Registry.CurrentUser.DeleteSubKeyTree(@"Software\ApplicationRegistries\ApplicationRegistries2.Test", false);

            Environment.SetEnvironmentVariable("ApplicationRegistries2.Test_IDefaultAttributeRegistry_ListenPortNo", null);

            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(null);

        }

        [Test]
        public void キーが指定されない場合はデフォルトの優先順位になる()
        {
            var listenPortNo = ApplicationRegistry.Get<IDefaultAttributeRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(80), "外部設定がない場合はデフォルト値が使われる");

            var xmlContents = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ApplicationRegisties>
    <IDefaultAttributeRegistry>
        <ListenPortNo>81</ListenPortNo>
    </IDefaultAttributeRegistry>
</ApplicationRegisties>
             ";

            File.WriteAllText(_defaultXmlFilePath, xmlContents, Encoding.UTF8);

            listenPortNo = ApplicationRegistry.Get<IDefaultAttributeRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(81), "XMLファイルがあればそれに従う");

            using (var key =
                Registry.CurrentUser.CreateSubKey(@"Software\ApplicationRegistries\ApplicationRegistries2.Test\IDefaultAttributeRegistry"))
            {
                // ReSharper disable once PossibleNullReferenceException
                key.SetValue("ListenPortNo", 82, RegistryValueKind.DWord);
            }


            listenPortNo = ApplicationRegistry.Get<IDefaultAttributeRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(82), "ユーザーレジストリがあれば、XMLを上書きする");

            Environment.SetEnvironmentVariable("ApplicationRegistries2.Test_IDefaultAttributeRegistry_ListenPortNo", "83");

            listenPortNo = ApplicationRegistry.Get<IDefaultAttributeRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(83), "環境変数があれば、ユーザーレジストリを上書きする");

            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(new[]
            {
                "--IDefaultAttributeRegistry_ListenPortNo=84"
            });
            listenPortNo = ApplicationRegistry.Get<IDefaultAttributeRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(84), "コマンドライン引数があれば、環境変数を上書きする");
        }
    }

    [ApplicationRegistry]
    public interface IDefaultAttributeRegistry
    {
        /// <summary>
        /// 待ち受けポートNo
        /// </summary>
        [DefaultValue(80)]
        int ListenPortNo { get; }

        /// <summary>
        /// 設定ファイルが格納されているフォルダ
        /// </summary>
        [DefaultValue("")]
        string TestName { get; }
    }
    
}
