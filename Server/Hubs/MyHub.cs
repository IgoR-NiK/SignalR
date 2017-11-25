using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

using Server.Models;

namespace Server.Hubs
{
    public class MyHub : Hub
    {
        public void SendMessageServer(string message)
        {
            Clients.All.sendMessageClient(message);
        }

        public void DrawServer(List<Data> points)
        {
            Clients.AllExcept(Context.ConnectionId).drawClient(points);
        }
    }
}