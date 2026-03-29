using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using [[[project_name]]].Helpers;
using [[[project_name]]].Managers;

namespace [[[project_name]]] {
    public class Main : IModApi {
        public const string ConfigFilePath = "[[[config_stem]]].xml";
        public const string LocaleFolderPath = "i18n/";

        public void InitMod(Mod _modInstance) {
            Log.Out("[[[log_prefix]]] Initializing ...");
            ModEvents.GameAwake.RegisterHandler(Awake);
            ModEvents.GameUpdate.RegisterHandler(Update);
            ModEvents.PlayerSpawnedInWorld.RegisterHandler(OnPlayerRespawn);
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        public static string GetAbsolutePath(string relativeFilePath, bool requireExists = true) {
            var gameDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName;
            if (gameDirectory == null) {
                Log.Exception(new Exception("Game directory could not be found."));
                throw new Exception("Game directory could not be found.");
            }

            var filePath = Path.Combine(gameDirectory, relativeFilePath);
            if (!requireExists)
                return filePath;

            if (File.Exists(filePath))
                return filePath;

            if (Directory.Exists(filePath))
                return filePath;

            throw new Exception($"File {filePath} could not be found. Check the mods folder");
        }

        public static bool IsDedicatedServer() {
            return GameManager.Instance && GameManager.IsDedicatedServer;
        }

        private void Awake(ref ModEvents.SGameAwakeData data) {
            if (!IsDedicatedServer())
                return;

            ConfigManager.Instantiate();
            [[[project_name]]]Manager.Instantiate();
            LocaleManager.Instantiate(ConfigManager.Instance.Config.Language);
        }

        private void Update(ref ModEvents.SGameUpdateData data) {
            if (!IsDedicatedServer())
                return;

            ConfigManager.Instance.UpdateFromFile();
            if (ConfigManager.Instance.Config.Enabled)
                [[[project_name]]]Manager.Instance.CheckFor[[[project_name]]]();
        }

        private void OnPlayerRespawn(ref ModEvents.SPlayerSpawnedInWorldData data) {
            if (!ConfigManager.Instance.Config.Enabled)
                return;

            if (data.RespawnType != RespawnType.JoinMultiplayer)
                return;

            if ([[[project_name]]]Manager.Instance.IsTimeFlowing)
                return;

            MessageHelper.SendPrivateChat(LocaleManager.Instance.Localize("onlogin_[[[command_prefix_long]]]_active"), data.ClientInfo);
        }
    }
}
