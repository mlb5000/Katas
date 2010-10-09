namespace AutoBuild.Core.BuildNotification
{
    public class ConsoleBuildNotifier : IBuildNotifier
    {
        private readonly IConsole console;
        private readonly IMessageLogger messageLogger;

        public ConsoleBuildNotifier(IConsole console, IMessageLogger messageLogger)
        {
            this.console = console;
            this.messageLogger = messageLogger;
        }

        public void BuildStarted()
        {
            messageLogger.ClearLog();
            console.Info("Build started");
        }

        public void MessageLogged(string message)
        {
            messageLogger.LogMessage(message);
        }

        public void Error(string errorMessage)
        {
            console.Error(errorMessage);
        }

        public void BuildFinished()
        {
            console.Info("Build succeeded");
        }
    }
}