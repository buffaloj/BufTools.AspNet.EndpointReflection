using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLDocumentationForParam : Exception
    {
        public MissingXMLDocumentationForParam(string paramName, MethodInfo methodInfo)
            : base($"<param ...> XML tag not found for the '{paramName}' parameter in '{methodInfo.DeclaringType.Name}.{methodInfo.Name}'" +
                   $"Try:\n <param name=\"{paramName}\" example=\"blah\">")
        {
        }
    }
}
