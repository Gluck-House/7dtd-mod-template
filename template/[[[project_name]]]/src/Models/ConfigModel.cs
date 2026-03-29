using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using [[[project_name]]].Enums;

namespace [[[project_name]]].Models {
    [Serializable]
    [XmlRoot("[[[config_root_name]]]")]
    public class ConfigModel {
        public ConfigModel() {
            Enabled = true;
            Mode = EMode.Whitelist;
            Players = new List<PlayerModel>();
            MinPlayers = 5;
            DaysToSkip = 0;
            LoopLimit = 0;
            Language = "en_us";
        }

        public ConfigModel(bool enabled, EMode mode, List<PlayerModel> players, int minPlayers, int daysToSkip,
            int loopLimit, string language) {
            Enabled = enabled;
            Mode = mode;
            Players = players;
            MinPlayers = minPlayers;
            DaysToSkip = daysToSkip;
            LoopLimit = loopLimit;
            Language = language;
        }

        [XmlElement("Enabled")] public bool Enabled { get; set; }

        [XmlElement("Mode")] public EMode Mode { get; set; }

        [XmlArray("Players")] public List<PlayerModel> Players { get; set; }

        [XmlElement("MinPlayers")] public int MinPlayers { get; set; }

        [XmlElement("DaysToSkip")] public int DaysToSkip { get; set; }

        [XmlElement("LoopLimit")] public int LoopLimit { get; set; }

        [XmlElement("Language")] public string Language { get; set; }
    }
}