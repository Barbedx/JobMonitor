using System.Runtime.Serialization;

namespace JobMonitor.DAL
{
    [DataContract]
    public class ConnectionConfiguration
    {
        public ConnectionConfiguration(string jobName, string serverName)
        {
            JobName = jobName;
            ServerName = serverName;
        }

        [DataMember]
        public string JobName { get; set; }
        [DataMember]
        public string ServerName{ get; set; }
        [DataMember]
        public string AdditionalQuery { get; set; }

        public override string ToString() =>
            $"{ServerName} => {JobName}"; 
        
    }

}
