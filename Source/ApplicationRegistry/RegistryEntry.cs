using Microsoft.Win32;

namespace ApplicationRegistries
{
    class RegistryEntry : IEntry
    {
        private readonly EntryDefine _define;
        private readonly string _key;
        private readonly string _name;
        private readonly string _defaultValue;

        public RegistryEntry(EntryDefine define, string key, string name, string defaultValue)
        {
            _define = define;
            _key = key;
            _name = name;
            _defaultValue = defaultValue;
        }

        public EntryDefine Define
        {
            get { return _define; }
        }

        public string Key
        {
            get { return _key; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string DefaultValue
        {
            get { return _defaultValue; }
        }

        public string GetValue()
        {
            var obj = Registry.GetValue(Key, Name, null);
            if (obj == null)
            {
                return DefaultValue;
            }
            return obj.ToString();
        }

        public bool ExistsValue()
        {
            var obj = Registry.GetValue(Key, Name, null);
            return obj != null;
        }

        public IEntry Repace(string from, string to)
        {
            return new RegistryEntry(
                _define.Replace(from, to), 
                _key.Replace(from, to),
                _name.Replace(from, to),
                _defaultValue.Replace(from, to));
        }
    }
}