namespace ApplicationRegistries
{
    class StaticValueEntry : IEntry
    {
        private readonly EntryDefine _define;
        private readonly string _value;

        public StaticValueEntry(EntryDefine define, string value)
        {
            _define = define;
            _value = value;
        }

        public EntryDefine Define
        {
            get { return _define; }
        }

        public string Value
        {
            get { return _value; }
        }

        public string GetValue()
        {
            return Value;
        }

        public bool ExistsValue()
        {
            return true;
        }

        public IEntry Repace(string from, string to)
        {
            return new StaticValueEntry(_define.Replace(from, to), _value.Replace(from, to));
        }
    }
}