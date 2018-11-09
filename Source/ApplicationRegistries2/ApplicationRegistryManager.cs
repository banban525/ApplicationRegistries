using System;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    /// <summary>
    /// The instance to access external settings
    /// </summary>
    public class ApplicationRegistryManager
    {
        /// <summary>
        /// Get external setting proxy
        /// </summary>
        /// <typeparam name="T">external settings interface type</typeparam>
        /// <returns>external setting proxy</returns>
        public T Get<T>() where T:class
        {
            if (_cache.Exists(typeof(T))==false)
            {
                var builder = new AccessorTypeBuilder();
                var define = builder.Parse(typeof(T));
                var typeInfo = builder.Build(define);

                var builtResult = Activator.CreateInstance(typeInfo.AsType(), new AccessorBase(define, AccessorRepository));

                _cache.Add(typeof(T), new RepositoryAccessorInfo(typeof(T), define, builtResult, DateTime.Now));
                return (T)_cache.Get(typeof(T)).BuildResult;
            }

            return (T)_cache.Get(typeof(T)).BuildResult;
        }

        /// <summary>
        /// regist custom accessor
        /// </summary>
        /// <param name="key">accessor key</param>
        /// <param name="accessor">custom accessor</param>
        public void RegistCustomAccessor(string key, IAccessor accessor)
        {
            AccessorRepository.RegistCustomAccessor(key, accessor);
        }


        internal AccessorRepository AccessorRepository{ get; } = new AccessorRepository();
        private readonly RepositoryAccessorCache _cache = new RepositoryAccessorCache();
    }
}