using Microsoft.Extensions.Logging;

namespace Simplic.OxS.Logging;

public class Logger<T> : ILogger<T>
{
    private readonly LogLevel minimumLogLevel;

    public Logger(LogLevel minimumLogLevel)
    {
        this.minimumLogLevel = minimumLogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel >= minimumLogLevel;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);

    }
}
