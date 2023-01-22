using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Reflection;
using static BufTools.AspNet.EndpointReflection.HttpEndpoint;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    /// <summary>
    /// This should never happen and will noly be thrown if there is an internal mismatch between the <see cref="Verbs"/> enum and 
    /// <see cref="HttpMethodAttribute"/> that are being searched for.
    /// </summary>
    public class VerbNotSupported : Exception
    {
        /// <summary>
        /// Creates an instance of an obkect
        /// </summary>
        /// <param name="methodInfo">The method that contains the unsupported <see cref="HttpMethodAttribute"/></param>
        public VerbNotSupported(MethodInfo methodInfo)
            : base($"{methodInfo.DeclaringType.Name}.{methodInfo.Name} does not have an Http Verb Attribute(HttpGet, HttpPut, HttpPost, HttpDelete)")
        {
        }
    }
}
