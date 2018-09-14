using System;
using System.Linq;

namespace ApplicationRegistries2
{
    public class AccessorFieldDefinition
    {
        public string Name { get; }
        public Type Type { get; }

        private readonly Attribute[] _attributes;

        internal AccessorFieldDefinition(string name, Type type, Attribute[] attributes)
        {
            Name = name;
            Type = type;
            _attributes = attributes;
        }

        public T GetAttribute<T>() where T : Attribute
        {
            return (T)_attributes.FirstOrDefault(_ => _ is T);
        }
    }


}
