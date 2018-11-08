using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationRegistries2.Accessors;

namespace ApplicationRegistries2
{
    /// <summary>
    /// Type declaration
    /// </summary>
    public class AccessorTypeDeclaration
    {
        /// <summary>
        /// Type name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Type object
        /// </summary>
        public Type TargetInterfaceType { get; }

        /// <summary>
        /// Field collection
        /// </summary>
        public IEnumerable<AccessorFieldDeclaration> Fields { get; }

        /// <summary>
        /// Accessor collection
        /// </summary>
        public IEnumerable<IAccessor> AccessToList { get; }

        /// <summary>
        /// attribute collection
        /// </summary>
        public IEnumerable<Attribute> Attributes;

        /// <summary>
        /// The keys specified in the ApplicationRegistry attribute
        /// </summary>
        public IEnumerable<string> Keys { get; }

        /// <summary>
        /// Type declaration
        /// </summary>
        /// <param name="name">type name</param>
        /// <param name="targetInterfaceType">interface type object</param>
        /// <param name="fields">field collection</param>
        /// <param name="attributes">attribute collection</param>
        /// <param name="accessToList">accessor collection</param>
        /// <param name="keys">The keys specified in the ApplicationRegistry attribute</param>
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

        /// <summary>
        /// get attribute
        /// </summary>
        /// <typeparam name="T">attribute type</typeparam>
        /// <returns>attribute object</returns>
        public T GetAttribute<T>() where T : Attribute
        {
            return (T)Attributes.FirstOrDefault(_ => _ is T);
        }

        /// <summary>
        /// get field
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>field declaration</returns>
        public AccessorFieldDeclaration GetField(string name)
        {
            return Fields.FirstOrDefault(_ => _.Name == name) ?? throw new InvalidOperationException();
        }
    }
}