using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using ApplicationRegistries.GeneratedXmlObject;

namespace ApplicationRegistries
{
    class Entries
    {
        private readonly List<IEntry> _entries;

        public Entries(IEnumerable<IEntry> entries)
        {
            _entries = new List<IEntry>(entries);
        }
        public static Entries Parse(string defineFilePath)
        {
            if (defineFilePath == null)
            {
                throw new ArgumentNullException("defineFilePath");
            }
            using (var stream = new FileStream(defineFilePath, FileMode.Open, FileAccess.Read))
            {
                return Parse(stream);
            }
        }

        public static Entries Parse(XmlReader define)
        {
            var serializer = new XmlSerializer(typeof(GeneratedXmlObject.ApplicationRegistryDefine));
            var xmlObject = (GeneratedXmlObject.ApplicationRegistryDefine)serializer.Deserialize(define);

            return Parse(xmlObject);
        }

        public static Entries Parse(Stream defineStream)
        {
            var serializer = new XmlSerializer(typeof(GeneratedXmlObject.ApplicationRegistryDefine));
            var xmlObject = (GeneratedXmlObject.ApplicationRegistryDefine)serializer.Deserialize(defineStream);
            return Parse(xmlObject);
        }

        private static Entries Parse(ApplicationRegistryDefine xmlObject)
        {
            var entries = new List<IEntry>();
            foreach (var entryXml in xmlObject.Entry)
            {
                var id = entryXml.id;
                var type = entryXml.Type.ToDomainType();
                var description = entryXml.Description;

                var item = new EntryDefine(id, type, description);

                if (entryXml.HasRegistryEntry)
                {
                    var entry = entryXml.CreateRegistryEntry(item);
                    entries.Add(entry);
                }
                if (entryXml.HasStaticValue)
                {
                    var entry = entryXml.CreateStaticValueEntry(item);
                    entries.Add(entry);
                }
                if (entryXml.HasCommandLineArgument)
                {
                    var entry = entryXml.CreateCommandLineArgumentEntry(item);
                    entries.Add(entry);
                }
                if (entryXml.HasEnvironmentVariable)
                {
                    var entry = entryXml.CreateEnvironmentVariableEntry(item);
                    entries.Add(entry);
                }
            }

            return new Entries(entries);
        }

        public static Entries Parse(Entries baseEntries, string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return Parse(baseEntries, stream);
            }
        }

        public static Entries Parse(Entries baseEntries, Stream setting)
        {
            var serializer = new XmlSerializer(typeof(GeneratedXmlObject.ApplicationRegistryBehavior));
            var xmlObject = (GeneratedXmlObject.ApplicationRegistryBehavior)serializer.Deserialize(setting);
            

            return Parse(baseEntries, xmlObject);
        }



