using JobMonitor.DAL.Heplers;
using JobMonitor.DAL.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

using System.IO;

namespace JobMonitor.DAL
{
    public class JobRepository
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private SqlConnection GetConnection(string serverName) =>
                new SqlConnection(
                $"Data Source={serverName};Initial Catalog=msdb;Integrated Security=True");
        #region querys

        private const string JobMonitoringQuery = @"SELECT 
	j.job_id as JobGuid,
	j.Name AS [JobName], 
    Coalesce(j.Description, 'No description available.') AS 'Description',
    SUSER_SNAME(j.owner_sid) AS [JobOwner], 
    [sCAT].[name] AS [JobCategory]
    ,(SELECT COUNT(step_id) FROM [msdb].dbo.sysjobsteps WHERE job_id = j.job_id) AS [NumberofSteps],
    j.Enabled as [JobEnabled] ,

	  case when js.schedule_uid is null then 0 else 1 end as [IsScheduled], 
	  js.shedule_name,

     CASE js.freq_type
        WHEN 1 THEN 'Once'
        WHEN 4 THEN 'Daily'
        WHEN 8 THEN 'Weekly'
        WHEN 16 THEN 'Monthly'
        WHEN 32 THEN 'Monthly relative'
        WHEN 64 THEN 'When SQLServer Agent starts'
		when 128 THEN 'Start whenever the CPUs become idle'
    END as [Frequency],
	   CASE [freq_type]
        WHEN 4 THEN 'Occurs every ' + CAST([freq_interval] AS VARCHAR(3)) + ' day(s)'
        WHEN 8 THEN 'Occurs every ' + CAST([freq_recurrence_factor] AS VARCHAR(3)) 
                    + ' week(s) on '
                    + CASE WHEN [freq_interval] & 1 = 1 THEN 'Sunday' ELSE '' END
                    + CASE WHEN [freq_interval] & 2 = 2 THEN ', Monday' ELSE '' END
                    + CASE WHEN [freq_interval] & 4 = 4 THEN ', Tuesday' ELSE '' END
                    + CASE WHEN [freq_interval] & 8 = 8 THEN ', Wednesday' ELSE '' END
                    + CASE WHEN [freq_interval] & 16 = 16 THEN ', Thursday' ELSE '' END
                    + CASE WHEN [freq_interval] & 32 = 32 THEN ', Friday' ELSE '' END
                    + CASE WHEN [freq_interval] & 64 = 64 THEN ', Saturday' ELSE '' END
        WHEN 16 THEN 'Occurs on Day ' + CAST([freq_interval] AS VARCHAR(3)) 
                     + ' of every '
                     + CAST([freq_recurrence_factor] AS VARCHAR(3)) + ' month(s)'
        WHEN 32 THEN 'Occurs on '
                     + CASE [freq_relative_interval]
                        WHEN 1 THEN 'First'
                        WHEN 2 THEN 'Second'
                        WHEN 4 THEN 'Third'
                        WHEN 8 THEN 'Fourth'
                        WHEN 16 THEN 'Last'
                       END
                     + ' ' 
                     + CASE [freq_interval]
                        WHEN 1 THEN 'Sunday'
                        WHEN 2 THEN 'Monday'
                        WHEN 3 THEN 'Tuesday'
                        WHEN 4 THEN 'Wednesday'
                        WHEN 5 THEN 'Thursday'
                        WHEN 6 THEN 'Friday'
                        WHEN 7 THEN 'Saturday'
                        WHEN 8 THEN 'Day'
                        WHEN 9 THEN 'Weekday'
                        WHEN 10 THEN 'Weekend day'
                       END
                     + ' of every ' + CAST([freq_recurrence_factor] AS VARCHAR(3)) 
                     + ' month(s)'
      END AS [Recurrence],
    CASE(js.freq_subday_interval)
        WHEN 0 THEN 'Once'
        ELSE cast('Every ' 
                + right(js.freq_subday_interval,2) 
                + ' '
                +     CASE(js.freq_subday_type)
                            WHEN 1 THEN 'Once'
                            WHEN 4 THEN 'Minutes'
                            WHEN 8 THEN 'Hours'
                        END as char(16))
    END as 'Subday Frequency',
    'Next Start Date'= CONVERT(DATETIME, RTRIM(NULLIF(js.next_run_date, 0)) + ' '
        + STUFF(STUFF(REPLACE(STR(RTRIM(js.next_run_time),6,0),
        ' ','0'),3,0,':'),6,0,':')),
    'Max Duration' = STUFF(STUFF(REPLACE(STR(maxdur.run_duration,7,0),
        ' ','0'),4,0,':'),7,0,':'),
 

