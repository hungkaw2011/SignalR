using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public static int notificationCounter = 0;
        public static List<string> messages = new();

        public async Task SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                notificationCounter++;
                await Clients.Others.SendAsync("Notification", Context.ConnectionId + ": " + message);
                messages.Add(message);
                await LoadMessages();
            }
        }
        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification", messages, notificationCounter);
        }
    }
}
