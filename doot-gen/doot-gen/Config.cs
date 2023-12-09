
using Newtonsoft.Json;
using Optional;
using Optional.Collections;
using Optional.Unsafe;
using System.Diagnostics;
using System.IO;

namespace doot_gen.doot_gen
{
    enum ConfigPath
    {
        Console = 1 << 0,
        Project = 1 << 1,
        GameFiles = 1 << 2,
        BnkExtr = 1 << 3,
        VGMStream = 1 << 4,
        _END = 1 << 5
    }

    internal class Config
    {
        Dictionary<ConfigPath, string> paths = new Dictionary<ConfigPath, string>();
        Dictionary<ConfigPath, Action<bool, string>> changeSubscriber = new Dictionary<ConfigPath, Action<bool, string>>();

        public bool PathAvailible(ConfigPath path)
        {
            return paths.ContainsKey(path);
        }

        public bool PathsAvailible(int pathMask)
        {
            for (ConfigPath path = (ConfigPath)1; path != ConfigPath._END; path = path.GetNext())
            {
                int i = (int)path;
                // Check if path was requested
                if ((i & pathMask) != i) continue;
                // Check if path is present
                if (!paths.ContainsKey(path)) return false;
            }
            return true;
        }

        public string GetPath(ConfigPath path)
        {
            return paths[path];
        }

        public bool TryGetPath(ConfigPath path, out string value)
        {
            bool found = PathAvailible(path);
            if (found) { value = GetPath(path); } else { value = "N/A"; }
            return found;
        }

        public void SetPath(ConfigPath path, string value)
        {
            paths[path] = value;
            if (changeSubscriber.TryGetValue(path, out Action<bool, string> action))
            {
                action(true, value);
            }
        }

        public void RemovePath(ConfigPath path)
        {
            paths.Remove(path);
            if (changeSubscriber.TryGetValue(path, out Action<bool, string> action))
            {
                action(false, null);
            }
        }

        public void SetUIAction(ConfigPath path, Action<bool, string> action)
        {
            changeSubscriber[path] = action;
            SyncUi();
        }

        private void SyncUi()
        {
            for (ConfigPath path = (ConfigPath)1; path != ConfigPath._END; path = path.GetNext())
            {
                if (!changeSubscriber.TryGetValue(path, out Action<bool, string> action)) continue;
                bool hasValue = paths.TryGetValue(path, out string value);
                action(hasValue, value);
            }
        }

        public void SaveTo(string configFile)
        {
            Debug.WriteLine("Saving config to " + configFile);
            string json = JsonConvert.SerializeObject(paths);
            StreamWriter file = File.CreateText(configFile);
            file.Write(json);
            file.Close();
        }

        public void LoadPathsFrom(string configFile)
        {
            Debug.WriteLine("Loading config from " + configFile);
            StreamReader stream = File.OpenText(configFile);
            try
            {
                paths = JsonConvert.DeserializeObject<Dictionary<ConfigPath, string>>(stream.ReadToEnd()); 
            }
            catch(Exception e)
            {
                paths = new Dictionary<ConfigPath, string>();
            }
            finally {
                stream.Close();
            }
            if (paths == null) { paths = new Dictionary<ConfigPath, string>(); }

            SyncUi();
        }

        public void Reset()
        {
            paths = new Dictionary<ConfigPath, string>();
            SyncUi();
        }
    }

    static class ConfigPathExtensions
    {
        public static ConfigPath GetNext(this ConfigPath path)
        {
            if (path == ConfigPath._END)
            {
                return ConfigPath._END;
            }
            return (ConfigPath)(((int)path) << 1);
        }
    }
}
