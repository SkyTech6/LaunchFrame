using System.Diagnostics;

namespace LaunchFrame.Core;

public interface IProcessHelper
{
    Task<Process?> StartProcessAsync(string pathOrUri, string? arguments = null, bool useShellExecute = true, CancellationToken cancellationToken = default);
    bool IsProcessRunning(string processName);
    bool HasMainWindow(string processName);
}
