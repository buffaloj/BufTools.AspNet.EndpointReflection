using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLExceptionDescription : Exception
    {
        public MissingXMLExceptionDescription(string exceptionType, MethodInfo methodInfo)
            : base($"XML Exception Description not found for the exception '{exceptionType}' for method '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
