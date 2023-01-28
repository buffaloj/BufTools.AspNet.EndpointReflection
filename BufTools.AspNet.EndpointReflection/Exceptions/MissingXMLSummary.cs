using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLSummary : Exception, IReportError
    {
        public MissingXMLSummary(MethodInfo methodInfo)
            : base($"XML Summary not found for the '{methodInfo.DeclaringType.Name}.{methodInfo.Name}' method")
        {
        }
    }
}
