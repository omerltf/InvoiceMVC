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
    public class WorkTypeRepository
    {
        private Context context;
        public WorkTypeRepository(Context context)
        {
            this.context = context;
        }

        public WorkType GetWorkTypeById(int id)
        {
            return context.WorkTypes.SingleOrDefault(w => w.Id == id);
        }

        public List<WorkType> GetWorkType()
        {
            return context.WorkTypes.ToList();
        }

        public void Insert(WorkType workType)
        {
            context.WorkTypes.Add(workType);
            context.SaveChanges();
        }

        public void Update(WorkType workType)
        {
            context.WorkTypes.Attach(workType);
            context.Entry(workType).State = EntityState.Modified;
            context.SaveChanges();
        }

    }
}