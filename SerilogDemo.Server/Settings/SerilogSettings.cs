using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Simplic.OxS.Logging.Extensions;

internal class SerilogSettings
{
    internal LoggerConfiguration CreateConfiguration()
    {
        var configuration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext();

        // push via scope
        // .Enrich.WithProperty("Class", $"{classColor}{typeof(T).Name}");
        // .Enrich.WithProperty("Class", "{SourceContext}");

        if (LogToConsole)
        {
            configuration.WriteTo.Console(
                outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u4} {@Class}{Method}] {Message:lj}{NewLine}{Exception}",
                theme: ConsoleTheme,
                restrictedToMinimumLevel: ConsoleLogOptions.LogLevel.ToLogEventLevel()
            );
        }

        if (LogToFile)
        {
            var filePath = $"{FileLogOptions.OutDir}/{FileLogOptions.FileName}.log";

            configuration.WriteTo.File(
                path: filePath,
                rollingInterval: RollingInterval.Day,
                outputTemplate:
                "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u4} {Class}.{Method}] {Message:lj}{NewLine}{Exception}",
                restrictedToMinimumLevel: FileLogOptions.LogLevel.ToLogEventLevel());
        }

        return configuration;
    }

    internal ConsoleTheme ConsoleTheme = new AnsiConsoleTheme(new Dictionary<ConsoleThemeStyle, string>
    {
        [ConsoleThemeStyle.Text] = "\x1b[38;5;28m", // Green
        [ConsoleThemeStyle.SecondaryText] = "\x1b[38;5;245m", // Light grey
        [ConsoleThemeStyle.TertiaryText] = "\x1b[38;5;242m", // Dark grey
        [ConsoleThemeStyle.Invalid] = "\x1b[38;5;160m", // Red
        [ConsoleThemeStyle.Null] = "\x1b[38;5;242m", // Dark grey
        [ConsoleThemeStyle.Name] = "\x1b[38;5;45m", // Blue
        [ConsoleThemeStyle.String] = "\x1b[38;5;110m", // Light green
        [ConsoleThemeStyle.Number] = "\x1b[38;5;167m", // Orange
        [ConsoleThemeStyle.Boolean] = "\x1b[38;5;110m", // Light green
        [ConsoleThemeStyle.Scalar] = "\x1b[38;5;242m", // Dark grey
        [ConsoleThemeStyle.LevelVerbose] = "\x1b[38;5;242m", // Dark grey
        [ConsoleThemeStyle.LevelDebug] = "\x1b[38;5;200m", // Magenta
        [ConsoleThemeStyle.LevelInformation] = "\x1b[38;5;34m", // Green
        [ConsoleThemeStyle.LevelWarning] = "\x1b[38;5;220m", // Yellow
        [ConsoleThemeStyle.LevelError] = "\x1b[38;5;160m", // Red
        [ConsoleThemeStyle.LevelFatal] = "\x1b[48;5;9;38;5;16m", // White on red
        // [ConsoleThemeStyle.Class] = "\x1b[48;5;9;38;5;16m", // White on red
    });

    internal bool LogToConsole { get; set; }
    internal bool LogToFile { get; set; }

    internal ConsoleLogOptions ConsoleLogOptions { get; set; } = new();
    internal FileLogOptions FileLogOptions { get; set; } = new();
}