using Microsoft.AspNetCore.SignalR;

namespace AspCoreAngular
{
    public class MessageHub : Hub< IMessageHub>
    {
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
    }
}