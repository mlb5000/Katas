namespace AutoBuild.Core.BuildNotification
{
    public interface IBuildNotifier
    {
        void BuildStarted();
        void MessageLogged(string message);
        void Error(string errorMessage);
        void BuildFinished();
    }
}