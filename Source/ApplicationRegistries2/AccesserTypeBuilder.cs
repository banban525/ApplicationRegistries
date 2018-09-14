using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using ApplicationRegistries2.Attributes;


namespace ApplicationRegistries2
{
    class AccessorTypeBuilder
    {

        internal AccessorDefinition Parse(Type type, AccessorRepository repository)
        {
            if (type.IsInterface == false)
            {
                throw new NotSupportedException();
            }

            if (ApplicationRegistryAttribute.IsDefined(type) == false)
            {
                throw new NotSupportedException();
            }

            var att = ApplicationRegistryAttribute.Get(type);

            if (att.Keys.All(repository.ExistsKey) == false)
            {
                throw new NotSupportedException();
            }

            var accessor = att.Keys.Select(repository.GetAccessor).ToArray();

            var fields = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(propertyInfo => new AccessorFieldDefinition(
                    propertyInfo.Name,
                    propertyInfo.PropertyType,
                    Attribute.GetCustomAttributes(propertyInfo, true)));

            return new AccessorDefinition(type.Name, type,
                fields.ToArray(), 
                Attribute.GetCustomAttributes(type, true),
                accessor);
        }

        internal TypeInfo Build(AccessorDefinition definition)
        {
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName("DynamicAssembly"),
                AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(
                "DynamicModule");

            var typeBuilder = moduleBuilder.DefineType(
                $"{definition.Name}_dynamic",
                TypeAttributes.Public | TypeAttributes.AutoLayout | TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit);

            
            typeBuilder.AddInterfaceImplementation(definition.TargetInterfaceType);

            // Field
            var accessorBaseField = typeBuilder.DefineField(
                "_accessorBase",
                typeof(AccessorBase),
                FieldAttributes.Private | FieldAttributes.InitOnly);

            // Construnctor
            {
                var construnBuilder = typeBuilder.DefineConstructor(
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName |
                    MethodAttributes.RTSpecialName,
                    CallingConventions.Standard,
                    new[] { typeof(AccessorBase) });
                var ilGenerator = construnBuilder.GetILGenerator();
                ilGenerator.Emit(OpCodes.Ldarg_0);
                // ReSharper disable once AssignNullToNotNullAttribute
                ilGenerator.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Stfld, accessorBaseField);
                ilGenerator.Emit(OpCodes.Ret);
            }

            foreach (var accessorFieldDefinition in definition.Fields)
            {
                
                var property = typeBuilder.DefineProperty(
                    accessorFieldDefinition.Name,
                    PropertyAttributes.None,
                    accessorFieldDefinition.Type,
                    null);
                var method = typeBuilder.DefineMethod(
                    $"get_{accessorFieldDefinition.Name}",
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot |
                    MethodAttributes.SpecialName | MethodAttributes.Virtual | MethodAttributes.Final,
                    accessorFieldDefinition.Type,
                    Type.EmptyTypes);

                property.SetGetMethod(method);

                var ilGenerator = method.GetILGenerator();
                

                var baseGetMethod = typeof(AccessorBase).GetMethod("Get", BindingFlags.Instance | BindingFlags.Public) ??
                                throw new InvalidOperationException();

                ilGenerator.DeclareLocal(accessorFieldDefinition.Type);

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, accessorBaseField);
                ilGenerator.Emit(OpCodes.Ldstr, accessorFieldDefinition.Name);
                ilGenerator.Emit(OpCodes.Callvirt, baseGetMethod);
                if (accessorFieldDefinition.Type.IsValueType)
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, accessorFieldDefinition.Type);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Castclass, accessorFieldDefinition.Type);
                }
                ilGenerator.Emit(OpCodes.Stloc_0);
                ilGenerator.Emit(OpCodes.Ldloc_0);
                ilGenerator.Emit(OpCodes.Ret);
            }
            return typeBuilder.CreateTypeInfo();
            

            {

                var property = typeBuilder.DefineProperty(
                    "SettingsFolder",
                    PropertyAttributes.None,
                    typeof(int),
                    null);
                var method = typeBuilder.DefineMethod(
                    "get_SettingsFolder",
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot |
                    MethodAttributes.SpecialName | MethodAttributes.Virtual | MethodAttributes.Final,
                    typeof(int),
                    Type.EmptyTypes);


                property.SetGetMethod(method);

                var ilGenerator = method.GetILGenerator();

                var baseGetMethod = typeof(AccessorBase).GetMethod("Get", BindingFlags.Instance | BindingFlags.Public) ??
                                throw new InvalidOperationException();

                LocalBuilder a = ilGenerator.DeclareLocal(typeof(int));

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, accessorBaseField);
                ilGenerator.Emit(OpCodes.Ldstr, "SettingsFolder");
                ilGenerator.Emit(OpCodes.Callvirt, baseGetMethod);
                ilGenerator.Emit(OpCodes.Unbox_Any, typeof(int));
                ilGenerator.Emit(OpCodes.Stloc_0);
                ilGenerator.Emit(OpCodes.Ldloc_0);
                ilGenerator.Emit(OpCodes.Ret);
            }

            var typeInfo = typeBuilder.CreateTypeInfo();
            return typeInfo;
        }
    }
}
