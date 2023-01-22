namespace Reflectamundo.TestWebApi.Responses
{
    /// <summary>
    /// An example of a resopnse from an HTTP request
    /// </summary>
    public class ExampleResponse
    {
        /// <summary>
        /// This contains the string passed in via the request
        /// </summary>
        public string? ReturnedString { get; set; }

        /// <summary>
        /// This contains the string passed in via the optional query params
        /// </summary>
        public string? FilterString { get; set; }
    }
}