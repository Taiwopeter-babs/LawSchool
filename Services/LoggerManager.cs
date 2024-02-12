using LawSchool.Contracts;
using NLog;

namespace LawSchool.Services;

/// <summary>
/// Logger manager for application
/// </summary>
public class LoggerManager : ILoggerManager
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public LoggerManager() { }

    public void LogDebug(string message) => logger.Debug(message);
    public void LogInfo(string message) => logger.Info(message);
    public void LogWarn(string message) => logger.Warn(message);
    public void LogError(string message) => logger.Error(message);
}