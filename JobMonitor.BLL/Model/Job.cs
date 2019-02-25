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
            name = Name;
        }
        public Job() { }

        public Guid Guid { get; private set; }
        public string Name { get; private set; }
        public DateTime? LastRunDate { get; set; }

        public LastRunOutcome? LastRunOutcome { get; set; }
        public string LastOutcomeMessage { get; set; }
        //public int CurentRetryAttempt { get; internal set; }
        public DateTime? NextRunDate { get; internal set; }
        public string Description { get; internal set; }
        public string JobOwner { get; internal set; }
        public string JobCategory { get; internal set; }
        public int NumberOfSteps { get; internal set; }
        public bool JobEnabled { get; internal set; }
        public bool IsScheduled { get; internal set; }
        public string SheduleName { get; internal set; }
        public string Frequency { get; internal set; }
        public string Recurrence { get; internal set; }
        public string SubdayFrequency { get; internal set; }
        public TimeSpan? MaxDuration { get; internal set; }
        public TimeSpan? LastRunDuration { get; internal set; }
        public int? LastRunStepNumber { get; internal set; }
        public string LastRunStepName { get; internal set; }
        public string LastRunStepMessage { get; internal set; }
        public string LastRunCommand { get; internal set; }
        public bool IsRunning { get; internal set; }
        public DateTime? UpdatedDate { get; set; }

        public string SqlServerPath { get; set; }

        [JsonIgnore]
        public virtual SqlServer SqlServer { get; set; }
    } 
}

