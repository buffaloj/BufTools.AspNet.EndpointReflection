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
        /// A useable route with parameters filled in from XML comment example values. This will be empty if XML comment examples do not exist
        /// </summary>
        public string ExampleRoute { get; set; }

        /// <summary>
        /// To construct an <see cref="ExampleRoute"/> from XML comments, there must be XML comments.  This collection lists the required XML comment data 
        /// </summary>
        /// <remarks>This error collection only deals with errors that must be dealt with to use the example route</remarks>
        public IList<Exception> XmlRouteValidationErrors { get; set; }

        /// <summary>
        /// If XML comments are missing on an endpoint, this will list them.
        /// </summary>
        /// <remarks>This warning collection lists all missing XML data on the endpoint</remarks>
        public IList<Exception> AllXmlValidationErrors { get; set; }

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
        public string MethodName { get; set; }

        /// <summary>
        /// A collection of the params the endpoint method accepts
        /// </summary>
        public IEnumerable<EndpointParam> MethodParams { get; set; }

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
        /// The text from the XML 'summary'  field
        /// </summary>
        public string XmlSummary { get; set; }

        /// <summary>
        /// The text from the XML 'returns'  field
        /// </summary>
        public string XmlReturns { get; set; }

        /// <summary>
        /// A collection of 'remarks' from the XML comments
        /// </summary>
        public IEnumerable<string> XmlRemarks { get; set; }

        /// <summary>
        /// A collection of exceptions thrown by the endpoint listed in the XML comments
        /// </summary>
        public IEnumerable<EndpointException> XmlExceptions { get; set; }
    }
}
