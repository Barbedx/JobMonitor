namespace JobMonitor.Console
{ 
    public class ConnectionConfiguration
    {
        public ConnectionConfiguration(string jobName, string serverName)
        {
            JobName = jobName;
            ServerName = serverName;
        }

 
        public string JobName { get; set; }
 
        public string ServerName { get; set; }
 
        public string AdditionalQuery { get; set; }
    }
}