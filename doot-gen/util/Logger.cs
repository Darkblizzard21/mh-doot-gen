namespace doot_gen.util
{

    internal class Logger
    {
        public static LogLevel Defaut
        {
            get
            {
#if DEBUG
                return LogLevel.Debug;
#else
                return LogLevel.Info;
#endif
            }
        }

        public static void Debug(string message) { Log("DEBUG:  " +message, LogLevel.Debug); }
        public static void Debug(string format, params object?[]? arg) { Log("DEBUG:  " + string.Format(format, arg), LogLevel.Debug); }
        public static void Info(string message) { Log("INFO:   " + message, LogLevel.Info); }
        public static void Info(string format, params object?[]? arg) { Log("INFO:   " + string.Format(format, arg), LogLevel.Info); }
        public static void Warn(string message) { Log("WARN:   " + message, LogLevel.Warn); }
        public static void Warn(string format, params object?[]? arg) { Log("WARN:   " + string.Format(format, arg), LogLevel.Warn); }
        public static void Error(string message) { Log("Error: " + message, LogLevel.Error); }
        public static void Error(string format, params object?[]? arg) { Log("Error: " + string.Format(format, arg), LogLevel.Error); }

        public static void Log(string message, LogLevel prio)
        {
            if (prio < Defaut) return;
            instance.sinks.ForEach(s => s.Log(message, prio));
        }
        public static List<ILogSink> CurrentSinks()
        {
            return instance.sinks;
        }
        public static void AddSink(ILogSink sink)
        {
            instance.sinks.Add(sink);
        }
        public static void RemoveSink(ILogSink sink)
        {
            instance.sinks.Remove(sink);
        }


        private Logger()
        {
#if DEBUG
            sinks.Add(new ConsoleLogger(LogLevel.Debug));
#endif
            sinks.Add(new MessageBoxLogger());
        }
        private static Logger instance = new Logger();
        private List<ILogSink> sinks = new List<ILogSink>();

    }
}
