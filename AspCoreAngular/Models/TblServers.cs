using System;
using System.Collections.Generic;

namespace AspCoreAngular.Models
{
    public partial class TblServers
    {
        public TblServers()
        {
            TblJobs = new HashSet<TblJobs>();
        }

        public string SqlServerPath { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<TblJobs> TblJobs { get; set; }
    }
}
