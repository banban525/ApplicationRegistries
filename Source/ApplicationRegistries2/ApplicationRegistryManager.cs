using System;
using System.Reflection;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    public class ApplicationRegistryManager
    {
        public T Get<T>() where T:class
        {
            if (_cache.Exists(typeof(T))==false)
            {
                var builder = new AccessorTypeBuilder();
                var define = builder.Parse(typeof(T), _accessorRepository);
                var typeInfo = builder.Build(define);

                var a = typeInfo.AsType().GetConstructor(new []{typeof(AccessorBase) });
                var builtResult = Activator.CreateInstance(typeInfo.AsType(), new AccessorBase(define));

                _cache.Add(typeof(T), new RepositoryAccessorInfo(typeof(T), define, builtResult, DateTime.Now));
                return (T)_cache.Get(typeof(T)).BuildResult;
            }

            return (T)_cache.Get(typeof(T)).BuildResult;
        }

        public void RegistCustomAccessor(string key, IAccessor accessor)
        {
            _accessorRepository.RegistCustomAccessor(key, accessor);
        }

        private readonly AccessorRepository _accessorRepository = new AccessorRepository();
        private readonly RepositoryAccessorCache _cache = new RepositoryAccessorCache();
    }
}