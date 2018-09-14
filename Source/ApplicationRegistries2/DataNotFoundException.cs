using System;
using System.Runtime.Serialization;

namespace ApplicationRegistries2
{
    /// <inheritdoc />
    [Serializable]
    public class DataNotFoundException : Exception
    {

        public DataNotFoundException()
        {
        }

        public DataNotFoundException(string message) : base(message)
        {
        }

        public DataNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DataNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}