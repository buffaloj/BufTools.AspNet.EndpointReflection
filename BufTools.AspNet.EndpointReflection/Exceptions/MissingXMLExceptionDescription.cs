using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLExceptionDescription : Exception, IReportError
    {
        public MissingXMLExceptionDescription(string exceptionType, MethodInfo methodInfo)
            : base($"XML Exception Description not found for the exception '{exceptionType}' for '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
