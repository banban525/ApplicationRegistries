using System;
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
                var define = builder.Parse(typeof(T), AccessorRepository);
                var typeInfo = builder.Build(define);

                var builtResult = Activator.CreateInstance(typeInfo.AsType(), new AccessorBase(define));

                _cache.Add(typeof(T), new RepositoryAccessorInfo(typeof(T), define, builtResult, DateTime.Now));
                return (T)_cache.Get(typeof(T)).BuildResult;
            }

            return (T)_cache.Get(typeof(T)).BuildResult;
        }

        public void RegistCustomAccessor(string key, IAccessor accessor)
        {
            AccessorRepository.RegistCustomAccessor(key, accessor);
        }

        internal AccessorRepository AccessorRepository{ get; } = new AccessorRepository();
        private readonly RepositoryAccessorCache _cache = new RepositoryAccessorCache();
    }
}