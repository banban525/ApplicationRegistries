using System;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using NUnit.Framework;

namespace ApplicationRegistries.Test
{
    [TestFixture]
    public class ApplicationRegistryAccesserTest
    {
        private string _defineXml;
        private ApplicationRegistryAccesser _accesser;
        [SetUp]
        public void SetUp()
        {
            _defineXml = @"<?xml version='1.0' encoding='utf-8' ?>
<ApplicationRegistryDefine
  xmlns='https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegistryDefine.xsd'>
  <Entry id='InstallDir' Type='string'>
    <Description>Installed Directory</Description>
    <Registory>
      <Key>HKEY_CURRENT_USER\SOFTWARE\banban525\ApplicationRegistries\Install</Key>
      <Name>Directory</Name>
      <DefaultValue>None</DefaultValue>
    </Registory>
  </Entry>
  <Entry id='ApplicationName' Type='string'>
    <Description>Application Name</Description>
    <StaticValue>
      <Value>ApplicationRegistries</Value>
    </StaticValue>
  </Entry>
  <Entry id='IsDebug' Type='bool'>
    <Description>Debug Mode</Description>
    <CommandLineArgument ignoreCase='true'>
      <ArgumentName>/Debug</ArgumentName>
      <DefaultValue>0</DefaultValue>
    </CommandLineArgument>
  </Entry>
  <Entry id='Proxy' Type='string'>
    <Description>Proxy for http access.</Description>
    <EnvironmentVariable>
      <VariableName>PROXY</VariableName>
      <DefaultValue>localhost</DefaultValue>
    </EnvironmentVariable>
  </Entry>
</ApplicationRegistryDefine>
";
            _accesser = new ApplicationRegistryAccesser(XmlReader.Create(new StringReader(_defineXml)));

        }
        [TestCase]
        [SetRegistry]
        public void AccessToRegistry()
        {
            var val = _accesser.GetString("InstallDir");
            Assert.That(val, Is.EqualTo(AppDomain.CurrentDomain.BaseDirectory));
        }

        [TestCase]
        public void AccessToRegistryNotCreated()
        {
            var val = _accesser.GetString("InstallDir");
            Assert.That(val, Is.EqualTo("None"));
        }

        [TestCase]
        public void AccessToStaticValue()
        {
            var val = _accesser.GetString("ApplicationName");
            Assert.That(val, Is.EqualTo("ApplicationRegistries"));
        }

        [TestCase]
        [SetCommandLineArguments]
        public void AccessToCommandlineArguments()
        {
            var val = _accesser.GetBoolean("IsDebug");
            Assert.That(val, Is.True);
        }

        [TestCase]
        public void AccessToCommandlineArgumentsNonExists()
        {
            var val = _accesser.GetBoolean("IsDebug");
            Assert.That(val, Is.False);
        }
        [TestCase]
        [SetEnvironmentVarialbe]
        public void AccessToEnvironmentVariable()
        {
            var val = _accesser.GetString("Proxy");
            Assert.That(val, Is.EqualTo("127.0.0.1"));
        }
        [TestCase]
        public void AccessToEnvironmentVariableNotExists()
        {
            var val = _accesser.GetString("Proxy");
            Assert.That(val, Is.EqualTo("localhost"));
        }

        [TestCase]
        [SetEnvironmentVarialbe]
        public void OverrideEntry()
        {
            _accesser.AddOverrideBehavior("Proxy", () => "192.168.0.1");
            var val = _accesser.GetString("Proxy");
            Assert.That(val, Is.EqualTo("192.168.0.1"));
        }

        [AttributeUsage(AttributeTargets.Method)]
        class SetCommandLineArgumentsAttribute : Attribute, ITestAction
        {
            public void BeforeTest(TestDetails testDetails)
            {
                var testFixture = testDetails.Fixture as ApplicationRegistryAccesserTest;
                testFixture._accesser = new ApplicationRegistryAccesser(
                    XmlReader.Create(new StringReader(testFixture._defineXml)),
                    new[] { "/Debug", "1"}
                    );
            }

            public void AfterTest(TestDetails testDetails)
            {
                
            }

            public ActionTargets Targets
            {
                get { return ActionTargets.Test;}
            }
        }

        [AttributeUsage(AttributeTargets.Method)]
        public class SetRegistryAttribute : Attribute, ITestAction
        {
            public void BeforeTest(TestDetails testDetails)
            {
                var registryKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\banban525\ApplicationRegistries\Install");
                registryKey.SetValue("Directory", AppDomain.CurrentDomain.BaseDirectory);
            }

            public void AfterTest(TestDetails testDetails)
            {
                var registryKeySoftware = Registry.CurrentUser.CreateSubKey(@"SOFTWARE");
                registryKeySoftware.DeleteSubKeyTree("banban525");
            }

            public ActionTargets Targets
            {
                get { return ActionTargets.Test; }
            }
        }



        [AttributeUsage(AttributeTargets.Method)]
        public class SetEnvironmentVarialbeAttribute : Attribute, ITestAction
        {
            public void BeforeTest(TestDetails testDetails)
            {
                Environment.SetEnvironmentVariable("PROXY", "127.0.0.1");
            }

            public void AfterTest(TestDetails testDetails)
            {
                Environment.SetEnvironmentVariable("PROXY", "");
            }

            public ActionTargets Targets
            {
                get { return ActionTargets.Test; }
            }
        }
    }
}