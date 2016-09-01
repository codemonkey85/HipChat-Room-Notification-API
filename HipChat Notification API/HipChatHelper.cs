using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace HipChat_Notification_API
{
    public class HipChatHelper
    {
        public string ApiUrl { get; set; }
        public string AuthToken { get; set; }

        public HipChatHelper()
        {
            ApiUrl = string.Empty;
            AuthToken = string.Empty;
        }

        public HipChatHelper(string apiUrl, string authToken)
        {
            ApiUrl = apiUrl;
            AuthToken = authToken;
        }

        public bool SendNotification(string message, string roomId, string messageFormat = "text", string color = "green", bool notify = false)
        {
            string refResponse = null;
            return SendNotification(message, roomId, messageFormat, color, notify, ref refResponse);
        }

        public bool SendNotification(string message, string roomId, string messageFormat, string color, bool notify, ref string response)
        {
            if (string.IsNullOrEmpty(ApiUrl.Trim()))
                throw new Exception("API Url must not be empty!");
            if (string.IsNullOrEmpty(AuthToken.Trim()))
                throw new Exception("API Token must not be empty!");
            if (string.IsNullOrEmpty(message.Trim()))
                throw new Exception("Message must not be empty!");
            if (string.IsNullOrEmpty(roomId.Trim()))
                throw new Exception("Room Id must not be empty!");

            if (response == null) response = string.Empty;
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(new
            {
                message,
                message_format = messageFormat,
                color,
                notify
            });
            string webResponse = string.Empty;
            return SendWebRequest(string.Format(ApiUrl, roomId, AuthToken), json, ref webResponse);
        }

        private bool SendWebRequest(string apiUrl, string json, ref string response)
        {
            var request = WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = json.Length;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }
            var httpResponse = request.GetResponse() as HttpWebResponse;

            if (httpResponse != null)
                return false;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
                return true;
            }
        }
    }
}