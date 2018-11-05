using System;
using System.Reflection;
using ApplicationRegistries2.Accessors;
using ApplicationRegistries2.Attributes;
using NUnit.Framework;

namespace ApplicationRegistries2.Test
{
    [TestFixture]
    public class CustomAccessorTest
    {
        [Test]
        public void 定義したカスタムアクセッサを使えるか()
        {
            ApplicationRegistry.RegistCustomAccessor("CUSTOM", new CustomAccessor());

            Assert.That(ApplicationRegistry.Get<ICustomRegistry>().AssemblyName,
                Is.EqualTo(Assembly.GetExecutingAssembly().GetName().Name));
        }

        [ApplicationRegistry("CUSTOM")]
        public interface ICustomRegistry
        {
            string AssemblyName { get; }
        }

        class CustomAccessor : IAccessor
        {
            public object Read(Type returnType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
            {
                if (accessorFieldDeclaration.Name == "AssemblyName")
                {
                    return Assembly.GetExecutingAssembly().GetName().Name;
                }
                throw new DataNotFoundException();
            }

            public bool Exists(Type fieldType, AccessorTypeDeclaration accessorTypeDeclaration, AccessorFieldDeclaration accessorFieldDeclaration)
            {
                return accessorFieldDeclaration.Name == "AssemblyName";
            }
            
        }
    }
}
