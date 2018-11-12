using System;
using System.Linq;
using JetBrains.Annotations;

namespace ApplicationRegistries2
{
    /// <summary>
    /// Field declaration
    /// </summary>
    public class AccessorFieldDeclaration
    {
        /// <summary>
        /// field name
        /// </summary>
        [NotNull]
        public string Name { get; }
        /// <summary>
        /// field type
        /// </summary>
        [NotNull]
        public Type Type { get; }

        private readonly Attribute[] _attributes;

        internal AccessorFieldDeclaration([NotNull]string name, [NotNull]Type type, [NotNull]Attribute[] attributes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            _attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        }

        /// <summary>
        /// Get attribute 
        /// </summary>
        /// <typeparam name="T">attribute type</typeparam>
        /// <returns>attribute</returns>
        [CanBeNull]
        public T GetAttribute<T>() where T : Attribute
        {
            return (T)_attributes.FirstOrDefault(_ => _ is T);
        }
    }


}
