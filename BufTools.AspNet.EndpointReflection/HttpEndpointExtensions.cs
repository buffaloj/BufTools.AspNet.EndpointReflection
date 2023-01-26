using BufTools.AspNet.EndpointReflection.Exceptions;
using BufTools.AspNet.EndpointReflection.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BufTools.AspNet.EndpointReflection
{
    /// <summary>
    /// An extension class that support finding endpoints in an <see cref="Assembly"/>
    /// </summary>
    public static class HttpEndpointExtensions
    {
        /// <summary>
        /// Using reflection, this method finds the endpoints in the assembly
        /// </summary>
        /// <param name="assembly">The assembly to search within to find endpoints</param>
        /// <returns>A collection of <see cref="HttpEndpoint"/> instances.  One for each endpoint.</returns>
        public static IEnumerable<HttpEndpoint> GetEndpoints(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any() &&
                            m.GetCustomAttributes(typeof(HttpMethodAttribute), true).Any())
                .Select(x => new HttpEndpoint
                {
                    ControllerType = x.DeclaringType,
                    Route = MakeRoute(x),
                    ExampleRoute = MakeRouteFromXmlExamples(x),
                    Parameters = x.GetParameters(),
                    MethodInfo = x,
                    ReturnType = x.ReturnType,
                    Assembly = x.DeclaringType.Assembly,
                    Verb = GetVerb(x),
                    BodyPayloadType = GetPayloadType(x.GetParameters()),
                    EndpointMethodName = x.Name,
                    ResponseTypes = GetResponseTypes(x)
                })
                .OrderBy(x => x.ControllerType.Name)
                .ThenBy(x => x.MethodInfo.Name);
        }

        private static IDictionary<HttpStatusCode, Type> GetResponseTypes(MethodInfo info)
        {
            return info.GetCustomAttributes<ProducesResponseTypeAttribute>()
                       .ToDictionary(p => (HttpStatusCode)p.StatusCode, p => p.Type);
        }

        private static string MakeRoute(MethodInfo info)
        {
            return MakeRoute(
                info.DeclaringType.GetCustomAttribute<RouteAttribute>(),
                info.GetCustomAttribute<RouteAttribute>(),
                info.GetCustomAttribute<HttpGetAttribute>(),
                info.GetCustomAttribute<HttpPutAttribute>(),
                info.GetCustomAttribute<HttpPostAttribute>(),
                info.GetCustomAttribute<HttpDeleteAttribute>(),
                info.GetCustomAttribute<HttpPatchAttribute>(),
                info.GetCustomAttribute<HttpOptionsAttribute>(),
                info.GetCustomAttribute<HttpHeadAttribute>());
        }

        private static string MakeRoute(params Attribute[] attributes)
        {
            var route = "";
            foreach (var attribute in attributes)
                route += attribute.Route();
            return route;
        }

        private static string MakeRouteFromXmlExamples(MethodInfo methodInfo)
        {
            var route = MakeRoute(methodInfo);
            var paramInfo = methodInfo.GetParameters();
            var assembly = methodInfo.DeclaringType.Assembly;
            var controllerType = methodInfo.DeclaringType;

            var loadedAssemblies = new HashSet<Assembly>();

            var regex = new Regex("{(.*?)}");
            var matches = regex.Matches(route);
            foreach (Match match in matches)
            {
                var paramName = match.Groups[1].Value;
                var param = paramInfo.Where(p => p.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (param == null)
                    throw new RouteParamMissingFromMethod($"{paramName} in route '{route}' does not have a parameter in {controllerType.Name}.{methodInfo.Name}");

                var xmlDocs = assembly.LoadXmlDocumentation();
                var endpointDocs = xmlDocs.GetDocumentation(methodInfo);
                if (endpointDocs == null)
                    throw new MissingXMLDocumentationForMethod($"XML Documentation not found for the '{controllerType.Name}.{methodInfo.Name}' method");

                var paramDoc = endpointDocs.Params.Where(p => p.Name == paramName).FirstOrDefault();
                if (paramDoc == null)
                    throw new MissingXMLDocumentationForParam($"<param ...> XML tag not found for the '{paramName}' parameter in '{controllerType.Name}.{methodInfo.Name}'" +
                                                              $"Try:\n <param name=\"{paramName}\" example=\"blah\">");

                if (string.IsNullOrWhiteSpace(paramDoc.Example))
                    throw new MissingXMLExampleForParam($"The <param ...> XML tag does not have an example value for the '{paramName}' parameter of the '{controllerType.Name}.{methodInfo.Name}' method\n" +
                                                        $"Try:\n <param name=\"{paramName}\" example=\"blah\">");

                route = route.Replace(match.Value, paramDoc.Example);
            }

            return route;
        }

        private static Type GetPayloadType(ParameterInfo[] parameters)
        {
            return parameters.Where(p => p.GetCustomAttribute<FromBodyAttribute>() != null)
                             .Select(p => p.ParameterType).FirstOrDefault();
        }

        private static HttpEndpoint.Verbs GetVerb(MethodInfo info)
        {
            if (info.GetCustomAttribute<HttpGetAttribute>() != null)
                return HttpEndpoint.Verbs.Get;

            if (info.GetCustomAttribute<HttpPutAttribute>() != null)
                return HttpEndpoint.Verbs.Put;

            if (info.GetCustomAttribute<HttpPostAttribute>() != null)
                return HttpEndpoint.Verbs.Post;

            if (info.GetCustomAttribute<HttpDeleteAttribute>() != null)
                return HttpEndpoint.Verbs.Delete;

            if (info.GetCustomAttribute<HttpPatchAttribute>() != null)
                return HttpEndpoint.Verbs.Patch;

            if (info.GetCustomAttribute<HttpOptionsAttribute>() != null)
                return HttpEndpoint.Verbs.Options;

            if (info.GetCustomAttribute<HttpHeadAttribute>() != null)
                return HttpEndpoint.Verbs.Head;

            throw new VerbNotSupported(info);
        }

        private static string Route(this Attribute attribute)
        {
            if (attribute == null)
                return "";

            if (attribute is RouteAttribute)
                return (attribute as RouteAttribute)?.Template.ToRouteSegment() ?? "";

            if (attribute is HttpMethodAttribute)
                return (attribute as HttpMethodAttribute)?.Template.ToRouteSegment() ?? "";

            return "";
        }

        private static string ToRouteSegment(this string route)
        {
            if (string.IsNullOrWhiteSpace(route))
                return "";

            return "/" + route.Trim(new char[] { '/', ' ', '\\' });
        }
    }
}
