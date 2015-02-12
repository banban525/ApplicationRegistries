using System;

namespace ApplicationRegistries
{
    class CommandLineArgumentEntry : IEntry
    {
        private readonly EntryDefine _define;
        private readonly string _argumentName;
        private readonly string _defaultValue;
        private readonly bool _ignoreCase;
        private string[] _commandLineArguments = Environment.GetCommandLineArgs();

        public CommandLineArgumentEntry(EntryDefine define, string argumentName, bool ignoreCase, string defaultValue)
        {
            _define = define;
            _argumentName = argumentName;
            _defaultValue = defaultValue;
            _ignoreCase = ignoreCase;
        }

        public EntryDefine Define
        {
            get { return _define; }
        }

        public string ArgumentName
        {
            get { return _argumentName; }
        }

        public bool IgnoreCase
        {
            get { return _ignoreCase; }
        }

        public string DefaultValue
        {
            get { return _defaultValue; }
        }

        public string GetValue()
        {
            var args = _commandLineArguments;
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                var searchArgumentName = ArgumentName;
                if (IgnoreCase)
                {
                    arg = arg.ToLower();
                    searchArgumentName = searchArgumentName.ToLower();
                }
                if (arg == searchArgumentName)
                {
                    if (i + 1 < args.Length)
                    {
                        return args[i + 1];
                    }
                }
            }
            return DefaultValue;
        }

        public bool ExistsValue()
        {
            var args = _commandLineArguments;
            foreach (string argOriginal in args)
            {
                var arg = argOriginal;
                var searchArgumentName = ArgumentName;
                if (IgnoreCase)
                {
                    arg = arg.ToLower();
                    searchArgumentName = searchArgumentName.ToLower();
                }
                if (arg == searchArgumentName)
                {
                    return true;
                }
            }
            return false;
        }


        public void SetCommandLineArguments(string[] commandLineArguments)
        {
            if (commandLineArguments != null)
            {
                _commandLineArguments = commandLineArguments;
            }
            else
            {
                _commandLineArguments = Environment.GetCommandLineArgs();
            }
        }
    }
}