using System;
using JetBrains.Annotations;

namespace ApplicationRegistries2.Attributes
{
    /// <summary>
    /// Specify the type of Properties.Settings
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class PropertiesSettingsTypeAttribute : Attribute
    {
        /// <summary>
        /// Specify the type of Properties.Settings
        /// </summary>
        /// <param name="parent">Properties.Settings type</param>
        public PropertiesSettingsTypeAttribute([NotNull]Type parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <summary>
        /// Properties.Settings type
        /// </summary>
        [NotNull]
        public Type Parent { get; }
    }
}