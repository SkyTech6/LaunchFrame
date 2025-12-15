using LaunchFrame.Core.Models;

namespace LaunchFrame.Core;

public static class ConfigDefaults
{
    public const string DefaultWarframeUri = "steam://rungameid/230410";

    public static LauncherConfig CreateDefault()
    {
        return new LauncherConfig
        {
            SkipIfAlreadyRunning = true,
            WaitForHelpersBeforeWarframe = true,
            Applications =
            {
                new ApplicationEntry
                {
                    Id = "alecaframe",
                    DisplayName = "AlecaFrame",
                    ExecutablePath = @"G:\\Games\\AlecaFrame\\Overwolf\\OverwolfLauncher.exe",
                    Arguments = "-launchapp afmcagbpgggkpdkokjhjkllpegnadmkignlonpjm -from-startmenu",
                    ProcessName = "AlecaFrame",
                    ReadyProcessNames = { "AlecaFrame", "OverwolfBrowser", "OverwolfLauncher" },
                    ReadyWindowSubstring = "AlecaFrame",
                    Enabled = true,
                    WaitForReady = true
                },
                new ApplicationEntry
                {
                    Id = "overframe",
                    DisplayName = "Overframe",
                    ProcessName = "Overframe",
                    ReadyWindowSubstring = "Overframe",
                    Enabled = true,
                    WaitForReady = true
                },
                new ApplicationEntry
                {
                    Id = "warframe",
                    DisplayName = "Warframe",
                    LaunchUri = DefaultWarframeUri,
                    Enabled = true,
                    WaitForReady = false
                }
            },
            Urls =
            {
                new UrlEntry { Url = "https://warframe.market", Enabled = false },
                new UrlEntry { Url = "https://wiki.warframe.com", Enabled = false }
            }
        };
    }
}
