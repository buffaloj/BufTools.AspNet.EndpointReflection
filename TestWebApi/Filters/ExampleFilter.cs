using Microsoft.AspNetCore.Mvc;

namespace Reflectamundo.TestWebApi.Filters
{
    /// <summary>
    /// An example of using an object to store filter parameters for an HTTP request
    /// </summary>
    public class ExampleFilter
    {
        /// <summary>
        /// A dummy string used as an example
        /// </summary>
        /// <example>A filter string!</example>
        [FromQuery(Name = "filter_string")]
        public string? FilterString { get; set; }
    }
}
