using Microsoft.AspNetCore.SignalR;

namespace ActivitySeeker.Bll.Notification
{
    public class NotificationAdminHub: Hub
    {
        public async Task Send(string message)
        {
            if(Clients is not null)
            {
                await Clients.All.SendAsync("Receive", message);
            }
        }
    }
}
