using System;

namespace AutoBuild.Core.BuildNotification
{
    public class BuildNotificationSystem : IBuildNotificationSystem
    {
        private readonly IBuildNotifier[] buildNotifiers;

        public BuildNotificationSystem(IBuildNotifier[] buildNotifiers)
        {
            this.buildNotifiers = buildNotifiers;
        }

        private void ForEachNotifier(Action<IBuildNotifier> action)
        {
            foreach (IBuildNotifier buildNotifier in buildNotifiers)
            {
                action(buildNotifier);
            }
        }


        public void BuildStarted()
        {
            ForEachNotifier(n => n.BuildStarted());
        }

        public void MessageLogged(string message)
        {
            ForEachNotifier(n => n.MessageLogged(message));
        }

        public void Error(string errorMessage)
        {
            ForEachNotifier(n => n.Error(errorMessage));
        }

        public void BuildFinished()
        {
            ForEachNotifier(n => n.BuildFinished());
        }
    }
}