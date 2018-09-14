using System;

namespace ApplicationRegistries2
{
    class RepositoryAccessorInfo
    {
        public RepositoryAccessorInfo(Type targetInterfaceType, AccessorDefinition define, object buildResult, DateTime builtTime)
        {
            TargetInterfaceType = targetInterfaceType;
            Define = define;
            BuildResult = buildResult;
            BuiltTime = builtTime;
        }

        public Type TargetInterfaceType { get; }
        public DateTime BuiltTime { get; }

        public object BuildResult { get; }

        public  AccessorDefinition Define { get; }
    }
}