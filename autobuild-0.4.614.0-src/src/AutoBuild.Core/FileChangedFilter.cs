using System.IO;

namespace AutoBuild.Core
{
    public class FileChangedFilter : IFileChangedFilter
    {
        private const string fileFilter = "(.cs)$|(.csproj)$|(.vb)$|(.vbproj)$";

        public bool IsMatch(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return (extension.Matches(fileFilter));
        }
    }
}