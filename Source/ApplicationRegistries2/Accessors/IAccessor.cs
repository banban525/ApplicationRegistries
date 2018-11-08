using System;

namespace ApplicationRegistries2.Accessors
{
    /// <summary>
    /// Interface to get various values
    /// </summary>
    public interface IAccessor
    {
        /// <summary>
        /// Read value
        /// </summary>
        /// <param name="returnType">Return type defined for the external setting interface</param>
        /// <param name="accessorDeclaration">accessor declaration</param>
        /// <param name="accessorFieldDeclaration">accessor field declaration</param>
        /// <returns></returns>
        object Read(Type returnType, AccessorTypeDeclaration accessorDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration);

        /// <summary>
        /// Returns whether the value exists
        /// </summary>
        /// <param name="fieldType">Property type defined in the external setting interface</param>
        /// <param name="accessorDeclaration">accessor declaration</param>
        /// <param name="accessorFieldDeclaration">accessor field declaration</param>
        /// <returns></returns>
        bool Exists(Type fieldType, AccessorTypeDeclaration accessorDeclaration, AccessorFieldDeclaration accessorFieldDeclaration);
        
    }
}