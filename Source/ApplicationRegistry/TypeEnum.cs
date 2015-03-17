using System;

namespace ApplicationRegistries
{
    enum TypeEnum
    {
        String,
        Int32,
        Boolean,
        StringArray,
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
                case GeneratedXmlObject.TypeEnum.string1:
                    return TypeEnum.StringArray;
            }
            throw new ArgumentException();
        }

        public static bool IsArray(this TypeEnum type)
        {
            return type == TypeEnum.StringArray;
        }
    }
}