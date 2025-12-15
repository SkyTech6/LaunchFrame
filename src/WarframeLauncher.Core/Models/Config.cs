using System.Text.Json.Serialization;

namespace LaunchFrame.Core.Models;

public sealed class LauncherConfig
{
    [JsonPropertyName("applications")]
    public List<ApplicationEntry> Applications { get; set; } = new();

    [JsonPropertyName("urls")]
    public List<UrlEntry> Urls { get; set; } = new();

    [JsonPropertyName("skipIfAlreadyRunning")]
    public bool SkipIfAlreadyRunning { get; set; } = true;

    [JsonPropertyName("waitForHelpersBeforeWarframe")]
    public bool WaitForHelpersBeforeWarframe { get; set; } = true;
}

public sealed class ApplicationEntry
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("executablePath")]
    public string? ExecutablePath { get; set; }
        = null;

    [JsonPropertyName("processName")]
    public string? ProcessName { get; set; }
        = null;

    [JsonPropertyName("arguments")]
    public string? Arguments { get; set; }
        = null;

    [JsonPropertyName("readyProcessNames")]
    public List<string> ReadyProcessNames { get; set; } = new();

    [JsonPropertyName("readyWindowSubstring")]
    public string? ReadyWindowSubstring { get; set; }
        = null;

    [JsonPropertyName("launchUri")]
    public string? LaunchUri { get; set; }
        = null;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    [JsonPropertyName("waitForReady")]
    public bool WaitForReady { get; set; } = true;
}

public sealed class UrlEntry
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;
}
