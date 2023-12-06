/*
using Newtonsoft.Json;
using Optional;

namespace doot_gen
{
    struct Config
    {
        private string configFile;

        public Option<string> consolePath;
        public Option<string> projectPath;
        public Option<string> gamePath;
        public Option<string> bnkextrPath;
        public Option<string> vgmstreamPath;

        public Config()
        {
            configFile = Directory.GetCurrentDirectory() + @"\config.json"; 
            Reset();
        }

        public void Reset()
        {
            consolePath = Option.None<string>();
            projectPath = Option.None<string>();
            gamePath = Option.None<string>();
            bnkextrPath = Option.None<string>();
            vgmstreamPath = Option.None<string>();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            StreamWriter file = File.CreateText(configFile);
            file.Write(json);
            file.Close();
        }

        public void SaveTo(string path)
        {
            configFile = path;
            Save();
        }

        public void Reload()
        {
            this = LoadFrom(configFile);
        }


        public static Config LoadFrom(string path)
        {
            StreamReader stream = File.OpenText(path);
            Config config = JsonConvert.DeserializeObject<Config>(stream.ReadToEnd());
            stream.Close();
            config.configFile = path;
            return config;
        }
    }
}*/