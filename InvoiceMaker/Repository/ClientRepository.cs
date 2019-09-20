using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Repository
{
    public class ClientRepository
    {
        private Context context;

        //public ClientRepository()
        //{
        //    connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        //}

        public ClientRepository(Context context)
        {
            this.context = context;
        }

        public Client GetById (int id)
        {
            return context.Clients.SingleOrDefault(c => c.Id == id);
        }

        public List<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public void Insert(Client client)
        {
            context.Clients.Add(client);
            context.SaveChanges();
        }

        public void Update(Client client)
        {
            context.Clients.Attach(client);
            context.Entry(client).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}