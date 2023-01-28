using System.Collections.Generic;

namespace BufTools.AspNet.EndpointReflection.Models
{
    public class MemberDoc
    {
        public string Summary { get; set; }
        public string Returns { get; set; }
        public string Example { get; set; }
        public IList<ParamDoc> Params { get; } = new List<ParamDoc>();
        public IList<ExceptionDoc> Exceptions { get; } = new List<ExceptionDoc>();
        public IList<string> Remarks { get; } = new List<string>();
    }
}
