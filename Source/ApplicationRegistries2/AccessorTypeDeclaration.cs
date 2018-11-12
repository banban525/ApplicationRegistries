using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

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
        [NotNull]
        public string Name { get; }
        /// <summary>
        /// Type object
        /// </summary>
        [NotNull]
        public Type TargetInterfaceType { get; }

        /// <summary>
        /// Field collection
        /// </summary>
        [NotNull]
        public IEnumerable<AccessorFieldDeclaration> Fields { get; }

        /// <summary>
        /// attribute collection
        /// </summary>
        [NotNull]
        public IEnumerable<Attribute> Attributes;

        /// <summary>
        /// The keys specified in the ApplicationRegistry attribute
        /// </summary>
        [NotNull]
        public IEnumerable<string> Keys { get; }

        /// <summary>
        /// Type declaration
        /// </summary>
        /// <param name="name">type name</param>
        /// <param name="targetInterfaceType">interface type object</param>
        /// <param name="fields">field collection</param>
        /// <param name="attributes">attribute collection</param>
        /// <param name="keys">The keys specified in the ApplicationRegistry attribute</param>
        public AccessorTypeDeclaration([NotNull]string name, [NotNull]Type targetInterfaceType,
            [NotNull]IReadOnlyCollection<AccessorFieldDeclaration> fields, [NotNull]IReadOnlyCollection<Attribute> attributes,
            [NotNull]IReadOnlyCollection<string> keys)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TargetInterfaceType = targetInterfaceType ?? throw new ArgumentNullException(nameof(targetInterfaceType));
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
            Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            Keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }

        /// <summary>
        /// get attribute
        /// </summary>
        /// <typeparam name="T">attribute type</typeparam>
        /// <returns>attribute object</returns>
        [CanBeNull]
        public T GetAttribute<T>() where T : Attribute
        {
            return (T)Attributes.FirstOrDefault(_ => _ is T);
        }

        /// <summary>
        /// get field
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>field declaration</returns>
        /// <exception cref="InvalidOperationException">field not found.</exception>
        [NotNull]
        public AccessorFieldDeclaration GetField(string name)
        {
            return Fields.FirstOrDefault(_ => _.Name == name) ?? throw new InvalidOperationException();
        }
    }
}