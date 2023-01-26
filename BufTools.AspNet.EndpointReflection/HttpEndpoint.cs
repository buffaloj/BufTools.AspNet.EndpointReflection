using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection
{
    /// <summary>
    /// A model representing an HTTP endpoint that exists within an assembly
    /// </summary>
    public partial class HttpEndpoint
    {
        /// <summary>
        /// The route to the endpoint without the base URL
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// A useable route with parameters filled in from XML comment example values 
        /// </summary>
        public string ExampleRoute { get; set; }

        /// <summary>
        /// The HTTP verb the endpoint responds to
        /// </summary>
        public Verbs Verb { get; set; }

        /// <summary>
        /// The type of the body payload the endpoint accepts, if there is one
        /// </summary>
        public Type BodyPayloadType { get; set; }

        /// <summary>
        /// A dictionary with the response code as the key and the object Type returned as the value
        /// </summary>
        public IDictionary<HttpStatusCode, Type> ResponseTypes { get; set; }

        /// <summary>
        /// The name of the method that handles the endpoint
        /// </summary>
        public string EndpointMethodName { get; set; }

        /// <summary>
        /// The return type of the endpoint method
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// The class type of the controller that contains the endpoint
        /// </summary>
        public Type ControllerType { get; set; }

        /// <summary>
        /// The assembly that contains the endpoint method
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// Reflection info for the parameters that the endpoint method accepts
        /// </summary>
        public ParameterInfo[] Parameters { get; set; }

        /// <summary>
        /// Reflection info for the method that implements the endpoint 
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
    }
}
