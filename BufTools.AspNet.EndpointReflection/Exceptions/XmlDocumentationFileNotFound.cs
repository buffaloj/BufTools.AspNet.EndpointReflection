using System;
using System.Runtime.Serialization;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class XmlDocumentationFileNotFound : Exception
    {
        public XmlDocumentationFileNotFound()
        {
        }

        public XmlDocumentationFileNotFound(string message) : base(message)
        {
        }

        public XmlDocumentationFileNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XmlDocumentationFileNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
