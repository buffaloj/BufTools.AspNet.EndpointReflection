using System;
using System.Runtime.Serialization;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLDocumentationForMethod : Exception
    {
        public MissingXMLDocumentationForMethod()
        {
        }

        public MissingXMLDocumentationForMethod(string message) : base(message)
        {
        }

        public MissingXMLDocumentationForMethod(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingXMLDocumentationForMethod(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
