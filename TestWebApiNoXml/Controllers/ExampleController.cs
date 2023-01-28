using Microsoft.AspNetCore.Mvc;

namespace TestWebApiNoXml.Controllers
{
    /// <summary>
    /// An example controller with example endpoints
    /// </summary>
    [ApiController]
    [Route("/api/v1")]
    public class ExampleController : ControllerBase
    {
        /// <summary>
        /// An example of a Get endpoint
        /// </summary>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [HttpGet("example/{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        /// <summary>
        /// An example of a Delete endpoint
        /// </summary>
        /// <returns>An <see cref="ExampleResponse"/></returns>
        [HttpDelete("example/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}