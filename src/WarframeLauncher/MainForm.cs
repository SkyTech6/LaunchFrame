using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using LaunchFrame.Core;
using LaunchFrame.Core.Models;

namespace LaunchFrame.UI;

public partial class MainForm : Form
{
    private readonly ConfigService _configService;
    private readonly LaunchManager _launchManager;
    private LauncherConfig _config = ConfigDefaults.CreateDefault();
    private readonly CancellationTokenSource _cts = new();

    public MainForm()
    {
        InitializeComponent();
        _configService = new ConfigService();
        _launchManager = new LaunchManager(new ProcessHelper());
        Load += async (_, _) => await LoadConfigAsync();
    }

    private async Task LoadConfigAsync()
    {
        try
        {
            _config = await _configService.LoadAsync(_cts.Token);
            FileSystemDiscovery.BackfillDefaults(_config);
            BindConfigToUi(_config);
            await _configService.SaveAsync(_config, _cts.Token);
            SetStatus("Configuration loaded.");
        }
        catch (Exception ex)
        {
            SetStatus($"Failed to load config: {ex.Message}");
        }
    }

    private void BindConfigToUi(LauncherConfig config)
    {
        var aleca = config.Applications.FirstOrDefault(a => a.Id == "alecaframe");
        var over = config.Applications.FirstOrDefault(a => a.Id == "overframe");
        var warframe = config.Applications.FirstOrDefault(a => a.Id == "warframe");

        if (aleca != null)
        {
            chkAlecaframe.Checked = aleca.Enabled;
            txtAlecaframePath.Text = aleca.ExecutablePath ?? string.Empty;
            txtAlecaframeArgs.Text = aleca.Arguments ?? string.Empty;
        }

        if (over != null)
        {
            chkOverframe.Checked = over.Enabled;
            txtOverframePath.Text = over.ExecutablePath ?? string.Empty;
            txtOverframeArgs.Text = over.Arguments ?? string.Empty;
        }

        if (warframe != null)
        {
            chkWarframe.Checked = warframe.Enabled;
            txtWarframeUri.Text = warframe.LaunchUri ?? ConfigDefaults.DefaultWarframeUri;
        }

        chkWaitForHelpers.Checked = config.WaitForHelpersBeforeWarframe;
        chkSkipIfRunning.Checked = config.SkipIfAlreadyRunning;
    }

    private LauncherConfig BuildConfigFromUi()
    {
        var config = _config;

        var aleca = config.Applications.FirstOrDefault(a => a.Id == "alecaframe") ?? new ApplicationEntry { Id = "alecaframe", DisplayName = "AlecaFrame" };
        aleca.Enabled = chkAlecaframe.Checked;
        aleca.ExecutablePath = txtAlecaframePath.Text.Trim();
        aleca.ProcessName = string.IsNullOrWhiteSpace(aleca.ProcessName) ? "AlecaFrame" : aleca.ProcessName;
        aleca.Arguments = txtAlecaframeArgs.Text.Trim();
        if (!config.Applications.Contains(aleca)) config.Applications.Add(aleca);

        var over = config.Applications.FirstOrDefault(a => a.Id == "overframe") ?? new ApplicationEntry { Id = "overframe", DisplayName = "Overframe" };
        over.Enabled = chkOverframe.Checked;
        over.ExecutablePath = txtOverframePath.Text.Trim();
        over.ProcessName = string.IsNullOrWhiteSpace(over.ProcessName) ? "Overframe" : over.ProcessName;
        over.Arguments = txtOverframeArgs.Text.Trim();
        if (!config.Applications.Contains(over)) config.Applications.Add(over);

        var warframe = config.Applications.FirstOrDefault(a => a.Id == "warframe") ?? new ApplicationEntry { Id = "warframe", DisplayName = "Warframe" };
        warframe.Enabled = chkWarframe.Checked;
        warframe.LaunchUri = string.IsNullOrWhiteSpace(txtWarframeUri.Text) ? ConfigDefaults.DefaultWarframeUri : txtWarframeUri.Text.Trim();
        warframe.ProcessName ??= "Warframe";
        warframe.WaitForReady = false;
        if (!config.Applications.Contains(warframe)) config.Applications.Add(warframe);

        config.WaitForHelpersBeforeWarframe = chkWaitForHelpers.Checked;
        config.SkipIfAlreadyRunning = chkSkipIfRunning.Checked;

        return config;
    }

