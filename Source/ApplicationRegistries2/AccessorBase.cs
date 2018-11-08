﻿namespace ApplicationRegistries2
{
    /// <summary>
    /// Internal class for access to external value
    /// </summary>
    public class AccessorBase
    {
        private readonly AccessorTypeDeclaration _accessorTypeDeclaration;
        internal AccessorBase(AccessorTypeDeclaration accessorDeclaration)
        {
            _accessorTypeDeclaration = accessorDeclaration;
        }

        /// <summary>
        /// load value
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>loaded value</returns>
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