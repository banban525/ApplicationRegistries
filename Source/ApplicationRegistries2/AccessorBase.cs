using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    /// <summary>
    /// Internal class for access to external value
    /// </summary>
    public class AccessorBase
    {
        private readonly AccessorTypeDeclaration _accessorTypeDeclaration;
        private readonly AccessorRepository _repository;
        internal AccessorBase(AccessorTypeDeclaration accessorDeclaration, AccessorRepository repository)
        {
            _accessorTypeDeclaration = accessorDeclaration;
            _repository = repository;
        }

        private readonly IAccessor _internalDefaultValueAccessor = new DefaultValueAccessor();

        /// <summary>
        /// load value
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>loaded value</returns>
        public object Get(string name)
        {
            var field = _accessorTypeDeclaration.GetField(name);

            foreach (var key in _accessorTypeDeclaration.Keys)
            {
                var accessor = _repository.GetAccessor(key);
                if (accessor.Exists(field.Type, _accessorTypeDeclaration, field))
                {
                    return accessor.Read(field.Type, _accessorTypeDeclaration, field);
                }
            }

            if (_internalDefaultValueAccessor.Exists(field.Type, _accessorTypeDeclaration, field))
            {
                return _internalDefaultValueAccessor.Read(field.Type, _accessorTypeDeclaration, field);
            }

            throw new DataNotFoundException();
        }
    }
}