using BufTools.AspNet.EndpointReflection.Exceptions;
using BufTools.AspNet.EndpointReflection.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// Combines reflection and XML comment parsing to finds the endpoints in the assembly and the details about them
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
                .Select(x => BuildEndpoint(x).AddXmlExampleRoute(x).AddXmlData(x))
                .OrderBy(x => x.ControllerType.Name)
                .ThenBy(x => x.MethodName);
        }

        private static HttpEndpoint BuildEndpoint(MethodInfo x)
        {
            return new HttpEndpoint
            {
                ControllerType = x.DeclaringType,
                Route = BuildRoute(x),
                ReturnType = x.ReturnType,
                Assembly = x.DeclaringType.Assembly,
                Verb = GetVerb(x),
                BodyPayloadType = GetPayloadType(x.GetParameters()),
                MethodName = x.Name,
                ResponseTypes = GetResponseTypes(x)
            };
        }

        private static HttpEndpoint AddXmlExampleRoute(this HttpEndpoint endpoint, MethodInfo x)
        {
            endpoint.ExampleRoute = ValidateAndBuildRouteFromXmlExamples(x, out var errors);
            endpoint.XmlRouteValidationErrors = errors;
            return endpoint;
        }

        private static HttpEndpoint AddXmlData(this HttpEndpoint endpoint, MethodInfo methodInfo)
        {
            var allErrors = new List<Exception>();
            endpoint.AllXmlValidationErrors = allErrors;

            var endpointParams = new List<EndpointParam>();
            endpoint.MethodParams = endpointParams;

            // Find missing route params
            var regex = new Regex("{(.*?)}");
            var routeParams = regex.Matches(endpoint.Route).Select(p => p.Groups[1].Value);
            var missing = routeParams.Where(rp => !methodInfo.GetParameters().Any(p => p.Name == rp))
                                     .Select(rp => new RouteParamMissingFromMethod(rp, endpoint.Route, methodInfo));
            allErrors.AddRange(missing);

            // Load XML documentation
            var assembly = methodInfo.DeclaringType.Assembly;
            var xmlDocs = assembly.LoadXmlDocumentation();
            if (xmlDocs == null)
            {
                allErrors.Add(new MissingXMLFileForAssembly(endpoint.Assembly));
                return endpoint;
            }

            var endpointDocs = xmlDocs.GetDocumentation(methodInfo);
            if (endpointDocs == null)
            {
                allErrors.Add(new MissingXMLDocumentationForMethod(methodInfo));
                return endpoint;
            }

            // Add the Summary
            endpoint.XmlSummary = endpointDocs.Summary;
            if (string.IsNullOrWhiteSpace(endpointDocs.Summary))
                allErrors.Add(new MissingXMLSummary(methodInfo));

            // Add the returns
            endpoint.XmlReturns = endpointDocs.Returns;
            if (endpointDocs.Returns != null && string.IsNullOrWhiteSpace(endpointDocs.Returns))
                allErrors.Add(new MissingXMLReturnsDescription(methodInfo));

            // Add the remarks
            endpoint.XmlRemarks = endpointDocs.Remarks;
            
            // Add the exception info
            endpoint.XmlExceptions = endpointDocs.Exceptions.Select(e => new EndpointException
            {
                XMLDescription = e.Description,
                ExceptionType = e.ExceptionType
            });
            allErrors.AddRange(endpoint.XmlExceptions
                .Where(e => !string.IsNullOrWhiteSpace(e.XMLDescription) &&
                            string.IsNullOrWhiteSpace(e.ExceptionType))
                .Select(e => new MissingXMLExceptionDescription(e.ExceptionType, methodInfo)));

            allErrors.AddRange(endpoint.XmlExceptions
                .Where(e => string.IsNullOrWhiteSpace(e.ExceptionType))
                .Select(e => new MissingXMLExceptionType(methodInfo)));

            foreach (var param in methodInfo.GetParameters())   
            {
                var paramDoc = endpointDocs.Params.Where(p => p.Name == param.Name).FirstOrDefault();
                if (paramDoc == null)
                    allErrors.Add(new MissingXMLDocumentationForParam(param.Name, methodInfo));

                if (paramDoc != null && string.IsNullOrWhiteSpace(paramDoc.Example))
                    allErrors.Add(new MissingXMLExampleForParam(param.Name, methodInfo));

                if (paramDoc != null && string.IsNullOrWhiteSpace(paramDoc.Description))
                    allErrors.Add(new MissingXMLParamDescription(param.Name, methodInfo));

                endpointParams.Add(new EndpointParam
                {
                    ParamType = param.ParameterType,
                    XmlExample = paramDoc?.Example,
                    XMLDescription = paramDoc?.Description,
                    UseageType = param.ToUseageType(endpoint.Route)
                });
            }

            return endpoint;
        }

        private static ParamUsageTypes ToUseageType(this ParameterInfo p, string route)
        {
            if (p.GetCustomAttribute<FromBodyAttribute>() != null)
                return ParamUsageTypes.Body;

            if (p.GetCustomAttribute<FromQueryAttribute>() != null)
                return ParamUsageTypes.Query;

            if (p.GetCustomAttribute<FromRouteAttribute>() != null)
                return ParamUsageTypes.Route;

            if (p.HasDefaultValue)// && p.DefaultValue == null )
                return ParamUsageTypes.Query;

            if (route.Contains("{" + p.Name + "}"))
                return ParamUsageTypes.Route;

            return ParamUsageTypes.Body;
        }

        private static string ValidateAndBuildRouteFromXmlExamples(MethodInfo methodInfo, out List<Exception> errors)
        {
            errors = new List<Exception>();

            var route = BuildRoute(methodInfo);
            var paramInfo = methodInfo.GetParameters();
            var assembly = methodInfo.DeclaringType.Assembly;

            var regex = new Regex("{(.*?)}");
            var routeParams = regex.Matches(route);
            var routeGood = true;
            foreach (Match routeParam in routeParams)
            {
                var paramName = routeParam.Groups[1].Value;
                var param = paramInfo.Where(p => p.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (param == null)
                {
                    routeGood = false;
                    errors.Add(new RouteParamMissingFromMethod(paramName, route, methodInfo));
                    continue;
                }

                var xmlDocs = assembly.LoadXmlDocumentation();
                if (xmlDocs == null)
                {
                    routeGood = false;
                    errors.Add(new MissingXMLFileForAssembly(assembly));
                    continue;
                }

                var endpointDocs = xmlDocs.GetDocumentation(methodInfo);
                if (endpointDocs == null)
                {
                    routeGood = false;
                    errors.Add(new MissingXMLDocumentationForMethod(methodInfo));
                    continue;
                }

                var paramDoc = endpointDocs.Params.Where(p => p.Name == paramName).FirstOrDefault();
                if (paramDoc == null)
                {
                    routeGood = false;
                    errors.Add(new MissingXMLDocumentationForParam(paramName, methodInfo));
                    continue;
                }

                if (string.IsNullOrWhiteSpace(paramDoc.Example))
                {
                    routeGood = false;
                    errors.Add(new MissingXMLExampleForParam(paramName, methodInfo));
                    continue;
                }

                route = route.Replace(routeParam.Value, paramDoc.Example);
            }

            return routeGood ? route : "";
        }

        private static IDictionary<HttpStatusCode, Type> GetResponseTypes(MethodInfo info)
        {
            return info.GetCustomAttributes<ProducesResponseTypeAttribute>()
                       .ToDictionary(p => (HttpStatusCode)p.StatusCode, p => p.Type);
        }

        private static string BuildRoute(MethodInfo info)
        {
            return BuildRoute(
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

        private static string BuildRoute(params Attribute[] attributes)
        {
            var route = "";
            foreach (var attribute in attributes)
                route += attribute.Route();
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
