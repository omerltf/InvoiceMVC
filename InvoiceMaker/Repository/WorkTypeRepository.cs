using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Repository
{
    public class WorkTypeRepository
    {
        string ConnectionString;
        public WorkTypeRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public WorkType GetWorkTypeById(int id)
        {
            WorkType type = new WorkType(0, null, 0);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT Id, WorkTypeName, Rate
                    FROM WorkType
                    WHERE Id = @Id
                    ORDER BY WorkTypeName
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int Id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    decimal rate = reader.GetDecimal(2);

                    type = new WorkType(Id, name, rate);
                }
            }
            return type;
        }

        public List<WorkType> GetWorkType()
        {
            List<WorkType> types = new List<WorkType>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT Id, WorkTypeName, Rate
                    FROM WorkType
                    ORDER BY WorkTypeName
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    decimal rate = reader.GetDecimal(2);

                    WorkType type = new WorkType(id, name, rate);
                    types.Add(type);
                }
            }
            return types;
        }

        public void Insert(WorkType workType)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"
                  INSERT INTO WorkType(WorkTypeName, Rate)
                  VALUES
                  (@WorkTypeName, @Rate)
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@WorkTypeName", workType.Name);
                command.Parameters.AddWithValue("@Rate", workType.Rate);
                command.ExecuteNonQuery();
            }
        }

        public void Update(WorkType workType)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"
                  UPDATE WorkType
                  SET WorkTypeName = @workTypeName
                    , Rate = @rate
                  WHERE Id = @id
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@workTypeName", workType.Name);
                command.Parameters.AddWithValue("@rate", workType.Rate);
                command.Parameters.AddWithValue("@id", workType.Id);
                command.ExecuteNonQuery();
            }
        }

    }
}