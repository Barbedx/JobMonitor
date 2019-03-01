using Job_Monitor.ViewModels;
using JobMonitor.BLL;
using JobMonitor.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Job_Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window 
    {
        static JobManager jobman = new JobManager(new JobRepository());
        public MainWindow()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = Jobs;
        }

        public List<JobViewModel> Jobs => jobman.GetAllJobInfo().Select(x => new JobViewModel(x)).ToList();


    }
}
