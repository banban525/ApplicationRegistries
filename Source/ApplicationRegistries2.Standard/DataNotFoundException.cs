using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ApplicationRegistries2
{
    /// <summary>
    /// Exception thrown when value is not found
    /// </summary>
    [Serializable]
    public class DataNotFoundException : Exception
    {
        /// <summary>
        /// Exception thrown when value is not found
        /// </summary>
        public DataNotFoundException()
        {
        }

        /// <summary>
        /// Exception thrown when value is not found
        /// </summary>
        /// <param name="message">message</param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public DataNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Exception thrown when value is not found
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="inner">inner exception</param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public DataNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
        /// <inheritdoc />
        protected DataNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}