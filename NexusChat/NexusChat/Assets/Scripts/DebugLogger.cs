using System.IO;
using UnityEngine;

public static class DebugLogger
{
    private static string logFilePath;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        logFilePath = Path.Combine(Application.persistentDataPath, "nexuschat_log.txt");
        try
        {
            File.WriteAllText(logFilePath, "NexusChat log start: " + System.DateTime.Now.ToString("o") + "\n");
        }
        catch { }
        Application.logMessageReceived += HandleLog;
        Debug.Log("DebugLogger inicializado. Log file: " + logFilePath);
    }

    private static void HandleLog(string logString, string stackTrace, LogType type)
    {
        try
        {
            File.AppendAllText(logFilePath, System.DateTime.Now.ToString("o") + " [" + type + "] " + logString + "\n");
            if (!string.IsNullOrEmpty(stackTrace))
                File.AppendAllText(logFilePath, stackTrace + "\n");
        }
        catch { }
    }

    public static void Log(string message)
    {
        Debug.Log(message);
    }

    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    public static void LogError(string message)
    {
        Debug.LogError(message);
    }
}
