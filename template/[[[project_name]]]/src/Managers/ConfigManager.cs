using System;
using System.IO;
using System.Xml;
using [[[project_name]]].Models;
using [[[project_name]]].Wrappers;

namespace [[[project_name]]].Managers {
    public class ConfigManager {
        private readonly string _absoluteFilePath;
        private DateTime _lastModified = new DateTime(1970, 1, 1);

        private ConfigManager(string fileLocation) {
            _absoluteFilePath = Main.GetAbsolutePath(fileLocation, requireExists: false);
            Config = LoadConfig();
        }

        public ConfigModel Config { get; }

        public bool IsLoopLimitEnabled => Config.LoopLimit > 0;

        private bool IsFileModified() {
            return _lastModified != new FileInfo(_absoluteFilePath).LastWriteTime;
        }

        private ConfigModel LoadConfig() {
            var configModel = new ConfigModel();
            try {
                Log.Out("[[[log_prefix]]] Loading configuration file...");
                configModel = XmlSerializerWrapper.FromXml<ConfigModel>(_absoluteFilePath);
            }
            catch (Exception e) when (e is FileNotFoundException || e is XmlException) {
                Log.Error("[[[log_prefix]]] Configuration file is either corrupt or does not exist.");
                Log.Out("[[[log_prefix]]] Creating a configuration file");
                XmlSerializerWrapper.ToXml(_absoluteFilePath, configModel);
            }
            finally {
                if (File.Exists(_absoluteFilePath))
                    _lastModified = new FileInfo(_absoluteFilePath).LastWriteTime;
                Log.Out("[[[log_prefix]]] Configuration loaded.");
            }

            return configModel;
        }

        public void UpdateFromFile() {
            if (!File.Exists(_absoluteFilePath))
                return;

            if (!IsFileModified())
                return;

            XmlSerializerWrapper.FromXmlOverwrite(_absoluteFilePath, Config);
            Log.Out(LocaleManager.Instance.LocalizeWithPrefix("log_updated_config"));
            [[[project_name]]]Manager.Instance.UpdateLoopState();
        }

        public void SaveToFile() {
            if (!File.Exists(_absoluteFilePath))
                return;

            XmlSerializerWrapper.ToXml(_absoluteFilePath, Config);
            _lastModified = new FileInfo(_absoluteFilePath).LastWriteTime;
        }

        public int DecreaseDaysToSkip() {
            if (Config.DaysToSkip == 0)
                return 0;
            Config.DaysToSkip--;
            SaveToFile();
            return Config.DaysToSkip;
        }

        public static implicit operator bool(ConfigManager? instance) {
            return instance != null;
        }

        #region Singleton

        private static ConfigManager? _instance;

        public static ConfigManager Instance {
            get { return _instance ??= new ConfigManager(Main.ConfigFilePath); }
        }

        public static void Instantiate() {
            _instance = new ConfigManager(Main.ConfigFilePath);
        }

        #endregion
    }
}
