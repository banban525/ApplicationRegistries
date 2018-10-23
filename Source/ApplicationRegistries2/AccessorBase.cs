namespace ApplicationRegistries2
{
    public class AccessorBase
    {
        private readonly AccessorTypeDeclaration _accessorTypeDeclaration;
        internal AccessorBase(AccessorTypeDeclaration accessorDeclaration)
        {
            _accessorTypeDeclaration = accessorDeclaration;
        }

        public object Get(string name)
        {
            var field = _accessorTypeDeclaration.GetField(name);


            foreach (var accessor in _accessorTypeDeclaration.AccessToList)
            {
                if (accessor.Exists(field.Type, _accessorTypeDeclaration, field))
                {
                    return accessor.Read(field.Type, _accessorTypeDeclaration, field);
                }
            }

            throw new DataNotFoundException();
        }
    }
}