using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkLineItem : ILineItem
    {   
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset When { get; private set; }

        public WorkLineItem (WorkDone workDone)
        {
            if (workDone.GetTotal() != -1)
            {
                this.Amount = workDone.GetTotal();
            }
            this.Description = workDone.WorkTypeName;
            this.When = workDone.StartedOn;
        }
    }
}