    private async void OnSaveConfig(object? sender, EventArgs e)
    {
        try
        {
            var updated = BuildConfigFromUi();
            await _configService.SaveAsync(updated, _cts.Token);
            SetStatus("Configuration saved.");
        }
        catch (Exception ex)
        {
            SetStatus($"Save failed: {ex.Message}");
        }
    }

    private async void OnLaunchSessionAsync(object? sender, EventArgs e)
    {
        SetBusy(true);
        SetStatus("Launching...");

        try
        {
            var config = BuildConfigFromUi();
            await _configService.SaveAsync(config, _cts.Token);
            await _launchManager.LaunchSessionAsync(config, _cts.Token);
            SetStatus("Launch sequence completed.");
        }
        catch (OperationCanceledException)
        {
            SetStatus("Launch cancelled.");
        }
        catch (Exception ex)
        {
            SetStatus($"Launch failed: {ex.Message}");
            MessageBox.Show(this, ex.Message, "Launch failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void OnBrowseAlecaframe(object? sender, EventArgs e)
    {
        txtAlecaframePath.Text = BrowseForExe(txtAlecaframePath.Text);
    }

    private void OnBrowseOverframe(object? sender, EventArgs e)
    {
        txtOverframePath.Text = BrowseForExe(txtOverframePath.Text);
    }

    private string BrowseForExe(string initial)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Executable (*.exe)|*.exe|All files (*.*)|*.*",
            FileName = initial
        };

        return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : initial;
    }

    private void OnOpenOptions(object? sender, EventArgs e)
    {
        using var dialog = new OptionsForm(_config.Urls);
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _config.Urls = dialog.Urls.Select(u => new UrlEntry { Url = u.Url, Enabled = u.Enabled }).ToList();
            SetStatus("URL options updated.");
        }
    }

    private void SetBusy(bool busy)
    {
        btnLaunch.Enabled = !busy;
        btnSave.Enabled = !busy;
        btnOptions.Enabled = !busy;
        btnCreateOneClick.Enabled = !busy;
        Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
    }

    private void SetStatus(string message)
    {
        lblStatus.Text = message;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _cts.Cancel();
        base.OnClosing(e);
    }

    private async void OnCreateOneClick(object? sender, EventArgs e)
    {
        SetBusy(true);
        try
        {
            var config = BuildConfigFromUi();
            await _configService.SaveAsync(config, _cts.Token);

            using var dialog = new SaveFileDialog
            {
                Filter = "Shortcut (*.lnk)|*.lnk|Batch file (*.cmd)|*.cmd",
                FileName = "LaunchFrame OneClick.lnk",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                SetStatus("Shortcut creation cancelled.");
                return;
            }

            var shortcutPath = dialog.FileName;
            var executable = Application.ExecutablePath;
            var arguments = "--oneclick";

            if (string.Equals(Path.GetExtension(shortcutPath), ".cmd", StringComparison.OrdinalIgnoreCase))
            {
                File.WriteAllLines(shortcutPath, new[]
                {
                    "@echo off",
                    $"\"{executable}\" {arguments}",
                    string.Empty
                });
            }
            else
            {
                CreateWindowsShortcut(shortcutPath, executable, arguments);
            }

            SetStatus($"One-click created: {Path.GetFileName(shortcutPath)}");
        }
        catch (Exception ex)
        {
            SetStatus($"Shortcut creation failed: {ex.Message}");
            MessageBox.Show(this, ex.Message, "Shortcut creation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private static void CreateWindowsShortcut(string shortcutPath, string targetPath, string arguments)
    {
        var shellType = Type.GetTypeFromProgID("WScript.Shell") ?? throw new InvalidOperationException("WScript.Shell is unavailable.");
        var shellInstance = Activator.CreateInstance(shellType) ?? throw new InvalidOperationException("Failed to create WScript.Shell.");
        dynamic shell = shellInstance;
        dynamic shortcut = shell.CreateShortcut(shortcutPath);
        shortcut.TargetPath = targetPath;
        shortcut.Arguments = arguments;
        shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
        shortcut.IconLocation = targetPath;
        shortcut.Save();
    }
}
