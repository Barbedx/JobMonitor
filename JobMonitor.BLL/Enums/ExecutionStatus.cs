using System;
using System.Collections.Generic;

namespace JobMonitor.BLL.Model
{
    public class ExecutionStatus
    {
        private readonly String name;
        private readonly int value;


        public static readonly ExecutionStatus Undefined = new ExecutionStatus(0, "Returns only those jobs that are not idle or suspended.");
        public static readonly ExecutionStatus Executing = new ExecutionStatus(1, "Executing.");
        public static readonly ExecutionStatus Waiting = new ExecutionStatus(2, "Waiting for thread.");
        public static readonly ExecutionStatus OnRetry = new ExecutionStatus(3, "Between retries.");
        public static readonly ExecutionStatus Idle = new ExecutionStatus(4, "Idle.");
        public static readonly ExecutionStatus Suspended = new ExecutionStatus(5, "Suspended.");
        public static readonly ExecutionStatus Exiting = new ExecutionStatus(7, "Performing completion actions.");

        internal static ExecutionStatus Find(int searchValue)
        {
            return AllStatuses.Find(x => x.value == searchValue);
        }
        private ExecutionStatus(int value, String name)
        {
            this.name = name;
            this.value = value;
        }
        public static List<ExecutionStatus> AllStatuses = new List<ExecutionStatus>()
        {
            Undefined    ,
            Executing    ,
            Waiting      ,
            OnRetry      ,
            Idle         ,
            Suspended    ,
            Exiting
        };

        public override String ToString()
        {
            return name;
        }

        public static explicit operator ExecutionStatus(int b)  // explicit byte to digit conversion operator
        {
            return AllStatuses.Find(x => x.value == b);
        }
    }
}