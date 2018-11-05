using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class DefaultValueTest
    {
        [Test]
        public void Test()
        {
            var listenPortNo = ApplicationRegistry.Get<IDefaultValueRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(80));

            var testName = ApplicationRegistry.Get<IDefaultValueRegistry>().TestName;
            Assert.That(testName, Is.EqualTo("temp"));
        }


        [ApplicationRegistry(
            BuiltInAccessors.CommandlineArguments,
            BuiltInAccessors.DefaultValue)]
        public interface IDefaultValueRegistry
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            [DefaultValue(80)]
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            [DefaultValue("temp")]
            string TestName { get; }
        }
    }
}
