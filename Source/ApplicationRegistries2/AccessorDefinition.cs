using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    public class AccessorDefinition
    {
        public string Name { get; }
        public Type TargetInterfaceType { get; }

        public IEnumerable<AccessorFieldDefinition> Fields { get; }

        public IEnumerable<IAccessor> AccessToList { get; }

        public IEnumerable<Attribute> Attributes;

        public AccessorDefinition(string name, Type targetInterfaceType, 
            IReadOnlyCollection<AccessorFieldDefinition> fields, IReadOnlyCollection<Attribute> attributes, 
            IReadOnlyCollection<IAccessor> accessToList)
        {
            Name = name;
            TargetInterfaceType = targetInterfaceType;
            Fields = fields;
            Attributes = attributes;
            AccessToList = accessToList;
        }


        public T GetAttribute<T>() where T : Attribute
        {
            return (T)Attributes.FirstOrDefault(_ => _ is T);
        }

        public AccessorFieldDefinition GetField(string name)
        {
            return Fields.FirstOrDefault(_ => _.Name == name) ?? throw new InvalidOperationException();
        }
    }
}