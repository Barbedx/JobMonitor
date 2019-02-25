using Job_Monitor.ViewModels;
using JobMonitor.BLL;
using JobMonitor.BLL.Model;
using JobMonitor.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
