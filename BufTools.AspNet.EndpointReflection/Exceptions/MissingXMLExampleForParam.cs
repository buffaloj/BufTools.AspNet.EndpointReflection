using System;
using System.Runtime.Serialization;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLExampleForParam : Exception
    {
        public MissingXMLExampleForParam()
        {
        }

        public MissingXMLExampleForParam(string message) : base(message)
        {
        }

        public MissingXMLExampleForParam(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingXMLExampleForParam(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
