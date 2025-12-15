using System.Diagnostics;

namespace LaunchFrame.Core;

public sealed class ProcessHelper : IProcessHelper
{
    public async Task<Process?> StartProcessAsync(string pathOrUri, string? arguments = null, bool useShellExecute = true, CancellationToken cancellationToken = default)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = pathOrUri,
            UseShellExecute = useShellExecute,
            Arguments = arguments ?? string.Empty
        };

        var process = Process.Start(startInfo);

        // Give the process a brief moment to spin up before further checks.
        await Task.Delay(TimeSpan.FromMilliseconds(200), cancellationToken);
        return process;
    }

    public bool IsProcessRunning(string processName)
    {
        if (string.IsNullOrWhiteSpace(processName))
        {
            return false;
        }

        try
        {
            return Process.GetProcessesByName(processName).Any();
        }
        catch
        {
            return false;
        }
    }

    public bool HasMainWindow(string processName)
    {
        if (string.IsNullOrWhiteSpace(processName))
        {
            return false;
        }

        try
        {
            return Process.GetProcessesByName(processName)
                          .Any(p => p.MainWindowHandle != IntPtr.Zero);
        }
        catch
        {
            return false;
        }
    }
}
