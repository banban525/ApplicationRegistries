using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ApplicationRegistries
{
    public class ApplicationRegistryAccesser
    {
        private Entries _entries;

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

        public void AddOverrideFile(string filePath)
        {
            _entries = Entries.Parse(_entries, filePath);
        }

        public void AddOverrideFile(Stream stream)
        {
            _entries = Entries.Parse(_entries, stream);
        }

        public void AddOverrideFile(XmlReader reader)
        {
            _entries = Entries.Parse(_entries, reader);
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

        public void AddOverrideBehavior(string key, Func<string> getValueFunc)
        {
            var define = _entries.Defines.First(_ => _.ID == key);
            if (define == null) { throw new KeyNotFoundException();}

            _entries = _entries.Replace(new RuntimeEntry(define, getValueFunc));
        }

        public string[] GetStringArray(string key)
        {
            var val = _entries.GetValue(key).ToString();
            return val.Split('\t');
        }
    }
}