using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreAngular.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AspCoreAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly SqlJobMonitorContext _dbContext;

        public SampleDataController(SqlJobMonitorContext context)
        {
            _dbContext = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();

            //_hubContext.Clients.All.SendMessage("message from get","get message");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
        [HttpGet("[action]")]
        public IEnumerable<string> GetUsers()
        {
            return _dbContext.Users.Select(u => u.UserName).ToList();
        }
         

        [HttpPost]
        public string Post([FromBody]Message msg)
        {
            string retMessage = string.Empty;

            try
            {
                //_hubContext.Clients.All.SendMessage(msg.User, msg.Text);
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
