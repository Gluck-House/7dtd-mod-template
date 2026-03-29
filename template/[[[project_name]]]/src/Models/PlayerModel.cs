using System;
using System.Xml.Serialization;
using UnityEngine.Serialization;

namespace [[[project_name]]].Models
{
    [Serializable]
    public class PlayerModel
    {

        [XmlAttribute("ID")]
        public string id;
        
        [XmlAttribute("Name")]
        public string playerName;
        
        [XmlAttribute("Whitelisted")]
        public bool skip[[[project_name]]];

        public PlayerModel()
        {
            this.id = Guid.NewGuid().ToString();
            this.playerName = string.Empty;
            this.skip[[[project_name]]] = false;
        }
        
        public PlayerModel(ClientInfo clientInfo)
        {
            this.id = clientInfo.PlatformId.CombinedString;
            this.playerName = clientInfo.playerName;
            this.skip[[[project_name]]] = false;
        }
    }
}
