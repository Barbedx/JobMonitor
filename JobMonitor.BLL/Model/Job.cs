using System;
using System.Collections;
using System.Drawing;
using JobMonitor.DAL;
using Newtonsoft.Json;

namespace JobMonitor.BLL.Model
{
    public class Job
    {
        public Job(SqlServer sqlServer, Guid guid, string name)
        {
            SqlServer = sqlServer;
            Guid = guid;
            Name = name;
        }
        public Job() { }

        public Guid Guid { get;   set; }
        public string Name { get;   set; }
        public DateTime? LastRunDate { get; set; }

        public LastRunOutcome? LastRunOutcome { get; set; }
        public string LastOutcomeMessage { get; set; }
        //public int CurentRetryAttempt { get;  set; }
        public DateTime? NextRunDate { get;  set; }
        public string Description { get;  set; }
        public string JobOwner { get;  set; }
        public string JobCategory { get;  set; }
        public int NumberOfSteps { get;  set; }
        public bool JobEnabled { get;  set; }
        public bool IsScheduled { get;  set; }
        public string SheduleName { get;  set; }
        public string Frequency { get;  set; }
        public string Recurrence { get;  set; }
        public string SubdayFrequency { get;  set; }
        public TimeSpan? MaxDuration { get;  set; }
        public TimeSpan? LastRunDuration { get;  set; }
        public int? LastRunStepNumber { get;  set; }
        public string LastRunStepName { get;  set; }
        public string LastRunStepMessage { get;  set; }
        public string LastRunCommand { get;  set; }
        public bool IsRunning { get;  set; }
        public DateTime? UpdatedDate { get; set; }

        public string SqlServerPath { get; set; }

        [JsonIgnore]
        public virtual SqlServer SqlServer { get; set; }
    } 
}

