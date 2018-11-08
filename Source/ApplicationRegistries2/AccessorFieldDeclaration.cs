using System;
using System.Linq;

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
        public string Name { get; }
        /// <summary>
        /// field type
        /// </summary>
        public Type Type { get; }

        private readonly Attribute[] _attributes;

        internal AccessorFieldDeclaration(string name, Type type, Attribute[] attributes)
        {
            Name = name;
            Type = type;
            _attributes = attributes;
        }

        /// <summary>
        /// Get attribute 
        /// </summary>
        /// <typeparam name="T">attribute type</typeparam>
        /// <returns>attribute</returns>
        public T GetAttribute<T>() where T : Attribute
        {
            return (T)_attributes.FirstOrDefault(_ => _ is T);
        }
    }


}
