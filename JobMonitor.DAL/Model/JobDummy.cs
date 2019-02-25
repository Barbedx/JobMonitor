using System;

namespace JobMonitor.DAL.Model
{
    public class JobDummy
    {
        public JobDummy(string serverName,Guid guid, string name)
        {
            Name = name;
            ServerName = serverName;
            Guid = guid;
        }
        public Guid Guid { get; set; }
        public string Name { get;       private  set; }
        public string ServerName { get; private set; }
        public DateTime? LastRunDate { get; set; }
        //public int CurentExecutionStatus { get; set; }
        //public string CurentExecutionStep { get; set; }
        public int? LastRunOutcome { get; set; }
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
    }
}
