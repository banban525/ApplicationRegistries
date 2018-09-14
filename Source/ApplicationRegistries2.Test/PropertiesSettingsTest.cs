using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class PropertiesSettingsTest
    {
        [Test]
        public void 通常アクセス()
        {
            var listenPortNo = ApplicationRegistry.Get<IPropertiesSettingsRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(Properties.Settings.Default.ListenPortNo));

            Properties.Settings.Default.TestName = TestContext.CurrentContext.Test.Name;
            Properties.Settings.Default.Save();

            var testName = ApplicationRegistry.Get<IPropertiesSettingsRegistry>().TestName;
            Assert.That(testName, Is.EqualTo(Properties.Settings.Default.TestName));
        }



        [ApplicationRegistry(BuiltInAccessors.PropertiesSettings)]
        [PropertiesSettingsType(typeof(Properties.Settings))]
        public interface IPropertiesSettingsRegistry
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }


        [Test]
        public void プロパティ名の変更()
        {
            var listenPortNo = ApplicationRegistry.Get<IPropertiesSettingsRegistryWithOtherPropertyName>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(Properties.Settings.Default.PortNo));
        }



        [ApplicationRegistry(BuiltInAccessors.PropertiesSettings)]
        [PropertiesSettingsType(typeof(Properties.Settings))]
        public interface IPropertiesSettingsRegistryWithOtherPropertyName
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [PropertiesSettingsName("PortNo")]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }

    }
}
