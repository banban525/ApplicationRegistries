using System;
using System.IO;
using System.Xml;

namespace $_output.Namespace$
{
    public class $_output.ClassName$
    {
        readonly ApplicationRegistries.ApplicationRegistryAccesser _accesser;
        const string DefineXml = @"$_output.InputXml$";

        public $_output.ClassName$(string[] commandlineArguments = null)
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

$_output.Entries.AllEntries:{entry|
        /// <summary>
        /// $entry.Define.Description$
        /// </summary>
        public $entry.Define.Type$ $entry.Define.ID$
        {
            get
            {
                return _accesser.Get$entry.Define.Type$("$entry.Define.ID$");
            \}
        \}
}$
    }
}