using System.Text.Json;
using LaunchFrame.Core.Models;

namespace LaunchFrame.Core;

public sealed class ConfigService
{
    private readonly string _configPath;
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public ConfigService(string? customPath = null)
    {
        _configPath = customPath ?? GetDefaultPath();
        var directory = Path.GetDirectoryName(_configPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public static string GetDefaultPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(appData, "LaunchFrame", "config.json");
    }

    public async Task<LauncherConfig> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_configPath))
        {
            return ConfigDefaults.CreateDefault();
        }

        await using var stream = File.OpenRead(_configPath);
        var config = await JsonSerializer.DeserializeAsync<LauncherConfig>(stream, _serializerOptions, cancellationToken);
        return config ?? ConfigDefaults.CreateDefault();
    }

    public async Task SaveAsync(LauncherConfig config, CancellationToken cancellationToken = default)
    {
        await using var stream = File.Create(_configPath);
        await JsonSerializer.SerializeAsync(stream, config, _serializerOptions, cancellationToken);
    }
}
