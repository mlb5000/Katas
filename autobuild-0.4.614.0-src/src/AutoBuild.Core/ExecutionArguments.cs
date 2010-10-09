namespace AutoBuild.Core
{
    public class ExecutionArguments
    {
        private readonly string buildFile;
        private readonly string watchPath;

        public ExecutionArguments(string buildFile, string watchPath)
        {
            this.buildFile = buildFile;
            this.watchPath = watchPath;
        }

        public string BuildFile
        {
            get { return buildFile; }
        }

        public string WatchPath
        {
            get { return watchPath; }
        }
    }
}