using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkType
    {
        public string Name { get; private set; }
        public decimal Rate { get; private set; }
        public int Id { get; private set; }
        public WorkType(int id, string name, decimal rate)
        {
            this.Id = id;
            this.Name = name;
            this.Rate = rate;
        }
    }
}