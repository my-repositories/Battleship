using System;

// TODO: REMOVE IT AFTER RELEASE v.1
namespace cmd
{
    public static class Logger
    {
#if !DEBUG
        private static readonly string LogsDirectory = $".{System.IO.Path.DirectorySeparatorChar}logs";
        private static readonly string LogName;

        static Logger()
        {
            LogName = $"{LogsDirectory}{System.IO.Path.DirectorySeparatorChar}{GetFormattedDate()}.txt";
            System.IO.Directory.CreateDirectory(LogsDirectory);
        }
#endif

        public static void Write(Challenger challenger, string message)
        {
            Write(challenger.GetType().Name + "." + message);
        }

        public static void Write(string message)
        {
            var dateTime = GetFormattedDate();
            var log = $"[{dateTime}] {message}";
#if DEBUG
            System.Diagnostics.Debug.WriteLine(log);
#else
            System.IO.File.AppendAllText(LogName, log + Environment.NewLine);
#endif
        }

        private static string GetFormattedDate()
        {
            return DateTime.Now.ToString("yyyy.MM.dd HH-mm-ss");
        }
    }
}
