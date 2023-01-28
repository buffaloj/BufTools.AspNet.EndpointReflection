using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLExceptionType : Exception, IReportError
    {
        public MissingXMLExceptionType(MethodInfo methodInfo)
            : base($"XML exception type missing in comment on '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
