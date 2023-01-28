using System;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Exceptions
{
    public class MissingXMLFileForAssembly : Exception
    {
        public MissingXMLFileForAssembly(Assembly assembly)
            : base($"XML file not found on disk for assembly '{assembly}'. \n\n" + 
                   $"To fix: add these tags to the <PropertyGroup> section of the projectfile:\n" +
                    "<GenerateDocumentationFile>true</GenerateDocumentationFile>\n" +
                    "<DocumentationFile />")
        {
        }
    }
}
