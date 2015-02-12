using System;
using System.IO;
using System.Xml;

namespace ApplicationRegistries
{
    public class ApplicationRegistryAccesser
    {
        private readonly Entries _entries;

        public ApplicationRegistryAccesser(string defineFilePath, string[] commandLineArguments = null)
        {
            _entries = Entries.Parse(defineFilePath);
            _entries.SetCommandLineArguments(commandLineArguments);
        }

        public ApplicationRegistryAccesser(XmlReader define, string[] commandLineArguments = null)
        {
            _entries = Entries.Parse(define);
            _entries.SetCommandLineArguments(commandLineArguments);
        }

        public ApplicationRegistryAccesser(Stream define, string[] commandLineArguments = null)
        {
            _entries = Entries.Parse(define);
            _entries.SetCommandLineArguments(commandLineArguments);
        }

        public string GetString(string key)
        {
            return _entries.GetValue(key);
        }

        public int GetInt32(string key)
        {
            return Convert.ToInt32(_entries.GetValue(key));
        }

        public bool GetBoolean(string key)
        {
            var val = _entries.GetValue(key);
            return val == "1" || val.ToLower() == "true" || val.ToLower() == "yes";
        }
    }
}