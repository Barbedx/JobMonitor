using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreAngular.Data;
using EFCore.BulkExtensions;
using JobMonitor.BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AspCoreAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Job>>> GetSqlServerList(IList<SqlServer>
            serversJobsData)
        {
            var username = User.Claims.FirstOrDefault()
            return await _dataBaseContext.Jobs.Include(j => j.SqlServer).ToListAsync();
            
            try
            {
                var jobs = serversJobsData.SelectMany(x => x.Jobs).ToList();
                _dataBaseContext.BulkInsertOrUpdate(serversJobsData);
                _dataBaseContext.BulkInsertOrUpdate(jobs);
                _hubContext.Clients.All.SendMessage("server", "Data has been updated");
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost("[action]")] 
        public string PostSqlServerList( IList<SqlServer> 
            serversJobsData)
        {
            //var serverList = JsonConvert.DeserializeObject<List<SqlServer>>(serversJobsData);
            string retMessage = string.Empty;

            try
            {
                var jobs = serversJobsData.SelectMany(x => x.Jobs).ToList();
                _dataBaseContext.BulkInsertOrUpdate(serversJobsData);
                _dataBaseContext.BulkInsertOrUpdate(jobs); 
                _hubContext.Clients.All.SendMessage("server", "Data has been updated");
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost("[action]")]
        public string PostSqlServer(SqlServer server)
        {
            //var serverList = JsonConvert.DeserializeObject<List<SqlServer>>(serversJobsData);
            string retMessage = string.Empty;

            try
            {
                _dataBaseContext.BulkInsertOrUpdate(new List<SqlServer> { server });// Becouse I dont know how to use EF :)
                _dataBaseContext.BulkInsertOrUpdate(server.Jobs.ToList());
                _hubContext.Clients.All.SendMessage("server", "Data has been updated");
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }


    }
}
