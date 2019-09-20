using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace InvoiceMaker.Repository
{
    public class WorkDoneRepository
    {
        private Context context;
        public WorkDoneRepository(Context context)
        {

            this.context = context;
        }
        
        public List<WorkDone> GetWorkDoneListCustom(int id)
        {
            return context.WorkDones
                    .Include(wd => wd.Client)
                    .Include(wd => wd.WorkType)
                    .Where(wd => wd.EndedOn.HasValue && wd.ClientId==id && wd.IsAssigned == false)
                    .ToList();
        }

        public List<WorkDone> GetWorkDoneList()
        {
            return context.WorkDones
                    .Include(wd => wd.Client)
                    .Include(wd => wd.WorkType)
                    .ToList();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    string sql = @"
            //        SELECT wd.Id, wd.ClientId, wd.WorkTypeId, wd.StartedOn,
            //                wd.EndedOn, c.ClientName, c.IsActivated,
            //                wt.WorkTypeName, wt.Rate
            //        FROM WorkDone AS wd
            //        JOIN Client AS c ON (wd.ClientId = c.Id)
            //        JOIN WorkType AS wt ON (wd.WorkTypeId = wt.Id)
            //    ";
            //    SqlCommand command = new SqlCommand(sql, connection);
            //    SqlDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        int wdId = reader.GetInt32(0);
            //        int wdClientId = reader.GetInt32(1);
            //        int wdWorkTypeId = reader.GetInt32(2);
            //        DateTimeOffset wdStartedOn = reader.GetDateTimeOffset(3);
            //        DateTimeOffset? wdEndedOn = null;
            //        if (!reader.IsDBNull(4))
            //        {
            //            wdEndedOn = reader.GetDateTimeOffset(4);
            //        }
            //        string cClientName = reader.GetString(5);
            //        bool cIsActivated = reader.GetBoolean(6);
            //        string wtWorkTypeName = reader.GetString(7);
            //        decimal wtRate = reader.GetDecimal(8);
            //        Client client = new Client(wdClientId, cClientName, cIsActivated);
            //        WorkType workType = new WorkType(wdWorkTypeId, wtWorkTypeName, wtRate);

            //        if (wdEndedOn.HasValue)
            //        {
            //            works.Add(new WorkDone(wdId, client, workType, wdStartedOn, wdEndedOn.Value));
            //        }
            //        else
            //        {
            //            works.Add(new WorkDone(wdId, client, workType, wdStartedOn));
            //        }
            //    }
            //}
        }

        public WorkDone GetById(int id)
        {
            return context.WorkDones
                    .Include(wd => wd.Client)
                    .Include(wd => wd.WorkType)
                    .Where(wd => wd.Id == id)
                    .SingleOrDefault();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string sql = @"
        //    SELECT wd.Id, wd.ClientId, wd.WorkTypeId, wd.StartedOn,
        //            wd.EndedOn, c.ClientName, c.IsActivated,
        //            wt.WorkTypeName, wt.Rate
        //    FROM WorkDone AS wd
        //    JOIN Client AS c ON (wd.ClientId = c.Id)
        //    JOIN WorkType AS wt ON (wd.WorkTypeId = wt.Id)
        //    WHERE wd.Id = @id
        //";
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        command.Parameters.AddWithValue("@id", id);
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int wdId = reader.GetInt32(0);
        //            int wdClientId = reader.GetInt32(1);
        //            int wdWorkTypeId = reader.GetInt32(2);
        //            DateTimeOffset wdStartedOn = reader.GetDateTimeOffset(3);
        //            DateTimeOffset? wdEndedOn = null;
        //            if (!reader.IsDBNull(4))
        //            {
        //                wdEndedOn = reader.GetDateTimeOffset(4);
        //            }
        //            string cClientName = reader.GetString(5);
        //            bool cIsActivated = reader.GetBoolean(6);
        //            string wtWorkTypeName = reader.GetString(7);
        //            decimal wtRate = reader.GetDecimal(8);
        //            Client client = new Client(wdClientId, cClientName, cIsActivated);
        //            WorkType workType = new WorkType(wdWorkTypeId, wtWorkTypeName, wtRate);

        //            if (wdEndedOn.HasValue)
        //            {
        //                return new WorkDone(wdId, client, workType, wdStartedOn, wdEndedOn.Value);
        //            }
        //            else
        //            {
        //                return new WorkDone(wdId, client, workType, wdStartedOn);
        //            }
        //        }
        //    }
            //return null;
        }

        public void Insert(WorkDone workDone)
        {
            context.WorkDones.Add(workDone);
            context.SaveChanges();
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    string sql = @"
            //      INSERT INTO WorkDone(ClientId, WorkTypeId, StartedOn, EndedOn)
            //      VALUES
            //      (@ClientId, @WorkTypeId, @StartedOn, @EndedOn)
            //    ";
            //    SqlCommand command = new SqlCommand(sql, connection);
            //    command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
            //    command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
            //    command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
            //    if (workDone.EndedOn == null) { command.Parameters.AddWithValue("@EndedOn", DBNull.Value); }
            //    else { command.Parameters.AddWithValue("@EndedOn", workDone.EndedOn); }
            //    command.ExecuteNonQuery();
            //}
        }

        public void Update(WorkDone workDone)
        {
            context.WorkDones.Attach(workDone);
            context.Entry(workDone).State = EntityState.Modified;
            context.SaveChanges();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    string sql = @"
            //      UPDATE WorkDone
            //      SET ClientId = @ClientId
            //        , WorkTypeId = @WorkTypeId
            //        , StartedOn = @StartedOn
            //        , EndedOn = @EndedOn
            //      WHERE Id = @id
            //    ";
            //    SqlCommand command = new SqlCommand(sql, connection);
            //    command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
            //    command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
            //    command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
            //    if (!workDone.EndedOn.HasValue) { command.Parameters.AddWithValue("@EndedOn", DBNull.Value); }
            //    else { command.Parameters.AddWithValue("@EndedOn", workDone.EndedOn); }
            //    command.Parameters.AddWithValue("@id", workDone.Id);
            //    command.ExecuteNonQuery();
            //}
        }

    }
}