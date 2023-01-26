
using System;
using System.Runtime.Serialization;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingParamInXMLDocumentation : Exception
    {
        public MissingParamInXMLDocumentation()
        {
        }

        public MissingParamInXMLDocumentation(string message) : base(message)
        {
        }

        public MissingParamInXMLDocumentation(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingParamInXMLDocumentation(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
