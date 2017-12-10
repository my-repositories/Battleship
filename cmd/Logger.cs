using System;
using System.Diagnostics;

// TODO: REMOVE IT AFTER RELEASE v.1
namespace cmd
{
    public class Logger
    {
        public static void Write(string message)
        {
            var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var log = $"[{dateTime}] {message}";
#if DEBUG
            WriteToConsole(log);
#else
            WriteToFile(log);
#endif
        }
#if DEBUG
        private static void WriteToConsole(string message)
        {
            Debug.WriteLine(message);
        }
#else
        private static void WriteToFile(string message)
        {
            Console.WriteLine(message);
        }
#endif
    }
}
