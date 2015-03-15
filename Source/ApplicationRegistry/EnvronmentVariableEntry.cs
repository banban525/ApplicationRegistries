using System;

namespace ApplicationRegistries
{
    class EnvronmentVariableEntry : IEntry
    {
        private readonly EntryDefine _define;
        private readonly string _variableName;
        private readonly string _defaultValue;

        public EnvronmentVariableEntry(EntryDefine define, string variableName, string defaultValue)
        {
            _define = define;
            _variableName = variableName;
            _defaultValue = defaultValue;
        }

        public EntryDefine Define
        {
            get { return _define; }
        }

        public string Behavior { get { return GetType().Name; } }

        public string VariableName
        {
            get { return _variableName; }
        }

        public string DefaultValue
        {
            get { return _defaultValue; }
        }

        public string GetValue()
        {
            var value = Environment.GetEnvironmentVariable(VariableName);
            if (value == null)
            {
                return DefaultValue;
            }
            return value;
        }

        public ValidateResults Validate()
        {
            if (_define.Type == TypeEnum.StringArray)
            {
                return ValidateResults.Empty + new ValidateDetail(ValidateErrorLevel.Error, "The EnvronmentValiable is not support a string[] type.");
            }
            return ValidateResults.Empty;
        }

        public bool ExistsValue()
        {
            var value = Environment.GetEnvironmentVariable(VariableName);
            return value != null;
        }

        public IEntry Repace(string from, string to)
        {
            return new EnvronmentVariableEntry(
                _define.Replace(from, to), 
                _variableName.Replace(from, to),
                _defaultValue.Replace(from, to));
        }
    }
}