        public static Entries Parse(Entries baseEntries, XmlReader reader)
        {
            var serializer = new XmlSerializer(typeof(GeneratedXmlObject.ApplicationRegistryBehavior));
            var xmlObject = (GeneratedXmlObject.ApplicationRegistryBehavior)serializer.Deserialize(reader);

            return Parse(baseEntries, xmlObject);
        }
        private static Entries Parse(Entries baseEntries, ApplicationRegistryBehavior xmlObject)
        {
            var entries = new List<IEntry>();
            foreach (var entry in xmlObject.Entry.Where(_ => _.Item is GeneratedXmlObject.Registry))
            {
                var baseEntry = baseEntries.GetEntry(entry.id);
                if (baseEntry == null)
                {
                    continue;
                }
                var registryXml = entry.Item as GeneratedXmlObject.Registry;
                if (registryXml == null)
                {
                    throw new InvalidOleVariantTypeException();
                }

                var key = registryXml.Key;
                var name = registryXml.Name;
                var defaultValue = registryXml.DefaultValue;
                entries.Add(new RegistryEntry(baseEntry.Define, key, name, defaultValue));
            }

            foreach (var entry in xmlObject.Entry.Where(_ => _.Item is GeneratedXmlObject.StaticValue))
            {
                var baseEntry = baseEntries.GetEntry(entry.id);
                if (baseEntry == null)
                {
                    continue;
                }
                var staticValueXml = entry.Item as GeneratedXmlObject.StaticValue;
                if (staticValueXml == null)
                {
                    throw new InvalidOleVariantTypeException();
                }

                var value = staticValueXml.Value;
                entries.Add(new StaticValueEntry(baseEntry.Define, value));
            }

            foreach (var entry in xmlObject.Entry.Where(_ => _.Item is GeneratedXmlObject.CommandLineArgument))
            {
                var baseEntry = baseEntries.GetEntry(entry.id);
                if (baseEntry == null)
                {
                    continue;
                }
                var commandLineXml = entry.Item as GeneratedXmlObject.CommandLineArgument;
                if (commandLineXml == null)
                {
                    throw new InvalidOleVariantTypeException();
                }
                var argumentName = commandLineXml.ArgumentName;
                var ignoreCase = commandLineXml.ignoreCase;
                var defaultValue = commandLineXml.DefaultValue;
                entries.Add(new CommandLineArgumentEntry(baseEntry.Define, argumentName,
                    ignoreCase, defaultValue));
            }

            foreach (var entry in xmlObject.Entry.Where(_ => _.Item is GeneratedXmlObject.EnvironmentVariable))
            {
                var baseEntry = baseEntries.GetEntry(entry.id);
                if (baseEntry == null)
                {
                    continue;
                }
                var environmentXml = entry.Item as GeneratedXmlObject.EnvironmentVariable;
                if (environmentXml == null)
                {
                    throw new InvalidOleVariantTypeException();
                }
                var variableName = environmentXml.VariableName;
                var defaultValue = environmentXml.DefaultValue;
                entries.Add(new EnvronmentVariableEntry(baseEntry.Define, variableName, defaultValue));
            }

            return new Entries(entries);
        }

        private IEntry GetEntry(string id)
        {
            return _entries.FirstOrDefault(_ => _.Define.ID == id);
        }

        public string GetValue(string id)
        {
            var entry = _entries.FirstOrDefault(_ => _.Define.ID == id);
            if (entry == null)
            {
                throw new KeyNotFoundException();
            }
            return entry.GetValue();
        }

        public IEnumerable<RegistryEntry> RegistryEntries
        {
            get { return _entries.Where(_ => _ is RegistryEntry).Cast<RegistryEntry>(); }
        }

        public IEnumerable<StaticValueEntry> StaticValueEntries
        {
            get { return _entries.Where(_ => _ is StaticValueEntry).Cast<StaticValueEntry>(); }
        }


        public IEnumerable<CommandLineArgumentEntry> CommandLineArgumentEntries
        {
            get { return _entries.Where(_ => _ is CommandLineArgumentEntry).Cast<CommandLineArgumentEntry>(); }
        }


        public IEnumerable<EnvronmentVariableEntry> EnvironmentVariableEntries
        {
            get { return _entries.Where(_ => _ is EnvronmentVariableEntry).Cast<EnvronmentVariableEntry>(); }
        }

        public IEnumerable<IEntry> AllEntries
        {
            get { return _entries; }
        }

        public IEnumerable<EntryDefine> Defines
        {
            get { return _entries.Select(_ => _.Define); }
        }

        internal void SetCommandLineArguments(string[] commandLineArguments)
        {
            foreach (var commandLineArgumentEntry in CommandLineArgumentEntries)
            {
                commandLineArgumentEntry.SetCommandLineArguments(commandLineArguments);
            }
        }

        public Entries Replace(IEntry entry)
        {
            var newEntries = _entries.Where(_ => _.Define.ID != entry.Define.ID).Concat(new IEntry[] {entry});
            return new Entries(newEntries);
        }
    }
}