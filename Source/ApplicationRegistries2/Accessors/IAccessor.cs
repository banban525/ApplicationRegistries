﻿using System;

namespace ApplicationRegistries2.Accessors
{
    public interface IAccessor
    {
        object Read(Type returnType, AccessorTypeDeclaration accessorDeclaration,
            AccessorFieldDeclaration accessorFieldDeclaration);

        bool Exists(Type fieldType, AccessorTypeDeclaration accessorDeclaration, AccessorFieldDeclaration accessorFieldDeclaration);
        
    }
}