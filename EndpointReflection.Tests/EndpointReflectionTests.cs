using Microsoft.VisualStudio.TestTools.UnitTesting;
using BufTools.AspNet.EndpointReflection;
using BufTools.AspNet.EndpointReflection.Exceptions;

namespace Reflectamundo.Asp.Tests
{
    [TestClass]
    public class EndpointReflectionTests
    {
        [TestMethod]
        public void GetEndpoints_WithValidAssembly_GetsEndpoints()
        {
            var assembly = typeof(TestWebApi.Controllers.ExampleController).Assembly;
            
            var endpoints = assembly.GetEndpoints().ToList();

            Assert.IsNotNull(endpoints);
            Assert.IsTrue(endpoints.All(e => !string.IsNullOrEmpty(e.Route)));
            Assert.IsTrue(endpoints.Any(e => !string.IsNullOrEmpty(e.ExampleRoute)));
            Assert.IsTrue(endpoints.All(e => !string.IsNullOrEmpty(e.MethodName)));
            Assert.IsTrue(endpoints.All(e => e.ReturnType != null));
            Assert.IsTrue(endpoints.All(e => e.ControllerType != null));
            Assert.IsTrue(endpoints.All(e => e.Assembly != null));
            Assert.IsTrue(endpoints.Any(e => e.ResponseTypes != null && e.ResponseTypes.Any()));
            Assert.IsTrue(endpoints.Any(e => e.XmlRouteValidationErrors != null));
            Assert.IsTrue(endpoints.Any(e => e.XmlRouteValidationErrors.Any(err => err is MissingXMLExampleForParam)));
            Assert.IsTrue(endpoints.Any(e => e.XmlRouteValidationErrors.Any(err => err is MissingXMLDocumentationForMethod)));
            Assert.IsTrue(endpoints.Any(e => e.XmlRouteValidationErrors.Any(err => err is MissingXMLDocumentationForParam)));
            Assert.IsTrue(endpoints.Any(e => e.XmlRouteValidationErrors.Any(err => err is RouteParamMissingFromMethod)));
            Assert.IsTrue(endpoints.All(e => e.AllXmlValidationErrors != null));
            Assert.IsTrue(endpoints.Any(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLParamDescription)));
            Assert.IsTrue(endpoints.Any(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLDocumentationForParam)));
            Assert.IsTrue(endpoints.Any(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLSummary)));
            Assert.IsTrue(endpoints.Any(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLDocumentationForMethod)));
            Assert.IsTrue(endpoints.Any(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLExampleForParam)));
            Assert.IsTrue(endpoints.Any(e => e.AllXmlValidationErrors.Any(err => err is RouteParamMissingFromMethod)));
            Assert.IsTrue(endpoints.Count(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLExceptionDescription)) == 1);
            Assert.IsTrue(endpoints.Count(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLExceptionType)) == 1);
            Assert.IsTrue(endpoints.Count(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLReturnsDescription)) == 1);
        }

        [TestMethod]
        public void GetEndpoints_WhenXmlCommentsNotIncludedInBuild_CompletesWithValidationErrors()
        {
            var assembly = typeof(TestWebApiNoXml.Controllers.ExampleController).Assembly;

            var endpoints = assembly.GetEndpoints().ToList();

            Assert.IsNotNull(endpoints);
            Assert.IsTrue(endpoints.Count(e => e.XmlRouteValidationErrors.Any(err => err is MissingXMLFileForAssembly)) == 2);
            Assert.IsTrue(endpoints.Count(e => e.AllXmlValidationErrors.Any(err => err is MissingXMLFileForAssembly)) == 2);

            Assert.IsTrue(endpoints.Count(e => string.IsNullOrEmpty(e.ExampleRoute) ) == 2);
        }
    }
}