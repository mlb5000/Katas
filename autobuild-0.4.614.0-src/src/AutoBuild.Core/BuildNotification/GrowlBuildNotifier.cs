using Growl.Connector;

namespace AutoBuild.Core.BuildNotification
{
    public class GrowlBuildNotifier : IBuildNotifier
    {
        private const string ApplicationName = "AutoBuild";
        private const string BuildStartedNotificationName = "BUILD_STARTED";
        private const string BuildFailedNotificationName = "BUILD_FAILED";
        private const string BuildSucceededNotificationName = "BUILD_SUCCEEDED";

        private readonly GrowlConnector growlConnector;
        private readonly Application application;
        private readonly NotificationType buildStartedNotification;
        private readonly NotificationType buildFailedNotification;
        private readonly NotificationType buildSucceededNotification;

        public GrowlBuildNotifier()
        {
            growlConnector = new GrowlConnector
                                 {
                                     EncryptionAlgorithm = Cryptography.SymmetricAlgorithmType.PlainText
                                 };

            application = new Application(ApplicationName);

            buildStartedNotification = new NotificationType(BuildStartedNotificationName, "Build started");
            buildFailedNotification = new NotificationType(BuildFailedNotificationName, "Build failed");
            buildSucceededNotification = new NotificationType(BuildSucceededNotificationName, "Build succeeded");

            growlConnector.Register(application, new[]
                                                     {
                                                         buildStartedNotification,
                                                         buildFailedNotification, buildSucceededNotification
                                                     });
        }

        public void BuildStarted()
        {
            SendNotification(BuildStartedNotificationName, "Build started");
        }

        private void SendNotification(string notificationName, string message)
        {
            var notification = new Notification(ApplicationName, notificationName, null,
                                                "Auto Build", message);
            growlConnector.Notify(notification);
        }

        public void MessageLogged(string message)
        {
        }

        public void Error(string errorMessage)
        {
            SendNotification(BuildFailedNotificationName, "Build failed");
        }

        public void BuildFinished()
        {
            SendNotification(BuildSucceededNotificationName, "Build succeeded");
        }
    }
}