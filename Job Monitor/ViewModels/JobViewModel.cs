using JobMonitor.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Job_Monitor.ViewModels
{
    public class JobViewModel
    {
        public JobViewModel(Job job)
        {
            Job = job;
        }

        public Job Job{ get; set; }
        public Brush JobColor
        {
            get
            {
                if (Job.IsRunning)
                {
                    return Brushes.ForestGreen;
                }
                if (LastRunMorethenDay)
                {
                    return Brushes.LightGoldenrodYellow;
                }
                switch (Job.LastRunOutcome)
                {
                    case LastRunOutcome.Failed:
                    case LastRunOutcome.Canceled:
                        return Brushes.PaleVioletRed;
                    default:
                        //case LastRunOutcome.Succeeded:
                        //case LastRunOutcome.Unknown:
                        return Brushes.Transparent;

                }
            }
        }

        public bool LastRunMorethenDay =>
            (DateTime.Today - Job.LastRunDate).Value.TotalHours > 24;
    }
}
