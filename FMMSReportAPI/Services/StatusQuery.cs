using FMMSReportAPI.Interface;
using FMMSReportAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace FMMSReportAPI.Services
{
    public class StatusQuery : IStatusQuery
    {

        SqlConnection connection = new SqlConnection("Data Source=77.68.103.104;Initial Catalog=faultymeter;User ID=mojecadmin;Password=Admin123"); 

        public string CreateStatusReport(IDataReader reader)
        {
            string file = HttpContext.Current.Server.MapPath("/CSV/Status.csv");
            List<string> lines = new List<string>();

            string headerLine = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);

                }

                headerLine = string.Join(",", columns);
                lines.Add(headerLine);
            }


            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));

            }

            System.IO.File.WriteAllLines(file, lines);

            return file;

        }

        public string GetStatusReport(StatusModel status)
        {
            connection.Open();
            return CreateStatusReport(new SqlCommand($"Select * from FaultyMeters where Status = '{status.status}' and Daterecieved  between {status.from}   and {status.to}  and DiscoID = {status.discoId}", connection).ExecuteReader());
           
        }
    }
}