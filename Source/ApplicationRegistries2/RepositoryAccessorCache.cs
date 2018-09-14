using System;
using System.Collections.Generic;

namespace ApplicationRegistries2
{
    class RepositoryAccessorCache
    {
        readonly Dictionary<Type, RepositoryAccessorInfo> _dictionary = new Dictionary<Type, RepositoryAccessorInfo>();

        public void Add(Type targetInterface, RepositoryAccessorInfo info)
        {
            _dictionary[targetInterface] = info;
        }

        public bool Exists(Type targetInterfaceType)
        {
            return _dictionary.ContainsKey(targetInterfaceType);
        }

        public RepositoryAccessorInfo Get(Type targetInterfaceType)
        {
            return _dictionary[targetInterfaceType];
        }
    }
}