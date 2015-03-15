using System;

namespace ApplicationRegistries
{
    public enum CommandlineType
    {
        UseNextValue,
        HasArgument,
        ParsePattern,
    }

    public static class CommandlineTypeExtensions
    {
        public static CommandlineType ToDomainType(this GeneratedXmlObject.CommandLineType type)
        {
            switch (type)
            {
                case GeneratedXmlObject.CommandLineType.useNextValue:
                    return CommandlineType.UseNextValue;
                case GeneratedXmlObject.CommandLineType.hasArgument:
                    return CommandlineType.HasArgument;
                case GeneratedXmlObject.CommandLineType.parsePattern:
                    return CommandlineType.ParsePattern;

            }
            throw new ArgumentException();
        }
    }
}