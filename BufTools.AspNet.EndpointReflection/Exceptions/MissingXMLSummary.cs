using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLSummary : Exception
    {
        public MissingXMLSummary(MethodInfo methodInfo)
            : base($"XML Summary not found for the method '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
