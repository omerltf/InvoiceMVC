using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace InvoiceMaker.Repository
{
    public class InvoicesRepository
    {
        private Context context;
        public InvoicesRepository(Context context)
        {
            this.context = context;
        }

        public void Insert(Invoice invoice)
        {
            context.Invoices.Add(invoice);
            context.SaveChanges();
        }

        public List<Invoice> GetList()
        {
            List<Invoice> invoices;
             invoices=context.Invoices
                        .Include( i => i.client)
                        .ToList();
            return invoices;
        }

        public Invoice GetById (int id)
        {
            return context.Invoices
                    .Include(i => i.client)
                    .SingleOrDefault(i => i.Id == id);
        }

        public void Update(Invoice invoice)
        {
            context.Invoices.Attach(invoice);
            context.Entry(invoice).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}