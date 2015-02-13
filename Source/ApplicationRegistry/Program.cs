using System.Runtime.InteropServices;

namespace ApplicationRegistries.GeneratedXmlObject
{
    partial class ApplicationRegistryDefineEntry
    {
        internal RegistryEntry CreateRegistryEntry(EntryDefine define)
        {
            var registryXml = Item as Registry;
            if (registryXml == null)
            {
                throw new InvalidOleVariantTypeException();
            }

            var key = registryXml.Key;
            var name = registryXml.Name;
            var defaultValue = registryXml.DefaultValue;
            return new RegistryEntry(define, key, name, defaultValue);
        }

        internal StaticValueEntry CreateStaticValueEntry(EntryDefine define)
        {
            var staticValueXml = Item as StaticValue;
            if (staticValueXml == null)
            {
                throw new InvalidOleVariantTypeException();
            }

            var value = staticValueXml.Value;
            return new StaticValueEntry(define, value);
        }

        internal CommandLineArgumentEntry CreateCommandLineArgumentEntry(EntryDefine define)
        {
            var commandLineXml = Item as CommandLineArgument;
            if (commandLineXml == null)
            {
                throw new InvalidOleVariantTypeException();
            }
            var argumentName = commandLineXml.ArgumentName;
            var ignoreCase = commandLineXml.ignoreCase;
            var defaultValue = commandLineXml.DefaultValue;
            return new CommandLineArgumentEntry(define, argumentName,
                ignoreCase, defaultValue);
        }

        internal EnvronmentVariableEntry CreateEnvironmentVariableEntry(EntryDefine define)
        {
            var environmentXml = Item as EnvironmentVariable;
            if (environmentXml == null)
            {
                throw new InvalidOleVariantTypeException();
            }
            var variableName = environmentXml.VariableName;
            var defaultValue = environmentXml.DefaultValue;
            return new EnvronmentVariableEntry(define, variableName, defaultValue);
        }

        internal bool HasRegistryEntry
        {
            get { return Item is Registry; }
        }
        internal bool HasStaticValue
        {
            get { return Item is StaticValue; }
        }
        internal bool HasCommandLineArgument
        {
            get { return Item is CommandLineArgument; }
        }
        internal bool HasEnvironmentVariable
        {
            get { return Item is EnvironmentVariable; }
        }
    }
}

