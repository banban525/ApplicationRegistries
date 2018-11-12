using System;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class CommandlineArgumentsAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var commandlineName = GetCommandlineName(accessorTypeDeclaration, accessorFieldDeclaration);

            var args = GetCommandlineArguments();
            string val = null;
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == commandlineName && i + 1 < args.Length)
                {
                    val = args[i + 1];
                    break;
                }

                if (args[i].StartsWith($"{commandlineName}="))
                {
                    val = args[i].Substring($"{commandlineName}=".Length);
                    break;
                }
            }

            if (val == null)
            {
                return null;
            }

            return Convert.ChangeType(val, returnType);
        }

        public bool Exists(Type fieldType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration field)
        {
            var commandlineName = GetCommandlineName(accessorTypeDeclaration, field);

            var args = GetCommandlineArguments();
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == commandlineName && i + 1 < args.Length)
                {
                    return true;
                }

                if (args[i].StartsWith($"{commandlineName}="))
                {
                    return true;
                }
            }

            return false;
        }

        internal static CommandlineArgumentsAccessorReportData GetPropertyData(AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration field)
        {
            var name = GetCommandlineName(accessorTypeDeclaration, field);

            return new CommandlineArgumentsAccessorReportData(name);
        }


        public class CommandlineArgumentsAccessorReportData
        {
            public CommandlineArgumentsAccessorReportData(string commandlineArgumentName)
            {
                CommandlineArgumentName = commandlineArgumentName;
            }

            public string CommandlineArgumentName { get; }
        }

        private static string GetCommandlineName(AccessorTypeDeclaration accessorTypeDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration)
        {
            var interfaceName = accessorTypeDeclaration.TargetInterfaceType.Name;

            var commandlineArgumentPrefixAttribute =
                accessorTypeDeclaration.GetAttribute<CommandlineArgumentPrefixAttribute>();
            var prefix = commandlineArgumentPrefixAttribute?.Prefix ?? interfaceName;

            var commandlineArgumentNameAttribute =
                accessorFieldDeclaration.GetAttribute<CommandlineArgumentNameAttribute>();
            var name = string.IsNullOrEmpty(commandlineArgumentNameAttribute?.Name)
                ? accessorFieldDeclaration.Name
                : commandlineArgumentNameAttribute.Name;

            return prefix == "" ? $"--{name}" : $"--{prefix}_{name}";
        }

        private string[] GetCommandlineArguments()
        {
            return _overridedCommandlineArguments ?? Environment.GetCommandLineArgs();
        }

        private static string[] _overridedCommandlineArguments;

        internal static void OverrideCommandlineArgumentsForUnitTests(string[] arguments)
        {
            _overridedCommandlineArguments = arguments;
        }
    }
}