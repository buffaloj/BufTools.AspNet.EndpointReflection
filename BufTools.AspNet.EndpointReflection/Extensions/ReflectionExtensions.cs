using System;
using System.IO;
using System.Reflection;

namespace BufTools.AspNet.EndpointReflection.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the path to the location of the assembly on disk
        /// </summary>
        /// <param name="assembly">The assembly to get a path to</param>
        /// <returns>A string containing the path to the assembly</returns>
        public static string GetDirectoryPath(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
