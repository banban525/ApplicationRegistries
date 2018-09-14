using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class XmlFileTest
    {
        private string _defaultXmlFilePath;
        private string _otherXmlFilePath;
        [SetUp]
        public void SetUp()
        {
            _defaultXmlFilePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                "ApplicationRegisties.xml");
            _otherXmlFilePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                "OtherApplicationRegisties.xml");
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_defaultXmlFilePath);
        }

        [Test]
        public void Test()
        {
            var xmlContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ApplicationRegisties>
    <IXmlFileRegistry>
        <ListenPortNo>80</ListenPortNo>
        <TestName>{TestContext.CurrentContext.Test.Name}</TestName>
    </IXmlFileRegistry>
</ApplicationRegisties>
             ";

            File.WriteAllText(_defaultXmlFilePath, xmlContents, Encoding.UTF8);
            

            var listenPortNo = ApplicationRegistry.Get<IXmlFileRegistry>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(80));

            var testName = ApplicationRegistry.Get<IXmlFileRegistry>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }



        [ApplicationRegistry(BuiltInAccessors.XmlFile)]
        public interface IXmlFileRegistry
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
        public void 別のXmlファイルの指定()
        {
            var xmlContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ApplicationRegisties>
    <IXmlFileRegistryWithOtherFile>
        <PortNo>81</PortNo>
        <TestName>{TestContext.CurrentContext.Test.Name}</TestName>
    </IXmlFileRegistryWithOtherFile>
</ApplicationRegisties>
             ";
            
            File.WriteAllText(_otherXmlFilePath, xmlContents, Encoding.UTF8);


            var listenPortNo = ApplicationRegistry.Get<IXmlFileRegistryWithOtherFile>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(81));

            var testName = ApplicationRegistry.Get<IXmlFileRegistryWithOtherFile>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }


        [ApplicationRegistry(BuiltInAccessors.XmlFile)]
        [XmlFile("OtherApplicationRegisties.xml")]
        public interface IXmlFileRegistryWithOtherFile
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [XmlName("PortNo")]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }


        [Test]
        public void XMLのルートパスを変える()
        {
            var xmlContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<XmlFileRegistryTest>
    <PortNo>82</PortNo>
    <TestName>{TestContext.CurrentContext.Test.Name}</TestName>
</XmlFileRegistryTest>
             ";

            File.WriteAllText(_otherXmlFilePath, xmlContents, Encoding.UTF8);


            var listenPortNo = ApplicationRegistry.Get<IXmlFileRegistryWithOtherFileAndOtherRoot>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(82));

            var testName = ApplicationRegistry.Get<IXmlFileRegistryWithOtherFileAndOtherRoot>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }



        [ApplicationRegistry(BuiltInAccessors.XmlFile)]
        [XmlFile("OtherApplicationRegisties.xml", "/XmlFileRegistryTest")]
        public interface IXmlFileRegistryWithOtherFileAndOtherRoot
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [XmlName("PortNo")]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }
    }
}
