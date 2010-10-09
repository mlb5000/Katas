using System.IO;

namespace AutoBuild.Core
{
    public class DirectoryWatcher : IDirectoryWatcher
    {
        private readonly ExecutionArguments executionArguments;
        private readonly FileSystemWatcher fsw;
        private readonly IFileChangedFilter fileChangedFilter;
        private readonly IConsole console;

        public event FileSystemEventHandler FileChanged = delegate { };

        public DirectoryWatcher(ExecutionArguments executionArguments, FileSystemWatcher fileSystemWatcher,
                                IFileChangedFilter fileChangedFilter, IConsole console)
        {
            this.executionArguments = executionArguments;
            fsw = fileSystemWatcher;
            this.fileChangedFilter = fileChangedFilter;
            this.console = console;

            RegisterEvents();
            Initialise();
        }

        private void RegisterEvents()
        {
            fsw.Created += OnFileChanged;
            fsw.Changed += OnFileChanged;
            fsw.Deleted += OnFileChanged;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (fileChangedFilter.IsMatch(e.FullPath))
            {
                FileChanged(this, e);
            }
        }

        private void Initialise()
        {
            fsw.IncludeSubdirectories = true;
            fsw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
        }

        public void Watch()
        {
            fsw.Path = Path.GetFullPath(executionArguments.WatchPath);
            Enable();

            console.Info("Watching path {0}".With(fsw.Path));
        }

        public void Disable()
        {
            fsw.EnableRaisingEvents = false;
        }

        public void Enable()
        {
            fsw.EnableRaisingEvents = true;
        }
    }
}