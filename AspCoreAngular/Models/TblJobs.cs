using System;
using System.Collections.Generic;

namespace AspCoreAngular.Models
{
    public partial class TblJobs
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTime? LastRunDate { get; set; }
        public int? LastRunOutcome { get; set; }
        public string SqlServerPath { get; set; }
        public string LastOutcomeMessage { get; set; }
        public DateTime? NextRunDate { get; set; }
        public string Description { get; set; }
        public string JobOwner { get; set; }
        public string JobCategory { get; set; }
        public int NumberOfSteps { get; set; }
        public bool JobEnabled { get; set; }
        public bool IsScheduled { get; set; }

        public string SheduleName { get; set; }
        public string Frequency { get; set; }
        public string Recurrence { get; set; }
        public string SubdayFrequency { get; set; }
        public int? MaxDuration { get; set; }
        public int? LastRunDuration { get; set; }
        public int LastRunStepNumber { get; set; }
        public string LastRunStepName { get; set; }
        public string LastRunStepMessage { get; set; }
        public string LastRunCommand { get; set; }
        public bool IsRunning { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TblServers SqlServerPathNavigation { get; set; }
    }
}
