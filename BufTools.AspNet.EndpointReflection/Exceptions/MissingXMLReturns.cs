using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLReturns : Exception, IReportError
    {
        public MissingXMLReturns(MethodInfo methodInfo)
            : base($"XML Return field not found for the '{methodInfo.DeclaringType.Name}.{methodInfo.Name}' method")
        {
        }
    }
}
