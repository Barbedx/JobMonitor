using System;
using System.Data.SqlClient;

namespace JobMonitor.DAL.Heplers
{
    public static class SqlDataReaderExtensions
    {
        public static DateTime? GetNullableDateTime(this SqlDataReader rdr, string field)
        {
            var data = rdr[field];
            if (data is DBNull)
                return null;
            else
                return Convert.ToDateTime(data);
        }
        public static TimeSpan? ToNullableTimeSpan(this SqlDataReader rdr, string field)
        {
            var str = rdr[field].ToString();
            if (string.IsNullOrWhiteSpace(str))
                return null;
            else return new TimeSpan(
                  Convert.ToInt32(str.Split(':')[0])
                , Convert.ToInt32(str.Split(':')[1])
                , Convert.ToInt32(str.Split(':')[2]));
        }
        public static int? ToNullableInt(this SqlDataReader rdr, string field)
        {
            var data = rdr[field];
            if (data is DBNull)
                return null;
            else
                return Convert.ToInt32(data);
        }
        //public static int GetInt(this SqlDataReader rdr, string field)
        //{
        //        return Convert.ToInt32(rdr[field]);
        //}
    }
}
