using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Events;

namespace Simplic.OxS.Logging.Extensions;

public static class LoggingExtensions
{
    public static void Trace(
        this ILogger logger,
        string message,
        [CallerMemberName] string caller = ""
    )
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (ClassScope())
        using (FunctionScope(caller))
        {
            logger.LogTrace(eventId, message);
        }
    }

    public static void Debug(
        this ILogger logger,
        string message,
        [CallerMemberName] string caller = ""
    )
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (ClassScope())
        using (FunctionScope(caller))
        {
            logger.LogDebug(eventId, message);
        }
    }

    public static void Info(
        this ILogger logger,
        string message,
        [CallerMemberName] string caller = ""
    )
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (ClassScope())
        using (FunctionScope(caller))
        {
            logger.LogInformation(eventId, message);
        }
    }

    public static void Warn(
        this ILogger logger,
        string message,
        [CallerMemberName] string caller = ""
    )
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (ClassScope())
        using (FunctionScope(caller))
        {
            logger.LogWarning(eventId, message);
        }
    }

    public static void Error(
        this ILogger logger,
        Exception e,
        string message = "",
        [CallerMemberName] string caller = ""
    )
    {
        var eventId = new EventId(-1, Guid.NewGuid().ToString());
        using (ClassScope())
        using (FunctionScope(caller))
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
        using (ClassScope())
        using (FunctionScope(caller))
        {
            logger.LogCritical(eventId, e, message);
        }
    }

    public static LogEventLevel ToLogEventLevel(this LogLevel logLevel) => logLevel switch
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

    private static IDisposable FunctionScope(string caller)
    {
        return LogContext.PushProperty("Fn", $".\u001b[33m{caller}");
    }

    private static IDisposable ClassScope()
    {
        var stackTrace = new StackTrace();
        var stackFrames = stackTrace.GetFrames();

        var callingFrame = stackFrames[1];
        const string classColor = "\u001b[38;2;30;216;184m";

        return LogContext.PushProperty("Class", $"{classColor}{callingFrame.GetType().Name}");
    }
}
