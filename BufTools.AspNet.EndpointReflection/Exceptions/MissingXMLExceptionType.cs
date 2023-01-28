using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLExceptionType : Exception
    {
        public MissingXMLExceptionType(MethodInfo methodInfo)
            : base($"XML exception type missing in comment on method '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
