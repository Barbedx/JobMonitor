using JobMonitor.BLL;
using JobMonitor.BLL.Model;
using JobMonitor.DAL;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobMonitor.SignalRDataAccessorr
{
    class Program
    {
        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static JobManager jobman = new JobManager(new JobRepository());
        public static string _hubPath{ get; set; }
        private static Uri _apiGetServerListPath { get; set; }
        public static Uri _apiPostServerJobsListPath { get; set; }
        public static int _timeoutInMS{ get; set; }
        public static bool IsTest { get; set; }

        static void Main(string[] args)
        {
            IsTest = args?.Length != 0;
            _hubPath = ConfigurationManager.AppSettings["hubPath"];
            _apiGetServerListPath =      new Uri( ConfigurationManager.AppSettings["ApiGetServerListPath"]);
            _apiPostServerJobsListPath = new Uri( ConfigurationManager.AppSettings["ApiPostServerJobsListPath"]);
            _timeoutInMS = Convert.ToInt16(ConfigurationManager.AppSettings["TimeoutInMilliseconds"]);

            var cancellationTokenSource = new CancellationTokenSource();

          
            Task.Run(() =>
                MainAsync(cancellationTokenSource.Token)
                .GetAwaiter()
                .GetResult()
            , cancellationTokenSource.Token)
            .ContinueWith((task) =>
            {
                if (task.IsFaulted)
                {
                    logger.Error($"Main async faulted with error: {task.Exception.Message}");
                    logger.Trace(task.Exception, task.Exception.StackTrace);
                }
                if (task.IsCompleted)
                {
                    logger.Info($"Main async complete");
                }
                if (task.IsCanceled)
                {
                    logger.Info($"Main async canceled!");
                }
            });

            System.Console.WriteLine("Press Enter to Exit ...");
            System.Console.ReadLine();
            cancellationTokenSource.Cancel();
           

        }

        private static IList<string> GetServerList()
        {
            if (IsTest) return new List<string> { "deuntp064" };
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept]="application/json";
                var result = client.DownloadString(_apiGetServerListPath); //URI  
                return JsonConvert.DeserializeObject<IList<string>>(result);
            }
        }
        private static void PostServerJobsList(IList<SqlServer> sqlServers)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = client.UploadString(_apiPostServerJobsListPath, "POST", JsonConvert.SerializeObject(sqlServers));
                logger.Trace($"Post data finished wit result : {result}");

            }
        }

        //TODO: Make windows Service!
        private static async Task MainAsync(CancellationToken  cancelationToken)
        {
            var hub = new HubConnectionBuilder()
                .WithUrl(_hubPath)
                .Build();
            logger.Trace($"Connection to hub \"{_hubPath}\" builded");
            //hub.StartAsync().Wait();
            logger.Trace($"Connection to hub \"{_hubPath}\" started");

            while (!cancelationToken.IsCancellationRequested)
            {
                var serverPathList =   GetServerList();

                var serverList = IsTest ? jobman.GetMockData(5) : jobman.GetAllServersJobsInfo(serverPathList);//GetAllJobInfo();
                logger.Trace($"Received from Data Server {serverList.Count} servers with {serverList.SelectMany(x => x.Jobs).Count()}");
                PostServerJobsList(serverList);

                //var jobListJson = JsonConvert.SerializeObject(serverList);


                //await hub.InvokeAsync("UpdateData","DataAccessor" ,DateTime.Now, jobListJson ,cancelationToken)
                //    .ContinueWith(t =>
                //   {
                //       if (t.IsFaulted)
                //       {
                //           System.Console.WriteLine($"Invoke method faulted with error{t.Exception.Message}");
                //           logger.Error($"Invoke method faulted with error{t.Exception.Message}");
                //           logger.Trace($"Invoke method faulted with Inner error{t.Exception.InnerException.Message}");
                //       } else System.Console.WriteLine($"Send to hub a {joblist.Count} rows");
                //   });
                //logger.Info($"Send to hub a {joblist.Count} rows");
                await Task.Delay(_timeoutInMS, cancelationToken);
            }
            await hub.DisposeAsync();
        }
         


        private static void RefreshData(HubConnection hubConnection)
        {
            var joblist = jobman.GetMockData(3);//GetAllJobInfo();
            hubConnection.InvokeAsync("UpdateData", joblist);
        }
    }
}
