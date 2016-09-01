using HipChat_Notification_API;
using System;

namespace TestHipChatHelper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length <= 4)
                    throw new Exception("Must supply API Url, Auth Token, and Room Id as arguments!");

                string apiUrl = args[1];
                string authToken = args[2];
                string roomId = args[3];

                HipChatHelper hipChatHelper = new HipChatHelper
                {
                    ApiUrl = apiUrl,
                    AuthToken = authToken
                };

                const string message = @"testing HipChatHelper";

                hipChatHelper.SendNotification(message, roomId, notify: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}