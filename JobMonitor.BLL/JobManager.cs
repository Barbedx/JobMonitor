using JobMonitor.BLL.Model;
using JobMonitor.DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobMonitor.BLL
{
    public class JobManager
    {
        private readonly JobRepository jobRep;
        public JobManager(JobRepository jobRepository)
        {
            this.jobRep = jobRepository;
        }

        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();


        //public Job GetJobInfo(ConnectionConfiguration configuration)
        //{

        //    //var jobRep = new JobRepository();
        //    var prepJob = jobRep.GetJobInfo(configuration);
        //    return new Job(configuration)
        //    {
        //        LastRunDate = prepJob.LastRunDate,
        //        CurentExecutionStatus = (ExecutionStatus)prepJob.CurentExecutionStatus,
        //        CurentExecutionStep = prepJob.CurentExecutionStep,
        //        LastRunOutcome = (LastRunOutcome)prepJob.LastRunOutcome,
        //        LastOutcomeMessage = prepJob.LastOutcomeMessage,
        //        CurentRetryAttempt = prepJob.CurentRetryAttempt,
        //        NextRunDate = prepJob.NextRunDate,
        //        Enable = prepJob.Enable
        //    };

        //}


        public List<SqlServer> GetMockData(int count = 5)
        {
            var servers = new List<SqlServer>() { new SqlServer("azure50"), new SqlServer("azureD10"), new SqlServer("local40"), new SqlServer("local40f1") };
            var jobs = new List<string>() { "Load swe data", "load call file", "prepare reports", "night job" };
            List<Job> result = new List<Job>();
            var rnd = new Random();
            Array LastRunOutcomeArray = Enum.GetValues(typeof(LastRunOutcome));

            for (int i = 0; i < count; i++)
            {
                var server = servers[rnd.Next(jobs.Count)];
                server.Jobs.Add(new Job(server, new Guid(), jobs[rnd.Next(jobs.Count)])
                {
                    LastRunDate = DateTime.Now.AddDays(-1 * rnd.Next(10)),
                    LastRunOutcome = (LastRunOutcome)LastRunOutcomeArray.GetValue(rnd.Next(LastRunOutcomeArray.Length)),
                    LastOutcomeMessage = $"LastOutcomeMessage{i}",
                    NextRunDate = DateTime.Now.AddDays(rnd.Next(10)),
                    Description = $"Description: {server.SqlServerPath}",
                    JobOwner = "JobOwner",
                    JobCategory = "JobCategory",
                    NumberOfSteps = rnd.Next(100),
                    JobEnabled = rnd.Next(1) == 1,
                    IsScheduled = rnd.Next(1) == 1,
                    SheduleName ="SheduleName",
                    Frequency = "Frequency",
                    Recurrence = "Recurrence",
                    SubdayFrequency = "SubdayFrequency",
                    MaxDuration = TimeSpan.FromMinutes(rnd.Next(100)),
                    LastRunDuration = TimeSpan.FromMinutes(rnd.Next(100)),
                    LastRunStepNumber = rnd.Next(100),
                    LastRunStepName = "LastRunStepName",
                    LastRunStepMessage = "LastRunStepMessage",
                    LastRunCommand = "LastRunCommand",
                    IsRunning = rnd.Next(1) == 1,

                });
            }
            return servers;
        }

        public List<Job> GetAllJobInfo()
        {
            throw new NotImplementedException();
        }

        public SqlServer GetJobsFromServer(SqlServer server)
        {
            var jobsDummy = jobRep.GetJobsFromServer(server.SqlServerPath);

            var jobsCollection = jobsDummy
                .Select(x => new Job(server, x.Guid, x.Name)
                {
                    LastRunDate = x.LastRunDate,
                    LastRunOutcome = (LastRunOutcome?)x.LastRunOutcome,
                    LastOutcomeMessage = x.LastOutcomeMessage,
                    NextRunDate = x.NextRunDate,
                    Description = x.Description,
                    JobOwner = x.JobOwner,
                    JobCategory = x.JobCategory,
                    NumberOfSteps = x.NumberOfSteps,
                    JobEnabled = x.JobEnabled,
                    IsScheduled = x.IsScheduled,
                    SheduleName = x.SheduleName,
                    Frequency = x.Frequency,
                    Recurrence = x.Recurrence,
                    SubdayFrequency = x.SubdayFrequency,
                    MaxDuration = x.MaxDuration,
                    LastRunDuration = x.LastRunDuration,
                    LastRunStepNumber = x.LastRunStepNumber,
                    LastRunStepName = x.LastRunStepName,
                    LastRunStepMessage = x.LastRunStepMessage,
                    LastRunCommand = x.LastRunCommand,
                    IsRunning = x.IsRunning,
                    UpdatedDate = DateTime.Now,
                    SqlServerPath = server.SqlServerPath
                    
                })
                .ToList();
            jobsCollection.AddRange(server.Jobs);
            server.Jobs = jobsCollection; 
            return server;
        }


        public List<SqlServer> GetAllServersJobsInfo(IEnumerable<string> serverNames)
        {
            var resultList = new List<SqlServer>();

            foreach (var serverName in serverNames)
            {
                try
                {
                    var server = new SqlServer(serverName);
                    if (jobRep.IsServerAvailable(serverName))
                    {
                        server = GetJobsFromServer(server);
                        server.IsEnabled = true;
                        server.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        server.IsEnabled = false;
                        server.UpdatedDate = DateTime.Now;

                    }
                    resultList.Add(server);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Error when download data from server {serverName}. Error:{ex.Message}");
                }
            }
            return resultList;

        }

        public bool IsServerAvailable(string servername)
        {

            return jobRep.IsServerAvailable(servername);
        }

        private List<ConnectionConfiguration> _connectionConfigurations;

        public List<ConnectionConfiguration> ConnectionConfigurations
        => _connectionConfigurations ?? (_connectionConfigurations = jobRep.GetConnectionConfigurations());

        public string DeleteConnection(string jobName, string serverName)
        {
            var config = ConnectionConfigurations.FirstOrDefault(x => x.JobName.ToLower() == jobName.ToLower() && x.ServerName.ToLower() == serverName.ToLower());
            if (config == null)
            {
                return $"It's not exists job \"{jobName}\" on server \"{serverName}\" in list";
            }

            ConnectionConfigurations.Remove(config);
            try
            {
                jobRep.SaveConnectionConfigurations(ConnectionConfigurations);
            }
            catch (Exception ex)
            {
                ConnectionConfigurations.Add(config);
                throw ex;
            }
            return "Saving complete";
        }


        public string AddConnection(string jobName, string serverName)
        {
            if (ConnectionConfigurations.Any(x => x.JobName.ToLower() == jobName.ToLower() && x.ServerName.ToLower() == serverName.ToLower()))
            {
                return $"It's already exists job \"{jobName}\" on server \"{serverName}\" in list";
            }

            if (!jobRep.IsServerAvailable(serverName))
            {
                return $"Server \"{serverName}\" doesn't available";
            }
            if (!jobRep.IsJobExists(jobName, serverName))
            {
                return $"Job \"{jobName}\" doesn't exists on server \"{serverName}\"";
            }

            var config = new ConnectionConfiguration(jobName, serverName);
            ConnectionConfigurations.Add(config);
            try
            {

                jobRep.SaveConnectionConfigurations(ConnectionConfigurations);
            }
            catch (Exception ex)
            {
                ConnectionConfigurations.Remove(config);
                throw ex;
            }
            return "Saving complete";
        }

        public IEnumerable<string> GetjobsList(string serverName)
        {

            return jobRep.GetJobsList(serverName);

        }
         
    }
}
