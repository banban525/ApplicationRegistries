using System;
using System.IO;
using System.Xml;

namespace ApplicationRegistries
{
    public class Registries
    {
        readonly ApplicationRegistryAccesser _accesser;
        const string DefineXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ApplicationRegistryDefine
  xmlns=""https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegistryDefine.xsd"">
  <Entry id=""InstallDir"" Type=""string"">
    <Description>Installed Directory</Description>
    <Registry>
      <Key>HKEY_CURRENT_USER\SOFTWARE\banban525\ApplicationRegistries\Install</Key>
      <Name>Directory</Name>
      <DefaultValue>None</DefaultValue>
    </Registry>
  </Entry>
  <Entry id=""ApplicationName"" Type=""string"">
    <Description>Application Name</Description>
    <StaticValue>
      <Value>ApplicationRegistries</Value>
    </StaticValue>
  </Entry>
  <Entry id=""IsDebug"" Type=""bool"">
    <Description>Debug Mode</Description>
    <CommandLineArgument ignoreCase=""true"">
      <ArgumentName>/Debug</ArgumentName>
      <DefaultValue>0</DefaultValue>
    </CommandLineArgument>
  </Entry>
  <Entry id=""Proxy"" Type=""string"">
    <Description>Proxy for http access.</Description>
    <EnvironmentVariable>
      <VariableName>PROXY</VariableName>
      <DefaultValue>localhost</DefaultValue>
    </EnvironmentVariable>
  </Entry>
</ApplicationRegistryDefine>";

        public Registries(string[] commandlineArguments = null)
        {
            _accesser = new ApplicationRegistryAccesser(XmlReader.Create(new StringReader(DefineXml)), commandlineArguments);
        }

        public void AddOverrideFile(string filePath)
        {
            _accesser.AddOverrideFile(filePath);
        }

        public void AddOverrideFile(Stream stream)
        {
            _accesser.AddOverrideFile(stream);
        }

        public void AddOverrideFile(XmlReader reader)
        {
            _accesser.AddOverrideFile(reader);
        }

        public void AddOverrideBehavior(string key, Func<string> getValueFunc)
        {
            _accesser.AddOverrideBehavior(key, getValueFunc);
        }

        /// <summary>
        /// Installed Directory
        /// </summary>
        public String InstallDir
        {
            get
            {
                return _accesser.GetString("InstallDir");
            }
        }
        /// <summary>
        /// Application Name
        /// </summary>
        public String ApplicationName
        {
            get
            {
                return _accesser.GetString("ApplicationName");
            }
        }
        /// <summary>
        /// Debug Mode
        /// </summary>
        public Boolean IsDebug
        {
            get
            {
                return _accesser.GetBoolean("IsDebug");
            }
        }
        /// <summary>
        /// Proxy for http access.
        /// </summary>
        public String Proxy
        {
            get
            {
                return _accesser.GetString("Proxy");
            }
        }

    }
}