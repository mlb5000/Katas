namespace AutoBuild.Core
{
    public interface IFileChangedFilter
    {
        bool IsMatch(string filePath);
    }
}