    'Last Run Duration' = STUFF(STUFF(REPLACE(STR(lastrun.run_duration,7,0),
        ' ','0'),4,0,':'),7,0,':'),  
    'Last Start Date' = CONVERT(DATETIME, RTRIM(lastrun.run_date) + ' '
        + STUFF(STUFF(REPLACE(STR(RTRIM(lastrun.run_time),6,0),
        ' ','0'),3,0,':'),6,0,':')),
    'Last Run Message' = lastrun.message,
    'Last Run Step No' = lastrunStep.Step_id,
    'Last Run Step Name' = lastrunStep.Step_name,
    'Last Run Step Message' = lastrunStep.message
	,lastrunJobStepInfo.command as LastRunCommand
    ,'Last_run_outcome' = lastrunJobStepInfo.Last_run_outcome
	--,isnull(last_executed_step_id,0)+1??
	,case when run_requested_date is null then 0 
		  when stop_execution_date is null then 1 else 0 end as IsRunning
FROM [msdb].dbo.sysjobs j
    LEFT JOIN [msdb].[dbo].[syscategories] AS [sCAT]
        ON j.[category_id] = [sCAT].[category_id]
LEFT OUTER JOIN	(
	select top 1 job_id, next_run_date, next_run_time,  name as Shedule_name, schedule_uid, 
	  freq_type,[freq_interval], [freq_recurrence_factor], freq_subday_interval,freq_subday_type ,[freq_relative_interval]
 from 
    [msdb].dbo.sysjobschedules js
	join [msdb].dbo.sysschedules s 
			ON js.schedule_id = s.schedule_id  
	order by case freq_type when 1 then 9999 else freq_type end,
			 case freq_subday_interval when 0 then 9999 when 1 then 9999 else freq_subday_interval end 
	) js
    ON j.job_id = js.job_id
	  

LEFT OUTER JOIN (SELECT job_id, max(run_duration) AS run_duration
        FROM [msdb].dbo.sysjobhistory
        GROUP BY job_id) maxdur
ON j.job_id = maxdur.job_id
-- INNER JOIN -- Swap Join Types if you don't want to include jobs that have never run
LEFT OUTER JOIN
    (SELECT j1.job_id, j1.run_duration, j1.run_date, j1.run_time, j1.message, j1.step_id,j1.step_name 
    FROM [msdb].dbo.sysjobhistory j1
    WHERE instance_id = (SELECT Max(instance_id) 
                         FROM [msdb].dbo.sysjobhistory j2 
                         WHERE j2.job_id = j1.job_id)
						 ) lastrun 
    ON j.job_id = lastrun.job_id

LEFT OUTER JOIN
    (SELECT j1.job_id, j1.run_duration, j1.run_date, j1.run_time, j1.message, j1.step_id,j1.step_name 
    FROM [msdb].dbo.sysjobhistory j1
    WHERE instance_id = (SELECT top 1 instance_id 
                         FROM [msdb].dbo.sysjobhistory j2 
                         WHERE j2.job_id = j1.job_id
						 order by run_date desc, run_time desc, step_id desc,instance_id)
						 ) lastrunStep
    ON j.job_id = lastrunStep.job_id
left outer join [msdb].dbo.[sysjobsteps] lastrunJobStepInfo
	on	lastrunJobStepInfo.job_id = lastrunStep.job_id
		and lastrunJobStepInfo.step_id = lastrunStep.step_id
LEFT OUTER JOIN (
SELECT    * FROM msdb.dbo.sysjobactivity SJ
 WHERE session_id = (SELECT Max(session_id) 
  FROM [msdb].dbo.sysjobactivity sj2 
  WHERE sj2.job_id = Sj.job_id)
  AND run_requested_date IS NOT NULL
  AND stop_execution_date IS NULL
  ) currentExecution on currentExecution.job_id = j.job_id
where  TRY_CONVERT(UNIQUEIDENTIFIER, j.name) IS  NULL

 ";
        #endregion

