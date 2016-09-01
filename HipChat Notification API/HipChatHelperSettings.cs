using HipChat_Notification_API.Properties;
using System;
using System.IO;
using System.Xml.Serialization;

namespace HipChat_Notification_API
{
    [Serializable]
    public class HipChatHelperSettings
    {
        public HipChatHelperSettings()
        {
            ApiUrl = string.Empty;
            AuthToken = string.Empty;
            RoomId = string.Empty;
        }

        [XmlElement("ApiUrl")]
        public string ApiUrl
        {
            get;
            set;
        }

        [XmlElement("AuthToken")]
        public string AuthToken
        {
            get;
            set;
        }

        [XmlElement("RoomId")]
        public string RoomId
        {
            get;
            set;
        }

        public void LoadSettings()
        {
            var serializer = new XmlSerializer(GetType());
            using (var fs = new FileStream(Settings.Default.SettingsFile, FileMode.Open, FileAccess.Read))
            {
                var deserialized = (HipChatHelperSettings)serializer.Deserialize(fs);
                ApiUrl = deserialized.ApiUrl;
                AuthToken = deserialized.AuthToken;
                RoomId = deserialized.RoomId;
            }
        }

        public void SaveSettings()
        {
            var serializer = new XmlSerializer(GetType());
            using (var fs = new FileStream(Settings.Default.SettingsFile, FileMode.Create, FileAccess.Write))
                serializer.Serialize(fs, this);
        }
    }
}