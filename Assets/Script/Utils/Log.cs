using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Utils
{
    public static class Log
    {
        private static readonly string logFilePath = Path.Combine(Application.persistentDataPath, "arena.log.txt");

        // Thread-safe queue pour stocker les logs
        private static readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

        // Semaphore pour contrôler l’écriture
        private static readonly SemaphoreSlim logSemaphore = new SemaphoreSlim(1, 1);

        private static bool isWriting = false;

        public static void Info(string message,
            [CallerFilePath] string file = "",
            [CallerMemberName] string method = "")
        {
            EnqueueLog("info", message, file, method);
        }

        public static void Warn(string message,
            [CallerFilePath] string file = "",
            [CallerMemberName] string method = "")
        {
            EnqueueLog("warning", message, file, method);
        }

        public static void Failure(string message,
            [CallerFilePath] string file = "",
            [CallerMemberName] string method = "")
        {
            EnqueueLog("failure", message, file, method);
        }

        private static void EnqueueLog(string level, string message, string file, string method)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string fileName = Path.GetFileNameWithoutExtension(file);
            string formatted = $"[{timestamp}][{fileName}][{method}][{level}] {message}";

            // Log console Unity
            switch (level)
            {
                case "failure":
                    Debug.LogWarning(formatted);
                    break;
                case "warning":
                    Debug.LogWarning(formatted);
                    break;
                default:
                    Debug.Log(formatted);
                    break;
            }

            // Ajouter dans la queue
            logQueue.Enqueue(formatted);

            // Démarrer la tâche d’écriture si elle n’est pas déjà lancée
            if (!isWriting)
            {
                Task.Run(() => ProcessLogQueue());
            }
        }

        private static async Task ProcessLogQueue()
        {
            isWriting = true;

            while (logQueue.TryDequeue(out var logEntry))
            {
                await logSemaphore.WaitAsync();
                try
                {
                    var directory = Path.GetDirectoryName(logFilePath);
                    if (!Directory.Exists(directory))
                    {
                        Debug.LogWarning($"Log directory does not exist: {directory}");
                        Directory.CreateDirectory(directory);
                        Info("Log directory created.", nameof(Log), nameof(ProcessLogQueue));
                    }
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[Logger][WriteError] {ex.Message}");
                }
                finally
                {
                    logSemaphore.Release();
                }
            }

            isWriting = false;
        }
    }
}
