using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLDocumentationForMethod : Exception
    {
        public MissingXMLDocumentationForMethod(MethodInfo methodInfo)
            : base($"XML Documentation not found for method '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
