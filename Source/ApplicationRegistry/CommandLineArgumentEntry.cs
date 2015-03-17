using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ApplicationRegistries
{
    class CommandLineArgumentEntry : IEntry
    {
        private readonly EntryDefine _define;
        private readonly string _argumentName;
        private readonly string _defaultValue;
        private readonly bool _ignoreCase;
        private string[] _commandLineArguments = Environment.GetCommandLineArgs();
        private readonly CommandlineType _type;
        private readonly bool _isMultiple;
        private readonly string _pattern;

        public CommandLineArgumentEntry(
            EntryDefine define, 
            string argumentName, 
            bool ignoreCase, 
            string defaultValue, CommandlineType type, bool isMultiple, string pattern)
        {
            _define = define;
            _argumentName = argumentName;
            _defaultValue = defaultValue;
            _type = type;
            _isMultiple = isMultiple;
            _pattern = pattern;
            _ignoreCase = ignoreCase;
        }

        public EntryDefine Define
        {
            get { return _define; }
        }

        public string Behavior { get { return GetType().Name; } }

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

        public CommandlineType Type
        {
            get { return _type; }
        }

        public bool IsMultiple
        {
            get { return _isMultiple; }
        }

        public string Pattern
        {
            get { return _pattern; }
        }

        public bool IsTypeHasArgument { get { return Type == CommandlineType.HasArgument; } }
        public bool IsTypeUseNextValue { get { return Type == CommandlineType.UseNextValue; } }
        public bool IsTypeParsePattern { get { return Type == CommandlineType.ParsePattern; } }

        public string GetValue()
        {
            var result = new List<string>();
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

                if (Type == CommandlineType.UseNextValue)
                {
                    if (arg == searchArgumentName)
                    {
                        if (i + 1 < args.Length)
                        {
                            result.Add(args[i + 1]);
                        }
                    }
                }
                else if(Type == CommandlineType.HasArgument)
                {
                    if (arg == searchArgumentName)
                    {
                        result.Add("True");
                        break;
                    }
                }
                else
                {
                    var regex = new Regex(Pattern, IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                    if (regex.IsMatch(arg))
                    {
                        result.Add(regex.Match(arg).Groups[1].Value);
                    }
                }
            }

            if (result.Count == 0)
            {
                return DefaultValue;
            }
            if (IsMultiple)
            {
                return string.Join("\t", result);
            }
            else
            {
                return result[0];
            }
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


                if (Type == CommandlineType.UseNextValue)
                {
                    if (arg == searchArgumentName)
                    {
                        return true;
                    }
                }
                else if (Type == CommandlineType.HasArgument)
                {
                    if (arg == searchArgumentName)
                    {
                        return true;
                    }
                }
                else
                {
                    var regex = new Regex(Pattern, IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                    return regex.IsMatch(arg);
                }
            }
            return false;
        }

        public IEntry Repace(string @from, string to)
        {
            return new CommandLineArgumentEntry(
                _define.Replace(from, to),
                _argumentName.Replace(from, to),
                _ignoreCase,
                _defaultValue.Replace(from, to),
                _type,
                _isMultiple,
                _pattern == null ? null : _pattern.Replace(from, to));
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

        public ValidateResults Validate()
        {
            if (Define.Type == TypeEnum.StringArray && _isMultiple == false)
            {
                return ValidateResults.Empty + new ValidateDetail(ValidateErrorLevel.Error, "A string[] type is required isMultiple in CommandlineArgument Entry.");
            }
            if (Define.Type != TypeEnum.StringArray && _isMultiple == true)
            {
                return ValidateResults.Empty + new ValidateDetail(ValidateErrorLevel.Error, "A isMultiple attribute is required string[] type.");
            }
            if (_isMultiple && Type == CommandlineType.HasArgument)
            {
                return ValidateResults.Empty + new ValidateDetail(ValidateErrorLevel.Error, "A hasArgument type is not return string[] type.");
            }
            return ValidateResults.Empty;
        }
    }
}