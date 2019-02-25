using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreAngular.Models;
using EFCore.BulkExtensions;
using JobMonitor.BLL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspCoreAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private SqlJobMonitorContext _dataBaseContext;
        private IHubContext<MessageHub, IMessageHub> _hubContext;

        public DataController(SqlJobMonitorContext context, IHubContext<MessageHub, IMessageHub> hubContext)
        {
            _dataBaseContext = context;
            _hubContext = hubContext;
        }

        //public DataController(SqlJobMonitorContext context)
        //{
        //    _dataBaseContext = context;
        //}

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<string>>> ServersPathList()
        {
            return await _dataBaseContext.Servers.Select(x => x.SqlServerPath).ToListAsync();
        }



        [HttpPost]
        //[DisableRequestSizeLimit]
        //[]
        public string Post( IList<SqlServer> 
            serversJobsData)
        {
            //var serverList = JsonConvert.DeserializeObject<List<SqlServer>>(serversJobsData);
            string retMessage = string.Empty;

            try
            {
                _dataBaseContext.BulkInsertOrUpdateAsync(serversJobsData).ConfigureAwait(false);
                _hubContext.Clients.All.SendMessage("server", "Data has been updated");
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
