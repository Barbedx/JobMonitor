using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace JobMonitor.WebApp
{
    internal class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}