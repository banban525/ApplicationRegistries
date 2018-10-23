using System;

namespace ApplicationRegistries2
{
    class RepositoryAccessorInfo
    {
        public RepositoryAccessorInfo(Type targetInterfaceType, AccessorTypeDeclaration accessorTypeDeclaration, object buildResult, DateTime builtTime)
        {
            TargetInterfaceType = targetInterfaceType;
            TypeDeclaration = accessorTypeDeclaration;
            BuildResult = buildResult;
            BuiltTime = builtTime;
        }

        public Type TargetInterfaceType { get; }
        public DateTime BuiltTime { get; }

        public object BuildResult { get; }

        public  AccessorTypeDeclaration TypeDeclaration { get; }
    }
}