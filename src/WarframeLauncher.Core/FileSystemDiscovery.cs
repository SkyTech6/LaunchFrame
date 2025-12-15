using LaunchFrame.Core.Models;

namespace LaunchFrame.Core;

public static class FileSystemDiscovery
{
    private static readonly string[] DefaultSearchRoots =
    {
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
    };

    public static string? FindExecutable(string displayName, string expectedExeName)
    {
        foreach (var root in DefaultSearchRoots.Where(d => !string.IsNullOrWhiteSpace(d)))
        {
            var path = TryFindInRoot(root, displayName, expectedExeName);
            if (!string.IsNullOrWhiteSpace(path))
            {
                return path;
            }
        }

        return null;
    }

    private static string? TryFindInRoot(string root, string displayName, string expectedExeName)
    {
        try
        {
            var exeMatches = Directory.EnumerateFiles(root, expectedExeName, SearchOption.AllDirectories)
                                      .FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(exeMatches))
            {
                return exeMatches;
            }

            // Fallback: search directories that contain the app name and look for any .exe inside.
            var candidateDir = Directory.EnumerateDirectories(root, "*" + displayName + "*", SearchOption.AllDirectories)
                                        .FirstOrDefault();
            if (candidateDir != null)
            {
                var exe = Directory.EnumerateFiles(candidateDir, "*.exe", SearchOption.AllDirectories)
                                   .FirstOrDefault();
                return exe;
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip protected locations silently.
        }
        catch (Exception)
        {
            // Ignore unexpected IO issues.
        }

        return null;
    }

    public static void BackfillDefaults(LauncherConfig config)
    {
        var aleca = config.Applications.FirstOrDefault(a => a.Id.Equals("alecaframe", StringComparison.OrdinalIgnoreCase));
        if (aleca != null && string.IsNullOrWhiteSpace(aleca.ExecutablePath))
        {
            aleca.ExecutablePath = FindExecutable("AlecaFrame", "AlecaFrame.exe");
        }

        var overframe = config.Applications.FirstOrDefault(a => a.Id.Equals("overframe", StringComparison.OrdinalIgnoreCase));
        if (overframe != null && string.IsNullOrWhiteSpace(overframe.ExecutablePath))
        {
            overframe.ExecutablePath = FindExecutable("Overframe", "Overframe.exe");
        }
    }
}
