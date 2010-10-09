using System;
using System.IO;
using System.Reflection;

namespace AutoBuild.Core
{
    public class PathResolver : IPathResolver
    {
        public string Resolve(string path)
        {
            string localPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            return new Uri(Path.Combine(localPath, path)).LocalPath;
        }
    }
}