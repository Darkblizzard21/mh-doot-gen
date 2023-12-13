using doot_gen.doot_gen;
using Optional;
using System.Diagnostics;

namespace doot_gen.util
{
    internal class ProcessHandle
    {
        private bool _logOutput;
        private Process _process;
        private bool _disposed = false;
        private Option<string> failMsg;
        public ProcessHandle(bool logOutput = true)
        {
            _logOutput = logOutput;
            _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
        }

        public ProcessHandle CreateWindow(bool create=true)
        {
            if (_disposed) throw new InvalidDataException("Process allready finnished");
            _process.StartInfo.CreateNoWindow = !create;
            return this;
        }

        public ProcessHandle SetupFailMessage(string msg)
        {
            if (_disposed) throw new InvalidDataException("Process allready finnished");
            failMsg = msg.Some();
            return this;
        }

        public ProcessHandle Start(string fileName, string arguments)
        {
            if (_disposed) throw new InvalidDataException("Process allready finnished");
            _process.StartInfo.FileName = fileName;
            _process.StartInfo.Arguments = arguments;
            _process.Start();
            return this;
        }

        public ProcessHandle WaitForExit()
        {
            Dispose();
            return this;
        }

        private void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _process.WaitForExit();
            if (_process.ExitCode != 0)
            {

                Logger.Info("Process \"" + _process.StartInfo.FileName + "\" failed with exit code: " + _process.ExitCode.ToString());
                if (failMsg.TryGet(out string msg)) Logger.Warn(msg);
            }
            string? line;
            while ((line = _process.StandardOutput.ReadLine()) != null)
            {
                Logger.Info(line, LogLevel.Info);
            }
            Logger.Info("", LogLevel.Info);
        }


    }
}
