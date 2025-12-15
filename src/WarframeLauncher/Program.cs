using LaunchFrame.Core;
using LaunchFrame.UI;

namespace LaunchFrame;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        if (args.Any(a => a.Equals("--oneclick", StringComparison.OrdinalIgnoreCase) ||
                          a.Equals("/oneclick", StringComparison.OrdinalIgnoreCase)))
        {
            RunOneClick();
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }

    private static void RunOneClick()
    {
        try
        {
            var configService = new ConfigService();
            var config = configService.LoadAsync().GetAwaiter().GetResult();
            FileSystemDiscovery.BackfillDefaults(config);
            configService.SaveAsync(config).GetAwaiter().GetResult();

            var launchManager = new LaunchManager(new ProcessHelper());
            launchManager.LaunchSessionAsync(config).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "LaunchFrame one-click failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}