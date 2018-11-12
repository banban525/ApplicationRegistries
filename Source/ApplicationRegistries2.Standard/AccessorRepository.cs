using System;
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
        };

        public IEnumerable<string> AllKeys => _accessors.Keys;

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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (accessor == null)
            {
                throw new ArgumentNullException(nameof(accessor));
            }

            _accessors[key] = accessor;
        }

    }
}