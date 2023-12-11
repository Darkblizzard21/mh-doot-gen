using Optional;

namespace doot_gen.util
{
    enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Error = 3
    }

    interface ILogSink
    {
        public abstract LogLevel GetLogLevel();
        public abstract void OverrideLogLevel(LogLevel newLogLevel);
        public abstract void Log(string message, LogLevel prio);
    }

    class ConsoleLogger : ILogSink
    {
        private LogLevel _logLevel;
        public ConsoleLogger(LogLevel initalLogLevel)
        {
            _logLevel = initalLogLevel;
        }

        public LogLevel GetLogLevel()
        {
            return _logLevel;
        }

        public void OverrideLogLevel(LogLevel newLogLevel)
        {
            _logLevel = newLogLevel;
        }

        public void Log(string msg, LogLevel prio)
        {
            if (prio < _logLevel) return;
            Console.WriteLine(msg);
        }
    }

    class FileLogger : ILogSink
    {
        private LogLevel _logLevel;
        private string _fileLocation;
        public FileLogger(string fileLocation, LogLevel initalLogLevel, Option<string> logHeader)
        {
            if (File.Exists(fileLocation)) File.Delete(fileLocation);
            _logLevel = initalLogLevel;
            _fileLocation = fileLocation;

            try
            {
                using (StreamWriter w = File.AppendText(_fileLocation))
                {
                    logHeader.DoIfPresent(str => w.WriteLine(str));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FileLogger failed to write to file: {}", ex.Message);
            }
        }
        public FileLogger(LogLevel initalLogLevel, Option<string> logHeader)
        {
            _logLevel = initalLogLevel;
            _fileLocation = "log-" + DateTime.Now.ToString("HH-mm-ss") + ".txt";

            try
            {
                using (StreamWriter w = File.AppendText(_fileLocation))
                {
                    logHeader.DoIfPresent(str => w.WriteLine(str));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FileLogger failed to write to file: {}", ex.Message);
            }
        }

        public LogLevel GetLogLevel()
        {
            return _logLevel;
        }

        public void OverrideLogLevel(LogLevel newLogLevel)
        {
            _logLevel = newLogLevel;
        }

        public void Log(string msg, LogLevel prio)
        {
            if (prio < _logLevel) return;
            try
            {
                using (StreamWriter w = File.AppendText(_fileLocation))
                {
                    w.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FileLogger failed to write to file: {}", ex.Message);
            }
        }
    }

    class MessageBoxLogger : ILogSink
    {
        private LogLevel _logLevel;
        public MessageBoxLogger(LogLevel initalLogLevel = LogLevel.Warn)
        {
            _logLevel = initalLogLevel;
        }

        public LogLevel GetLogLevel()
        {
            return _logLevel;
        }

        public void OverrideLogLevel(LogLevel newLogLevel)
        {
            _logLevel = newLogLevel;
        }

        public void Log(string msg, LogLevel prio)
        {
            if (prio < _logLevel) return;
            string titel = "DootGen-" + prio.ToString();
            string additonalMsg = prio == LogLevel.Error ? "\nYou can summit an error report as an issue on\n https://github.com/Darkblizzard21/mh-doot-gen/issues/new" : "";
            MessageBoxIcon icon = prio switch
            {
                LogLevel.Debug => MessageBoxIcon.Information,
                LogLevel.Info => MessageBoxIcon.Information,
                LogLevel.Warn => MessageBoxIcon.Warning,
                LogLevel.Error => MessageBoxIcon.Error,
                _ => throw new NotImplementedException()
            };
            MessageBox.Show(
                       msg + additonalMsg, 
                       titel,
                       MessageBoxButtons.OK,
                       icon);
        }
    }
}
