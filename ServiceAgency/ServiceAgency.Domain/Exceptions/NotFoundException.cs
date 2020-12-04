using System;
using System.Runtime.Serialization;

namespace ServiceAgency.Domain.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        private int id;
        private Type type;

        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(int id, Type type)
        {
            this.id = id;
            this.type = type;
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}