using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    public class AccessorTypeDeclaration
    {
        public string Name { get; }
        public Type TargetInterfaceType { get; }

        public IEnumerable<AccessorFieldDeclaration> Fields { get; }

        public IEnumerable<IAccessor> AccessToList { get; }

        public IEnumerable<Attribute> Attributes;

        public IEnumerable<string> Keys { get; }

        public AccessorTypeDeclaration(string name, Type targetInterfaceType, 
            IReadOnlyCollection<AccessorFieldDeclaration> fields, IReadOnlyCollection<Attribute> attributes, 
            IReadOnlyCollection<IAccessor> accessToList,
            IReadOnlyCollection<string> keys)
        {
            Name = name;
            TargetInterfaceType = targetInterfaceType;
            Fields = fields;
            Attributes = attributes;
            AccessToList = accessToList;
            Keys = keys;
        }


        public T GetAttribute<T>() where T : Attribute
        {
            return (T)Attributes.FirstOrDefault(_ => _ is T);
        }

        public AccessorFieldDeclaration GetField(string name)
        {
            var result = Fields.FirstOrDefault(_ => _.Name == name);
            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }
    }
}