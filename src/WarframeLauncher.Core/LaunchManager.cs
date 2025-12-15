using System.Diagnostics;
using LaunchFrame.Core.Models;

namespace LaunchFrame.Core;

public sealed class LaunchManager
{
    private readonly IProcessHelper _processHelper;
    private readonly TimeSpan _readyTimeout = TimeSpan.FromSeconds(30);
    private readonly TimeSpan _pollInterval = TimeSpan.FromMilliseconds(500);
    private readonly TimeSpan _stepDelay = TimeSpan.FromSeconds(2);

    public LaunchManager(IProcessHelper processHelper)
    {
        _processHelper = processHelper;
    }

    public async Task LaunchSessionAsync(LauncherConfig config, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(config);

        // 1. Helpers first (everything except warframe id)
        var helpers = config.Applications
            .Where(a => !IsWarframe(a) && a.Enabled)
            .ToList();

        foreach (var app in helpers)
        {
            await LaunchApplicationAsync(app, config.SkipIfAlreadyRunning, cancellationToken);
            if (config.WaitForHelpersBeforeWarframe && app.WaitForReady)
            {
                await WaitForReadinessAsync(app, cancellationToken);
            }

            await Task.Delay(_stepDelay, cancellationToken);
        }

        // 2. URLs
        foreach (var url in config.Urls.Where(u => u.Enabled))
        {
            await _processHelper.StartProcessAsync(url.Url, useShellExecute: true, cancellationToken: cancellationToken);
            await Task.Delay(_stepDelay, cancellationToken);
        }

        // 3. Warframe last
        var warframe = config.Applications.FirstOrDefault(IsWarframe);
        if (warframe != null && warframe.Enabled)
        {
            await LaunchApplicationAsync(warframe, skipIfRunning: false, cancellationToken);
        }
    }

    private async Task LaunchApplicationAsync(ApplicationEntry app, bool skipIfRunning, CancellationToken cancellationToken)
    {
        if (skipIfRunning && !string.IsNullOrWhiteSpace(app.ProcessName) && _processHelper.IsProcessRunning(app.ProcessName))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(app.LaunchUri))
        {
            await _processHelper.StartProcessAsync(app.LaunchUri, useShellExecute: true, cancellationToken: cancellationToken);
            return;
        }

        if (string.IsNullOrWhiteSpace(app.ExecutablePath))
        {
            throw new InvalidOperationException($"Executable path missing for {app.DisplayName}.");
        }

        if (!File.Exists(app.ExecutablePath))
        {
            throw new FileNotFoundException($"Executable not found: {app.ExecutablePath}");
        }

        await _processHelper.StartProcessAsync(app.ExecutablePath, app.Arguments, useShellExecute: true, cancellationToken: cancellationToken);
    }

    private async Task WaitForReadinessAsync(ApplicationEntry app, CancellationToken cancellationToken)
    {
        var start = Stopwatch.StartNew();
        while (start.Elapsed < _readyTimeout)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (IsAppReady(app))
            {
                return;
            }

            await Task.Delay(_pollInterval, cancellationToken);
        }

        throw new TimeoutException($"Timed out waiting for {app.DisplayName} to become ready.");
    }

    private bool IsAppReady(ApplicationEntry app)
    {
        var processNames = new List<string>();
        if (!string.IsNullOrWhiteSpace(app.ProcessName))
        {
            processNames.Add(app.ProcessName);
        }
        if (app.ReadyProcessNames.Count > 0)
        {
            processNames.AddRange(app.ReadyProcessNames.Where(n => !string.IsNullOrWhiteSpace(n)));
        }

        // 1) Window title substring match (most reliable for UI-ready apps)
        if (!string.IsNullOrWhiteSpace(app.ReadyWindowSubstring))
        {
            try
            {
                var substring = app.ReadyWindowSubstring;
                if (Process.GetProcesses()
                    .Any(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle)
                               && p.MainWindowTitle.Contains(substring, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }
            catch
            {
                // Ignore and continue to other heuristics.
            }
        }

        // 2) Process with a main window handle
        foreach (var name in processNames)
        {
            if (_processHelper.IsProcessRunning(name) && _processHelper.HasMainWindow(name))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsWarframe(ApplicationEntry entry)
    {
        return entry.Id.Equals("warframe", StringComparison.OrdinalIgnoreCase);
    }
}
