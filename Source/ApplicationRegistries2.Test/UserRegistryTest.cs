using ApplicationRegistries2.Attributes;
using Microsoft.Win32;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class UserRegistryTest
    {
        [TearDown]
        public void TearDown()
        {
            Registry.CurrentUser.DeleteSubKeyTree(@"Software\ApplicationRegistries\ApplicationRegistries2.Test", false);
            Registry.CurrentUser.DeleteSubKeyTree(@"Software\ApplicationRegistries\Temp", false);
        }

        [Test]
        public void Test()
        {
            
            using (var key =
                Registry.CurrentUser.CreateSubKey(@"Software\ApplicationRegistries\ApplicationRegistries2.Test\IUserRegistryRegistry"))
            {
                // ReSharper disable once PossibleNullReferenceException
                key.SetValue("ListenPortNo", 80, RegistryValueKind.DWord);
                key.SetValue("TestName", TestContext.CurrentContext.Test.Name, RegistryValueKind.String);
            }

            var listenPortNo = ApplicationRegistry.Get<IUserRegistryRegistry>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(80));

            var testName = ApplicationRegistry.Get<IUserRegistryRegistry>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }


        [ApplicationRegistry(BuiltInAccessors.UserRegistry)]
        public interface IUserRegistryRegistry
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
        public void プレフィックス付きの場合はそのキーと名前から取得する()
        {

            using (var key =
                Registry.CurrentUser.CreateSubKey(@"Software\ApplicationRegistries\Temp"))
            {
                // ReSharper disable once PossibleNullReferenceException
                key.SetValue("ListenPortNo", 81, RegistryValueKind.DWord);
                key.SetValue("Temp", TestContext.CurrentContext.Test.Name, RegistryValueKind.String);
            }

            var listenPortNo = ApplicationRegistry.Get<IUserRegistryRegistryWithPrefix>().ListenPortNo;
            Assert.That(listenPortNo, Is.EqualTo(81));

            var testName = ApplicationRegistry.Get<IUserRegistryRegistryWithPrefix>().TestName;
            Assert.That(testName, Is.EqualTo(TestContext.CurrentContext.Test.Name));
        }

        [ApplicationRegistry(BuiltInAccessors.UserRegistry)]
        [RegistryKey(@"SOFTWARE\ApplicationRegistries\Temp")]
        public interface IUserRegistryRegistryWithPrefix
        {
            /// <summary>
            /// 待ち受けポートNo
            /// </summary>
            int ListenPortNo { get; }

            /// <summary>
            /// 実行中のテスト名
            /// </summary>
            [RegistryName("Temp")]
            string TestName { get; }
        }
    }
}