        public List<JobDummy> GetJobsFromServer(string serverName)
        {
            var resultList = new List<JobDummy>();
            try
            {
                using (SqlConnection con = GetConnection(serverName))
                {
                    using (var sqlCommand = new SqlCommand(JobMonitoringQuery, con))
                    {
                        con.Open();
                        using (var rdr = sqlCommand.ExecuteReader())
                        { 
                            while (rdr.Read())
                            {
                                resultList.Add(new JobDummy(serverName, Guid.Parse(rdr["JobGuid"].ToString()), rdr["JobName"].ToString())       //1
                                {                                                      //1
                                    Description = rdr["Description"].ToString(),                         //1
                                    JobOwner = rdr["JobOwner"].ToString(),                               //1
                                    JobCategory = rdr["JobCategory"].ToString(),                         //1
                                    NumberOfSteps = Convert.ToInt32(rdr["NumberofSteps"]),               //1
                                    JobEnabled = Convert.ToInt16(rdr["JobEnabled"])==1,                  //1
                                    IsScheduled = Convert.ToInt16( rdr["IsScheduled"])==1,               //1
                                    SheduleName = rdr["shedule_name"].ToString(),                        //1
                                    Frequency = rdr["Frequency"].ToString(),                             //1
                                    Recurrence = rdr["Recurrence"].ToString(),                           //1
                                    SubdayFrequency = rdr["Subday Frequency"].ToString(),                //1
                                    NextRunDate =  rdr.GetNullableDateTime("Next Start Date"),
                                    MaxDuration = rdr.ToNullableTimeSpan("Max Duration"),                     //1
                                    LastRunDuration = rdr.ToNullableTimeSpan("Last Run Duration"),            //1
                                    LastRunDate = rdr.GetNullableDateTime("Last Start Date"),                 //1
                                    LastOutcomeMessage = rdr["Last Run Message"].ToString(),               //1
                                    LastRunStepNumber =  rdr.ToNullableInt("Last Run Step No"),               //1
                                    LastRunStepName = rdr["Last Run Step Name"].ToString(),           //1
                                    LastRunStepMessage = rdr["Last Run Step Message"].ToString(),     //1
                                    LastRunCommand = rdr["LastRunCommand"].ToString() ,                   //1
                                    LastRunOutcome =  rdr.ToNullableInt("Last_run_outcome"),
                                    IsRunning = Convert.ToInt32(rdr["IsRunning"])==1
                                });
                            }
                            
                        }
                    }
                }
                return resultList;

            }
            catch (Exception ex)
            {
                logger.Error($"Get jobs for server :\"{serverName}\" failed! Error:{ex.Message} ");
                logger.Debug(ex,$"Get jobs for server :\"{serverName}\" failed! Error:{ex.Message} ");
                throw;
            }
        }

  
        public void SaveConnectionConfigurations(List<ConnectionConfiguration> connectionConfigurations, string file = "Config.json")
        {
            try
            {

                using (StreamWriter wr = new StreamWriter(file))
                {
                    wr.WriteLine(JsonConvert.SerializeObject(connectionConfigurations));
                    wr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetJobsList(string serverName, bool OnlyEnable = true)
        {
            using (SqlConnection con = new SqlConnection(
                 $"Data Source={serverName};Initial Catalog=msdb;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand($"SELECT  [name] FROM msdb.dbo.sysjobs WHERE { (OnlyEnable ? "enabled = 1 and" : " ") } "
                                                + "TRY_CONVERT(UNIQUEIDENTIFIER, name) IS  NULL", con);

                var result = new List<string>();
                try
                {
                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(rdr.GetValue(0).ToString());
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    Debug.Write($"Can not get list : {ex.Message}");
                    throw ex;
                }


            }
        }
        public bool IsJobExists(string jobName, string serverName)
        {
            using (SqlConnection con = new SqlConnection(
                  $"Data Source={serverName};Initial Catalog=msdb;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand($"SELECT  job_id FROM msdb.dbo.sysjobs WHERE(name = N'{jobName}')", con);

                try
                {
                    con.Open();
                    return cmd.ExecuteScalar() != null;
                }
                catch (Exception ex)
                {
                    Debug.Write($"Connection can't open : {ex.Message}");
                    return false;
                }

            }

        }
         

        public bool IsServerAvailable(string serverName)
        {
            using (SqlConnection con = new SqlConnection(
                    $"Data Source={serverName};Initial Catalog=msdb;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT 1", con);
                try
                {
                    con.Open();
                    return cmd.ExecuteScalar().ToString() == "1";
                }
                catch (Exception ex)
                {
                    Debug.Write($"Connection can't open : {ex.Message}");
                    return false;
                }

            }
        }

        public string timeformat(string time)
        {
            var inttime = Convert.ToInt32(time);
            string hr, min, sec;
            sec = Convert.ToString(inttime % 100);
            min = Convert.ToString(inttime % 10000 / 100);
            hr = Convert.ToString(inttime / 10000);

            if (hr.Length == 1) hr = "0" + hr;
            if (min.Length == 1) min = "0" + min;
            if (sec.Length == 1) sec = "0" + sec;
            return hr + ":" + min + ":" + sec;
        }
       

        public List<ConnectionConfiguration> GetConnectionConfigurations(string file = "Config.json")
        {
            var result = new List<ConnectionConfiguration>();
            if (File.Exists(file))
            {
                using (StreamReader rdr = new StreamReader(file))
                {
                    result = JsonConvert.DeserializeObject<List<ConnectionConfiguration>>(rdr.ReadToEnd());
                    rdr.Close();
                }
                return result;
            }
            else return new List<ConnectionConfiguration>();
        }
    }
}
