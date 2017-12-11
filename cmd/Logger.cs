using System;
using System.IO;
using System.Diagnostics;

// TODO: REMOVE IT AFTER RELEASE v.1
namespace cmd
{
    public static class Logger
    {
        private static readonly string _logsDirectory = "./logs";
        private static readonly string _logName;

        static Logger()
        {
            _logName = $"{_logsDirectory}/{GetFormattedDate()}.txt";
            Directory.CreateDirectory(_logsDirectory);
        }

        public static void Write(Challenger challenger, string message)
        {
            Write(challenger.GetType().Name + message);
        }

        public static void Write(string message)
        {
            var dateTime = GetFormattedDate();
            var log = $"[{dateTime}] {message}";
#if DEBUG
            Debug.WriteLine(log);
#else
            File.AppendAllText(_logName, log + Environment.NewLine);
#endif
        }

        private static string GetFormattedDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
