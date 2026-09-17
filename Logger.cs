using BepInEx.Logging;

namespace CosmoDreadCoop;

internal static class CosmoLogger
{
	private static ManualLogSource logSource;

	public static void SetSource(ManualLogSource logger)
	{
		logSource = logger;
	}

    public static void LogInfo(object message)
    {
        logSource.LogInfo(message);
    }

    public static void LogWarning(object message)
    {
        logSource.LogWarning(message);
    }

    public static void LogError(object message)
    {
        logSource.LogError(message);
    }

    public static void LogFatal(object message)
    {
        logSource.LogFatal(message);
    }

    public static void LogDebug(object message)
    {
        logSource.LogDebug(message);
    }
}