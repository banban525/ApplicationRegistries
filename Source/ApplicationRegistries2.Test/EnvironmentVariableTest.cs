using System;
using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class EnvironmentVariableTest
    {
        [Test]
        public void デフォルトの挙動()
        {
            Environment.SetEnvironmentVariable("ApplicationRegistries2.Test_IEnvironmentVariableRegistry_ListenPortNo", "80");

            var listenPortNo = ApplicationRegistry.Get<IEnvironmentVariableRegistry>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(80));

            Environment.SetEnvironmentVariable("ApplicationRegistries2.Test_IEnvironmentVariableRegistry_TestName", TestContext.CurrentContext.Test.Name);

            var testName = ApplicationRegistry.Get<IEnvironmentVariableRegistry>().TestName;

            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }

        [Test]
        public void データが見つからない場合は例外が発生する()
        {
            Assert.Throws<DataNotFoundException>(() =>
            {
                Environment.SetEnvironmentVariable("ApplicationRegistries2.Test_IEnvironmentVariableRegistry_ListenPortNo", null);

                // ReSharper disable once UnusedVariable
                var listenPortNo = ApplicationRegistry.Get<IEnvironmentVariableRegistry>().ListenPortNo;
            });

        }

        [Test]
        public void Prefixが変更された場合はそれに従う()
        {
            Environment.SetEnvironmentVariable("ApplicationRegistoryTest_PortNo", "81");

            var listenPortNo = ApplicationRegistry.Get<IEnvironmentVariableRegistryWithPrefix>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(81));

            Environment.SetEnvironmentVariable("ApplicationRegistoryTest_TestName", TestContext.CurrentContext.Test.Name);
            var testName = ApplicationRegistry.Get<IEnvironmentVariableRegistryWithPrefix>().TestName;

            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }



        [ApplicationRegistry(BuiltInAccessors.EnvironmenetVariable)]
        public interface IEnvironmentVariableRegistry
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


        [ApplicationRegistry(BuiltInAccessors.EnvironmenetVariable)]
        [EnvironmentVariablePrefix("ApplicationRegistoryTest")]
        public interface IEnvironmentVariableRegistryWithPrefix
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [EnvironmentVariableName("PortNo")]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }

        [ApplicationRegistry(BuiltInAccessors.EnvironmenetVariable)]
        [EnvironmentVariablePrefix("")]
        public interface IEnvironmentVariableRegistryWithNoPrefix
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [EnvironmentVariableName("PortNo")]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }
    }
}
