using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class RouteParamMissingFromMethod : Exception, IReportError
    {
        public RouteParamMissingFromMethod(string paramName, string route, MethodInfo methodInfo) :
            base($"{paramName} in route '{route}' does not have a parameter in {methodInfo.DeclaringType.Name}.{methodInfo.Name}")
        {

        }
    }
}
