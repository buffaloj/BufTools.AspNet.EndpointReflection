using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflectamundo.TestWebApi.Controllers;
using System.Reflection;
using BufTools.AspNet.EndpointReflection;
using BufTools.AspNet.EndpointReflection.Exceptions;

namespace Reflectamundo.Asp.Tests
{
    [TestClass]
    public class EndpointReflectionTests
    {
        private readonly Assembly _assembly;

        public EndpointReflectionTests()
        {
            _assembly = typeof(ExampleController).Assembly;
        }

        [TestMethod]
        public void GetEndpoints_WithValidAssembly_GetsEndpoints()
        {
            var endpoints = _assembly.GetEndpoints().ToList();

            Assert.IsNotNull(endpoints);
            Assert.IsTrue(endpoints.All(e => !string.IsNullOrEmpty(e.Route)));
            Assert.IsTrue(endpoints.All(e => !string.IsNullOrEmpty(e.ExampleRoute)));
            Assert.IsTrue(endpoints.All(e => !string.IsNullOrEmpty(e.EndpointMethodName)));
            Assert.IsTrue(endpoints.All(e => e.Parameters != null));
            Assert.IsTrue(endpoints.All(e => e.MethodInfo != null));
            Assert.IsTrue(endpoints.All(e => e.ReturnType != null));
            Assert.IsTrue(endpoints.All(e => e.ControllerType != null));
            Assert.IsTrue(endpoints.All(e => e.Assembly != null));
            Assert.IsTrue(endpoints.Any(e => e.ResponseTypes != null && e.ResponseTypes.Any()));
            Assert.IsTrue(endpoints.Any(e => e.XmlValidationErrors != null));
            Assert.IsTrue(endpoints.Any(e => e.XmlValidationErrors != null && e.XmlValidationErrors.Any(err => err is MissingXMLExampleForParam)));
            Assert.IsTrue(endpoints.Any(e => e.XmlValidationErrors != null && e.XmlValidationErrors.Any(err => err is MissingXMLDocumentationForMethod)));
            Assert.IsTrue(endpoints.Any(e => e.XmlValidationErrors != null && e.XmlValidationErrors.Any(err => err is MissingXMLExampleForParam)));
            Assert.IsTrue(endpoints.Any(e => e.XmlValidationErrors != null && e.XmlValidationErrors.Any(err => err is RouteParamMissingFromMethod)));
        }
    }
}