using System.Collections.Generic;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    class AccessorRepository
    {
        private readonly Dictionary<string, IAccessor> _accessors = new Dictionary<string, IAccessor>()
        {
            {BuiltInAccessors.MachineRegistry, new RegistoryAccessor(RegistoryAccessor.RegistryRoot.LocalMachine)},
            {BuiltInAccessors.UserRegistry, new RegistoryAccessor(RegistoryAccessor.RegistryRoot.CurrentUser)},
            {BuiltInAccessors.XmlFile, new XmlFileAccessor()},
            {BuiltInAccessors.CommandlineArguments, new CommandlineArgumentsAccessor()},
            {BuiltInAccessors.EnvironmenetVariable, new EnvironmentVariableAccessor()},
            {BuiltInAccessors.DefaultValue ,new DefaultValueAccessor()},
            {BuiltInAccessors.PropertiesSettings, new PropertiesSettingsAccessor()},
        };


        public IAccessor GetAccessor(string key)
        {
            return _accessors[key];
        }

        public bool ExistsKey(string key)
        {
            return _accessors.ContainsKey(key);
        }

        public void RegistCustomAccessor(string key, IAccessor accessor)
        {
            _accessors[key] = accessor;
        }

    }
}