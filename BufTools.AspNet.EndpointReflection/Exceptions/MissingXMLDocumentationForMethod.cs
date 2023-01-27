using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLDocumentationForMethod : Exception, IReportError
    {
        public MissingXMLDocumentationForMethod(MethodInfo methodInfo)
            : base($"XML Documentation not found for the '{methodInfo.DeclaringType.Name}.{methodInfo.Name}' method")
        {
        }
    }
}
