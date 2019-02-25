using JobMonitor.BLL;
using JobMonitor.BLL.Model;
using JobMonitor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobMonitor.Console.Commands
{
    public static class DefaultCommands
    {
        static JobManager jobman = new JobManager(new JobRepository());

        public static string Help()
        {
            return "Help:" + Environment.NewLine
                + "Commands list:" + Environment.NewLine
                + " Help: show this help, example: console>Help" + Environment.NewLine
                + " Show: show all list of jobs, example: console>Show" + Environment.NewLine
                + " Exit: Close application" + Environment.NewLine
                + " Add [jobName] [serverName]: Add  server and job to monitoring list, example: console>Add \"Night job\" \"Azr-wew99\"" + Environment.NewLine
                + " Delete [jobName] [serverName]: Delete server and job from monitoring list, example: console>Delete \"Night job\" \"Azr-wew99\"" + Environment.NewLine
                + " ConList: Print all available job and server pairs" + Environment.NewLine
                + " JobList [serverName]: Print all job on server" + Environment.NewLine

                ;
        }
        #region Show all job commands
        /// <summary>
        /// Show list of  all available  Jobs
        /// </summary>
        /// <returns></returns>
        public static string Show()
        {
            try
            {
                var tempKey = ConsoleKey.EraseEndOfFile;
                List<Job> joblist;
                while (true)
                {
                    System.Console.WriteLine("Please wait. Loading in progress");
                    joblist = jobman.GetAllJobInfo();
                    System.Console.Clear();
                    ShowInfo(joblist);
                    tempKey = System.Console.ReadKey().Key;
                    if (tempKey == ConsoleKey.Q || tempKey == ConsoleKey.Escape)
                    {
                        return "Wait for command, for example \"help\" ";
                    }

                }
            }
            catch (Exception ex)
            {
                return $"Error when downloading data. Message:{ex.Message}";
            }
        }

        private static void ShowInfo(List<Job> jobList)
        {
            foreach (var item in jobList)
            {
                Write(item); 
                System.Console.WriteLine();
            }

            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("Press any key to refresh, ESQ to Quit");
        }

        private static void Write(Job job)
        {
            if (job.IsRunning)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                switch (job.LastRunOutcome)
                {
                    case LastRunOutcome.Failed:
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LastRunOutcome.Succeeded:
                        System.Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case LastRunOutcome.Canceled:
                        System.Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case LastRunOutcome.Unknown:
                        break;
                    default:
                        break;
                }
            }

            System.Console.WriteLine($"{job.Name} {job.LastRunStepMessage}");
        }
        #endregion
        public static string Exit()
        {
            Environment.Exit(0);
            return string.Empty;
        }
        public static string Add(string jobName, string serverName)
        {
            return jobman.AddConnection(jobName, serverName);
        }

        public static string JobList(string serverName)
        {
            return jobman.IsServerAvailable(serverName) ?
                string.Join(Environment.NewLine, jobman.GetjobsList(serverName)) :
                    $"Server \"{serverName}\" doesn't available";
        }

        public static string Delete(string jobName, string serverName = "servak")
        {
            return jobman.DeleteConnection(jobName, serverName);

        }
        public static string ConList()
        {
            return
                string.Join(Environment.NewLine, jobman.ConnectionConfigurations);
            //jobman.ConnectionConfigurations.Select(x => $"{x.JobName} => {x.ServerName} {Environment.NewLine}").jo.ToString();
        }
    }

}