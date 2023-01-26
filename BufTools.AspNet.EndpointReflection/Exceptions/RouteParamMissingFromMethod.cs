using System;
using System.Runtime.Serialization;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class RouteParamMissingFromMethod : Exception
    {
        public RouteParamMissingFromMethod()
        {
        }

        public RouteParamMissingFromMethod(string message) : base(message)
        {
        }

        public RouteParamMissingFromMethod(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RouteParamMissingFromMethod(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
