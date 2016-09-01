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
                HipChatHelper hipChatHelper;
                if (args.Length < 3)
                {
                    hipChatHelper = new HipChatHelper();
                    hipChatHelper.LoadSettings();
                }
                else
                {
                    string apiUrl = args[0];
                    string authToken = args[1];
                    string roomId = args[2];
                    hipChatHelper = new HipChatHelper(apiUrl, authToken, roomId);
                }

                const string message = @"testing HipChatHelper";

                hipChatHelper.SendNotification(message, notify: true);
                hipChatHelper.SaveSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}