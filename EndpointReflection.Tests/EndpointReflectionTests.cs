using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflectamundo.TestWebApi.Controllers;
using System.Reflection;
using BufTools.AspNet.EndpointReflection;

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
            var endpoints = _assembly.GetEndpoints();

            Assert.IsNotNull(endpoints);
        }
    }
}