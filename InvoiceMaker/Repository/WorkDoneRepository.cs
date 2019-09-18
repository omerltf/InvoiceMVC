using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Repository
{
    public class WorkDoneRepository
    {
        string connectionString;
        public WorkDoneRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public List<WorkDone> GetWorkDoneList()
        {
            List<WorkDone> works = new List<WorkDone>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT wd.Id, wd.ClientId, wd.WorkTypeId, wd.StartedOn,
                            wd.EndedOn, c.ClientName, c.IsActivated,
                            wt.WorkTypeName, wt.Rate
                    FROM WorkDone AS wd
                    JOIN Client AS c ON (wd.ClientId = c.Id)
                    JOIN WorkType AS wt ON (wd.WorkTypeId = wt.Id)
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int wdId = reader.GetInt32(0);
                    int wdClientId = reader.GetInt32(1);
                    int wdWorkTypeId = reader.GetInt32(2);
                    DateTimeOffset wdStartedOn = reader.GetDateTimeOffset(3);
                    DateTimeOffset? wdEndedOn = null;
                    if (!reader.IsDBNull(4))
                    {
                        wdEndedOn = reader.GetDateTimeOffset(4);
                    }
                    string cClientName = reader.GetString(5);
                    bool cIsActivated = reader.GetBoolean(6);
                    string wtWorkTypeName = reader.GetString(7);
                    decimal wtRate = reader.GetDecimal(8);
                    Client client = new Client(wdClientId, cClientName, cIsActivated);
                    WorkType workType = new WorkType(wdWorkTypeId, wtWorkTypeName, wtRate);

                    if (wdEndedOn.HasValue)
                    {
                        works.Add(new WorkDone(wdId, client, workType, wdStartedOn, wdEndedOn.Value));
                    }
                    else
                    {
                        works.Add(new WorkDone(wdId, client, workType, wdStartedOn));
                    }
                }
            }
            return works;
        }

        public WorkDone GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
            SELECT wd.Id, wd.ClientId, wd.WorkTypeId, wd.StartedOn,
                    wd.EndedOn, c.ClientName, c.IsActivated,
                    wt.WorkTypeName, wt.Rate
            FROM WorkDone AS wd
            JOIN Client AS c ON (wd.ClientId = c.Id)
            JOIN WorkType AS wt ON (wd.WorkTypeId = wt.Id)
            WHERE wd.Id = @id
        ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int wdId = reader.GetInt32(0);
                    int wdClientId = reader.GetInt32(1);
                    int wdWorkTypeId = reader.GetInt32(2);
                    DateTimeOffset wdStartedOn = reader.GetDateTimeOffset(3);
                    DateTimeOffset? wdEndedOn = null;
                    if (!reader.IsDBNull(4))
                    {
                        wdEndedOn = reader.GetDateTimeOffset(4);
                    }
                    string cClientName = reader.GetString(5);
                    bool cIsActivated = reader.GetBoolean(6);
                    string wtWorkTypeName = reader.GetString(7);
                    decimal wtRate = reader.GetDecimal(8);
                    Client client = new Client(wdClientId, cClientName, cIsActivated);
                    WorkType workType = new WorkType(wdWorkTypeId, wtWorkTypeName, wtRate);

                    if (wdEndedOn.HasValue)
                    {
                        return new WorkDone(wdId, client, workType, wdStartedOn, wdEndedOn.Value);
                    }
                    else
                    {
                        return new WorkDone(wdId, client, workType, wdStartedOn);
                    }
                }
            }
            return null;
        }

        public void Insert(WorkDone workDone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                  INSERT INTO WorkDone(ClientId, WorkTypeId, StartedOn, EndedOn)
                  VALUES
                  (@ClientId, @WorkTypeId, @StartedOn, @EndedOn)
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
                command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
                command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
                if (workDone.EndedOn == null) { command.Parameters.AddWithValue("@EndedOn", DBNull.Value); }
                else { command.Parameters.AddWithValue("@EndedOn", workDone.EndedOn); }
                command.ExecuteNonQuery();
            }
        }

        public void Update(WorkDone workDone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                  UPDATE WorkDone
                  SET ClientId = @ClientId
                    , WorkTypeId = @WorkTypeId
                    , StartedOn = @StartedOn
                    , EndedOn = @EndedOn
                  WHERE Id = @id
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
                command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
                command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
                if (!workDone.EndedOn.HasValue) { command.Parameters.AddWithValue("@EndedOn", DBNull.Value); }
                else { command.Parameters.AddWithValue("@EndedOn", workDone.EndedOn); }
                command.Parameters.AddWithValue("@id", workDone.Id);
                command.ExecuteNonQuery();
            }
        }

    }
}