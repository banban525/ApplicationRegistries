using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class CommandLineTest
    {
        [SetUp]
        public void SetUp()
        {
            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(null);
        }

        [TearDown]
        public void TearDown()
        {
            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(null);
        }

        [Test]
        public void Test()
        {
            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(new[]
            {
                "--ICommandlineRegistry_ListenPortNo=80",
                "--ICommandlineRegistry_TestName",
                TestContext.CurrentContext.Test.Name
            });

            var listenPortNo = ApplicationRegistry.Get<ICommandlineRegistry>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(80));

            var testName = ApplicationRegistry.Get<ICommandlineRegistry>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }



        [ApplicationRegistry(BuiltInAccessors.CommandlineArguments)]
        public interface ICommandlineRegistry
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
        public void プレフィックス付きの場合()
        {
            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(new[]
            {
                "--Test_PortNo",
                "81",
                $"--Test_TestName={TestContext.CurrentContext.Test.Name}",
            });

            var listenPortNo = ApplicationRegistry.Get<ICommandlineRegistryWithPrefix>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(81));

            var testName = ApplicationRegistry.Get<ICommandlineRegistryWithPrefix>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }


        [ApplicationRegistry(BuiltInAccessors.CommandlineArguments)]
        [CommandlineArgumentPrefix("Test")]
        public interface ICommandlineRegistryWithPrefix
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [CommandlineArgumentName("PortNo")]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }


        [Test]
        public void 空のプレフィックスの場合はプロパティの名前だけでアクセスする()
        {
            Accessors.CommandlineArgumentsAccessor.OverrideCommandlineArgumentsForUnitTests(new[]
            {
                "--PortNo",
                "82",
                $"--TestName={TestContext.CurrentContext.Test.Name}",
            });

            var listenPortNo = ApplicationRegistry.Get<ICommandlineRegistryWithNoPrefix>().ListenPortNo;

            Assert.That(listenPortNo, Is.EqualTo(82));

            var testName = ApplicationRegistry.Get<ICommandlineRegistryWithNoPrefix>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }


        [ApplicationRegistry(BuiltInAccessors.CommandlineArguments)]
        [CommandlineArgumentPrefix("")]
        public interface ICommandlineRegistryWithNoPrefix
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [CommandlineArgumentName("PortNo")]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            string TestName { get; }
        }
    }
}
