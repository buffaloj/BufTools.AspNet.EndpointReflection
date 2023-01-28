using Microsoft.AspNetCore.Mvc;
using Reflectamundo.TestWebApi.Filters;
using Reflectamundo.TestWebApi.Requests;
using Reflectamundo.TestWebApi.Responses;

namespace Reflectamundo.TestWebApi.Controllers
{
    /// <summary>
    /// An example controller with example endpoints
    /// </summary>
    [ApiController]
    [Route("/api/v1")]
    public class ExampleController : ControllerBase
    {
        /// <summary>
        /// An example of a Get endpoint that will return the filter string
        /// </summary>
        /// <param name="filter">An object used to filter the request</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("example")]
        public IActionResult Get([FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(null, filter);
            return Ok(response);
        }

        /// <summary>
        /// An example of a Get endpoint that returns a resource by id
        /// </summary>
        /// <param name="id" example="19">The id of the requested resource</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("example/{id}")]
        public IActionResult Get(int id)
        {
            var response = new ExampleResponse { ReturnedId = id };
            return Ok(response);
        }

        /// <summary>
        /// An example of a Post endpoint that will return request and filter params in a response
        /// </summary>
        /// <param name="request">A request object</param>
        /// <param name="filter">The object used to filter the request</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost("example")]
        public IActionResult Post(ExampleRequest request, [FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(request, filter);
            return Ok(response);
        }

        /// <summary>
        /// An example of a Put endpoint that will return requets and filter params in a response
        /// </summary>
        /// <param name="request">A request object</param>
        /// <param name="filter">The object used to filter the request</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPut("example")]
        public IActionResult Put(ExampleRequest request, [FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(request, filter);
            return Ok(response);
        }

        /// <summary>
        /// An example of a Delete endpoint that will return requets and filter params in a response
        /// </summary>
        /// <param name="request">A request object</param>
        /// <param name="filter">The object used to filter the request</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpDelete("example")]
        public IActionResult Delete(ExampleRequest request, [FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(request, filter);
            return Ok(response);
        }

        /// <summary>
        /// An example of a Patch endpoint that will return requets and filter params in a response
        /// </summary>
        /// <param name="request">A request object</param>
        /// <param name="filter">The object used to filter the request</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPatch("example")]
        public IActionResult Patch(ExampleRequest request, [FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(request, filter);
            return Ok(response);
        }

        /// <summary>
        /// An example of an Options endpoint that will return requets and filter params in a response
        /// </summary>
        /// <param name="request">A request object</param>
        /// <param name="filter">The object used to filter the request</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpOptions("example")]
        public IActionResult Options(ExampleRequest request, [FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(request, filter);
            return Ok(response);
        }

        /// <summary>
        /// An example of an Head endpoint that will return requets and filter params in a response
        /// </summary>
        /// <param name="request">A request object</param>
        /// <param name="filter">The object used to filter the request</param>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpOptions("example")]
        public IActionResult Head(ExampleRequest request, [FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(request, filter);
            return Ok(response);
        }

        private ExampleResponse GetResponse(ExampleRequest? request, [FromQuery] ExampleFilter? filter)
        {
            return new ExampleResponse
            {
                ReturnedString = request?.StringToReturn,
                FilterString = filter?.FilterString
            };
        }

        /// <summary>
        /// A method that is public, but isn't an endpoint
        /// </summary>
        public void APublicNonEndpointMethod()
        {
        }
        
        [HttpHead("example/no_xmldocs/{id}")]
        public IActionResult EndpointWithNoXMLDocs(int id)
        {
            var response = new ExampleResponse { ReturnedId = id };
            return Ok(response);
        }
        
        /// <summary>
        /// An endpoint with no XML for params
        /// </summary>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_param_docs")]
        public IActionResult EndpointWithNoParamDocs(ExampleRequest request, [FromQuery] ExampleFilter filter)
        {
            var response = GetResponse(request, filter);
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no XML example for param
        /// </summary>
        /// <param name="id">A request object</param>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_param_example/{id}")]
        public IActionResult EndpointWithNoParamExample(int id)
        {
            var response = new ExampleResponse { ReturnedId = id };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no XML for param
        /// </summary>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_param_xml/{id}")]
        public IActionResult EndpointWithNoParamXML(int id)
        {
            var response = new ExampleResponse { ReturnedId = id };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with missing route param
        /// </summary>
        /// <param name="id" example="5">A request object</param>
        /// <returns>A message!</returns>
        [HttpOptions("example/missing_route_param/{id}")]
        public IActionResult EndpointWithNoRouteParam()
        {
            var response = new ExampleResponse { ReturnedId = 5 };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no returns XML
        /// </summary>
        /// <param name="id">A request object</param>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_returns_xml/{id}")]
        public IActionResult EndpointWithNoReturnsXml(int id)
        {
            var response = new ExampleResponse { ReturnedId = id };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no param description
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_param_description/{id}")]
        public IActionResult EndpointWithNoParamDescription(int id)
        {
            var response = new ExampleResponse { ReturnedId = id };
            return Ok(response);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_summary/{id}")]
        public IActionResult EndpointWithNoSummary(int id)
        {
            var response = new ExampleResponse { ReturnedId = id };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no description on the exception
        /// </summary>
        /// <exception cref="ExampleRequest"></exception>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_exception_description")]
        public IActionResult EndpointWithNoExceptionDescription()
        {
            var response = new ExampleResponse { ReturnedId = 5 };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no description on the exception
        /// </summary>
        /// <exception cref="">Description</exception>
        /// <returns>A message!</returns>
        [HttpOptions("example/no_exception_description")]
        public IActionResult EndpointWithNoExceptionType()
        {
            var response = new ExampleResponse { ReturnedId = 5 };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no description on the exception
        /// </summary>
        [HttpOptions("example/no_returns_field")]
        public IActionResult EndpointWithNoReturnsField()
        {
            var response = new ExampleResponse { ReturnedId = 5 };
            return Ok(response);
        }

        /// <summary>
        /// An endpoint with no description on the exception
        /// </summary>
        /// <returns></returns>
        [HttpOptions("example/has_returns_but_no_description")]
        public IActionResult EndpointWithNoReturnsDescription()
        {
            var response = new ExampleResponse { ReturnedId = 5 };
            return Ok(response);
        }
    }
}