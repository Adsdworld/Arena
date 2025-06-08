using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Script.Utils
{
    public static class Log
    {
        // ignore warning : Name 'logFilePath' does not match rule 'Static readonly fields (private)'. Suggested name is 'LOGFilePath'.
        private static string logFilePath = null;

        // Thread-safe queue pour stocker les logs
        // ignore warning : Name 'logQueue' does not match rule 'Static readonly fields (private)'. Suggested name is 'LOGQueue'.
        private static readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

        // Semaphore pour contrôler l’écriture
        // ignore warning : Name 'logSemaphore' does not match rule 'Static readonly fields (private)'. Suggested name is 'LOGSemaphore'.
        private static readonly SemaphoreSlim logSemaphore = new SemaphoreSlim(1, 1);

        private static bool _isWriting;

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
        
        public static void Error(string message,
            [CallerFilePath] string file = "",
            [CallerMemberName] string method = "")
        {
            EnqueueLog("error", message, file, method);
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
            if (!_isWriting)
            {
                Task.Run(() => ProcessLogQueue());
            }
        }

        private static async Task ProcessLogQueue()
        {
            _isWriting = true;

            while (logQueue.TryDequeue(out var logEntry))
            {
                await logSemaphore.WaitAsync();
                try
                {
                    if (logFilePath == null)
                    {
                        var tcs = new TaskCompletionSource<string>();

                        MainThreadDispatcher.Enqueue(() =>
                        {
                            tcs.SetResult(Application.persistentDataPath);
                        });

                        logFilePath = Path.Combine(await tcs.Task, "arena.log.txt");
                    }
                    
                    var directory = Path.GetDirectoryName(logFilePath);
                    if (!Directory.Exists(directory) && directory != null)
                    {
                        Debug.LogWarning($"Log directory does not exist: {directory}");
                        Directory.CreateDirectory(directory);
                        Info("Log directory created.");
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

            _isWriting = false;
        }
    }
}
