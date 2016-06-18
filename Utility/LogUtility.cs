using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{

    public enum LogType { INFO, DEBUG, ERROR }

    public interface ILogable
    {
        string LogName { get; }
        string LogIntro();
    }

    public static class LogUtility
    {
        private static DateTime _currentDate;
        private static string _todayLogFolderLocation;

        static LogUtility()
        {
            _currentDate = DateTime.Today;

            CreateLogFolder();
            AddTodayFolder();
        }

        private static void CreateLogFolder()
        {
            string logFolderLocation = Path.Combine(Configuration.ServiceServerConfiguration.SERVICE_SERVER_LOCATION, "Logs");
            if (!Directory.Exists(logFolderLocation))
                Directory.CreateDirectory(logFolderLocation);
        }

        private static void AddTodayFolder()
        {
            _currentDate = DateTime.Today;
            _todayLogFolderLocation = Path.Combine(Configuration.ServiceServerConfiguration.SERVICE_SERVER_LOCATION, "Logs", DateTime.Today.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(_todayLogFolderLocation))
                Directory.CreateDirectory(_todayLogFolderLocation);
        }

        private static void CreateLogFile(ILogable item)
        {
            string logLocation = Path.Combine(_todayLogFolderLocation, String.Format("{0} - {1}.log", item.LogName, DateTime.Today.ToString("yyyy-MM-dd")));
            using (StreamWriter sw = File.CreateText(logLocation))
            {
                sw.WriteLine(item.LogIntro());
            }
        }

        public static void Log(ILogable item, LogType type, string message)
        {
            if (DateTime.Today.Date != _currentDate)
                AddTodayFolder();

            lock (item.LogName)
            {
                try
                {
                    string logLocation = Path.Combine(_todayLogFolderLocation, String.Format("{0} - {1}.log", item.LogName, DateTime.Today.ToString("yyyy-MM-dd")));
                    if (File.Exists(logLocation))
                        CreateLogFile(item);
                    string logLine = String.Format("{0} [Thread={1}] - {2} - {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzz"), Thread.CurrentThread.ManagedThreadId, type.ToString(), message);

                    using (StreamWriter sw = File.AppendText(logLocation))
                    {
                        sw.WriteLine(logLine);
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }


    }
}
