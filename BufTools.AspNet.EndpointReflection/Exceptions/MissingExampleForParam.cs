using System;
using System.Runtime.Serialization;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingExampleForParam : Exception
    {
        public MissingExampleForParam()
        {
        }

        public MissingExampleForParam(string message) : base(message)
        {
        }

        public MissingExampleForParam(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingExampleForParam(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
