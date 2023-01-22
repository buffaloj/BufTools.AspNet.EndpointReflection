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
        /// The HTTP verb the endpoint acts on
        /// </summary>
        public Verbs Verb { get; set; }

        /// <summary>
        /// Reflection info for the parameters that the endpoint method accepts
        /// </summary>
        public ParameterInfo[] Parameters { get; set; }

        /// <summary>
        /// Reflection info for the method that implements the endpoint 
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// The type the method that handles the endpoint returns
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// The name of the controller class that contains the endpoint
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// The assembly that contains the endpoint method
        /// </summary>
        public Assembly Assembly { get; set; }
    }
}
