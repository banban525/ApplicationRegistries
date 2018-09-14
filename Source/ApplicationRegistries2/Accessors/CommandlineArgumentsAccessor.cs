using System;
using ApplicationRegistries2.Attributes;

namespace ApplicationRegistries2.Accessors
{
    /// <inheritdoc />
    class CommandlineArgumentsAccessor : IAccessor
    {
        public object Read(Type returnType, AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition)
        {
            var commandlineName = GetCommandlineName(accessorDefinition, accessorFieldDefinition);

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

        public bool Exists(Type fieldType, AccessorDefinition accessorDefinition, AccessorFieldDefinition field)
        {
            var commandlineName = GetCommandlineName(accessorDefinition, field);

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

        private static string GetCommandlineName(AccessorDefinition accessorDefinition,
            AccessorFieldDefinition accessorFieldDefinition)
        {
            var interfaceName = accessorDefinition.TargetInterfaceType.Name;

            var commandlineArgumentPrefixAttribute =
                accessorDefinition.GetAttribute<CommandlineArgumentPrefixAttribute>();
            var prefix = commandlineArgumentPrefixAttribute?.Prefix ?? $@"{interfaceName}";

            var commandlineArgumentNameAttribute =
                accessorFieldDefinition.GetAttribute<CommandlineArgumentNameAttribute>();
            var name = commandlineArgumentNameAttribute?.Name ?? accessorFieldDefinition.Name;

            return prefix == "" ? $"--{name}" : $"--{prefix}_{name}";
        }

        private string[] GetCommandlineArguments()
        {
            return _overridedCommandlineArguments ?? Environment.GetCommandLineArgs();
        }

        private static string[] _overridedCommandlineArguments = null;

        internal static void OverrideCommandlineArgumentsForUnitTests(string[] arguments)
        {
            _overridedCommandlineArguments = arguments;
        }
    }
}