using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Events;

namespace Simplic.OxS.Logging.Extensions;

public static class LoggingExtensions
{
    public static void Trace(this ILogger logger, string message)
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (CallerScope())
        {
            logger.LogTrace(eventId, message);
        }
    }

    public static void Debug(this ILogger logger, string message)
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (CallerScope())
        {
            logger.LogDebug(eventId, message);
        }
    }

    public static void Info(this ILogger logger, string message)
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (CallerScope())
        {
            logger.LogInformation(eventId, message);
        }
    }

    public static void Warn(this ILogger logger, string message)
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (CallerScope())
        {
            logger.LogWarning(eventId, message);
        }
    }

    public static void Error(this ILogger logger, Exception e, string message = "")
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (CallerScope())
        {
            logger.LogError(eventId, e, message);
        }
    }

    public static void Crit(
        this ILogger logger,
        Exception e,
        string message = "",
        [CallerMemberName] string caller = ""
    )
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (CallerScope())
        {
            logger.LogCritical(eventId, e, message);
        }
    }

    public static LogEventLevel ToLogEventLevel(this LogLevel logLevel) =>
        logLevel switch
        {
            LogLevel.None => LogEventLevel.Verbose,
            LogLevel.Trace => LogEventLevel.Verbose,
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Information => LogEventLevel.Information,
            LogLevel.Warning => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            LogLevel.Critical => LogEventLevel.Fatal,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };

    // private static IDisposable FunctionScope(string caller)
    // {
    //     try
    //     {
    //         const string fnColor = "\u001b[33m";
    //         return LogContext.PushProperty("Fn", $".{fnColor}{caller}");
    //     }
    //     catch (Exception)
    //     {
    //         return LogContext.PushProperty("Fn", "");
    //     }
    // }

    private static IDisposable CallerScope()
    {
        try
        {
            var callerMethod = new StackTrace().GetFrame(2)!.GetMethod();
            var callerType = callerMethod!.ReflectedType!.Name;
            var callerFn = callerMethod.Name;

            const string fnColor = "\u001b[33m";
            const string classColor = "\u001b[38;2;30;216;184m";
            const string noColor = "\u001b[0m";

            return LogContext.PushProperty(
                "Caller",
                $"{classColor}{callerType}{noColor}.{fnColor}{callerFn}"
            );
        }
        catch (Exception)
        {
            return LogContext.PushProperty("Caller", "");
        }
    }
}
