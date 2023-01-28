using System;

namespace BufTools.AspNet.EndpointReflection
{
    public class EndpointParam
    {
        public Type ParamType { get; set; }
        public string XMLDescription { get; set; }
        public string XmlExample { get; set; }
        public ParamUsageTypes UseageType { get; set; }
    }
}
