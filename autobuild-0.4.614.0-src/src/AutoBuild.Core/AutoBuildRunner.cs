using System;
using System.IO;

namespace AutoBuild.Core
{
    public class AutoBuildRunner
    {
        private readonly IDirectoryWatcher watcher;
        private readonly IBuildRunner buildRunner;
        private readonly IConsole console;

        public AutoBuildRunner(IDirectoryWatcher watcher, IBuildRunner buildRunner, IConsole console)
        {
            this.watcher = watcher;
            this.buildRunner = buildRunner;
            this.console = console;

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            watcher.FileChanged += HandleFileChanged;
            buildRunner.BuildCompleted += HandleBuildCompleted;
        }

        private void HandleFileChanged(object sender, FileSystemEventArgs e)
        {
            console.Debug("File {0} {1}".With(e.ChangeType, e.Name));
            RunBuild();
        }

        private void RunBuild()
        {
            watcher.Disable();
            buildRunner.RunBuild();
        }

        private void HandleBuildCompleted(object sender, EventArgs e)
        {
            watcher.Enable();
        }

        public void Execute()
        {
            watcher.Watch();
        }
    }
}