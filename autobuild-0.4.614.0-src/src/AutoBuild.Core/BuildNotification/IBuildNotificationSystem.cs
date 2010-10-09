namespace AutoBuild.Core.BuildNotification
{
    public interface IBuildNotificationSystem
    {
        void BuildStarted();
        void MessageLogged(string message);
        void Error(string errorMessage);
        void BuildFinished();
    }
}