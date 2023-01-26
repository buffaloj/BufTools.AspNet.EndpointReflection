using System;
using System.Runtime.Serialization;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLDocumentationForParam : Exception
    {
        public MissingXMLDocumentationForParam()
        {
        }

        public MissingXMLDocumentationForParam(string message) : base(message)
        {
        }

        public MissingXMLDocumentationForParam(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingXMLDocumentationForParam(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
