using NLog;
using System;
using System.IO;

namespace xjakubs.Logger {
    /// <summary>
    /// Simple web server. Serves the contents of a specified
    /// directory to an address plus port.
    /// </summary>
    public class Logs {
        private const String FILE_LOG_SUFFIX = ".log";
        private const String FOLDER_LOG = "logs";

        private static NLog.Logger mLogger;
        private bool mInit = false;
        private String logName;

        public Logs(String name) {
            logName = name;
            // If directory does not exist, create it. 
            if (!Directory.Exists(FOLDER_LOG)) {
                Directory.CreateDirectory(FOLDER_LOG);
            }

            // remove old logs
            removeOldLogs();

            // init
            try {
                mLogger = LogManager.GetCurrentClassLogger();
                var config = new NLog.Config.LoggingConfiguration();

                String timeStamp = getTimestamp(DateTime.Now);
                var logfile = new NLog.Targets.FileTarget("logfile") { FileName = FOLDER_LOG + Path.DirectorySeparatorChar + logName + timeStamp + FILE_LOG_SUFFIX };
                var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
                LogManager.Configuration = config;
                mInit = true;
            } catch (Exception ex) {
                Console.WriteLine("ERROR Logs: " + ex.Message);
            }
        }

        public void logInfo(String content) {
            if (mInit) {
                mLogger.Info(content);
            }
        }

        public void logError(String content) {
            if (mInit) {
                mLogger.Error(content);
            }
        }

        private void removeOldLogs() {
            string[] files = Directory.GetFiles(FOLDER_LOG);

            foreach (string file in files) {
                FileInfo fi = new FileInfo(file);
                if (fi.Name.Contains(logName)) {
                    if (fi.LastAccessTime < DateTime.Now.AddDays(-14)) {
                        fi.Delete();
                    }
                }
            }
        }

        private String getTimestamp(DateTime value) {
            return value.ToString("yyyyMMdd.HHmmss");
        }

    }
}

