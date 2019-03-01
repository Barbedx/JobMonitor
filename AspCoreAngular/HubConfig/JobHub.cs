using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AspCoreAngular.HubConfig
{
    public class JobHub : Hub
    {

        public Task UpdateData(string sender, DateTime dateTime ,string   jobsListJson)
        {
            return Clients.AllExcept(new[] { Context.ConnectionId })
                //.SendAsync("UpdateData", sende);r
                .SendAsync("UpdateData", sender, dateTime  , jobsListJson);
        }

        #region client-client realization
        private static readonly ConcurrentDictionary<int, string> ConnectionMap = new ConcurrentDictionary<int, string>();

        public override Task OnConnectedAsync()
        {
           
            return base.OnConnectedAsync();
        }
        #endregion client-client realization


    }
}
