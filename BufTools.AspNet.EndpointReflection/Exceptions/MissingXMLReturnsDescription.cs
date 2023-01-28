using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLReturnsDescription : Exception, IReportError
    {
        public MissingXMLReturnsDescription(MethodInfo methodInfo)
            : base($"XML Return exists but has no description for method '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
