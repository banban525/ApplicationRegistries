using System;

namespace ApplicationRegistries
{
    enum TypeEnum
    {
        String,
        Int32,
        Boolean,
    }


    static class TypeEnumExtensions
    {
        public static TypeEnum ToDomainType(this GeneratedXmlObject.TypeEnum type)
        {
            switch (type)
            {
                case GeneratedXmlObject.TypeEnum.@bool:
                    return TypeEnum.Boolean;
                case GeneratedXmlObject.TypeEnum.@int:
                    return TypeEnum.Int32;
                case GeneratedXmlObject.TypeEnum.@string:
                    return TypeEnum.String;
            }
            throw new ArgumentException();
        }
    }
}