namespace AutoBuild.Core
{
    public interface IMessageLogger
    {
        string[] LoggedMessages { get; }
        void ClearLog();
        void LogMessage(string message);
    }
}