using System;
using System.Collections.Generic;

namespace JobMonitor.BLL.Model
{
    public class SqlServer
    {
        public SqlServer(string path)
        {
            SqlServerPath = path;
            Jobs = new HashSet<Job>();
        }
        public SqlServer()
        {
            Jobs = new HashSet<Job>();
        }
         public string SqlServerPath { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}

