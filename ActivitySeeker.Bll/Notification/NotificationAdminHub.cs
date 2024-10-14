using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
