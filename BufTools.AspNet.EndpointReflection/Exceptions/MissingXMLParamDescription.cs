using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLParamDescription : Exception, IReportError
    {
        public MissingXMLParamDescription(string paramName, MethodInfo methodInfo)
            : base($"XML Description not found for the parameter '{paramName}' in method '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'")
        {
        }
    }
}
