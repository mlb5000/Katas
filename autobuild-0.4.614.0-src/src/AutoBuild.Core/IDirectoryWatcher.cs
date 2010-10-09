using System.IO;

namespace AutoBuild.Core
{
    public interface IDirectoryWatcher
    {
        void Watch();
        event FileSystemEventHandler FileChanged;
        void Disable();
        void Enable();
    }
}