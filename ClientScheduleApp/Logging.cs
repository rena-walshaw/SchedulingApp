using System;
using System.IO;

/// <summary>
/// Provides logging functionality for tracking user login activity.
/// </summary>
public static class Logging
{
    // Path to the log file where login history is stored.
    private static string logFilePath = "Login_History.txt";

    /// <summary>
    /// Logs a user's login activity with a timestamp.
    /// </summary>
    /// <param name="username">The username of the user who logged in.</param>
    public static void LogActivity(string username)
    {
        // Create a log entry with the current date and time.
        string logEntry = $"{DateTime.Now}: User {username} logged in.";

        // Append the log entry to the file, adding a new line.
        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
    }
}
