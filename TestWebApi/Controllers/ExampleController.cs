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
    }
}