namespace ApplicationRegistries2
{
    public class AccessorBase
    {
        private readonly AccessorDefinition _accessorDefinition;
        internal AccessorBase(AccessorDefinition accessorDefinition)
        {
            _accessorDefinition = accessorDefinition;
        }

        public object Get(string name)
        {
            var field = _accessorDefinition.GetField(name);


            foreach (var accessor in _accessorDefinition.AccessToList)
            {
                if (accessor.Exists(field.Type, _accessorDefinition, field))
                {
                    return accessor.Read(field.Type, _accessorDefinition, field);
                }
            }

            throw new DataNotFoundException();
        }
    }
}