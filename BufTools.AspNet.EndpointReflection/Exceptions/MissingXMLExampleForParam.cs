using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLExampleForParam : Exception
    {
        public MissingXMLExampleForParam(string paramName, MethodInfo methodInfo)
            : base($"The <param ...> XML tag does not have an example value for the '{paramName}' parameter of the '{methodInfo.DeclaringType.Name}.{methodInfo.Name}' method\n" +
                   $"Try:\n <param name=\"{paramName}\" example=\"blah\">")
        {
        }
    }
}
