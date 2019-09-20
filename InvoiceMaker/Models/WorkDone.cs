using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkDone
    {
        public int Id { get; set; }
        public DateTimeOffset StartedOn { get; set; }
        public DateTimeOffset? EndedOn { get; set; }
        //public string ClientName { get; private set; }
        //public string WorkTypeName { get; private set; }
        public int ClientId { get; set; }
        public int WorkTypeId { get; set; }
        public bool IsAssigned { get; set; }
        public int? InvoiceId { get; set; } 
        public Invoice Invoice { get; set; }
        //public decimal Rate { get; private set; }

        public Client Client { get; set; }
        public WorkType WorkType { get; set; }

        public WorkDone() { }

        public WorkDone(int id, Client client, WorkType workType)
        {
            this.Id = id;
            //this.ClientName = client.Name;
            //this.WorkTypeName = workType.Name;
            this.ClientId = client.Id;
            this.WorkTypeId = workType.Id;
            //this.Rate = workType.Rate;
            this.StartedOn = DateTime.Now;

            this.Client = client;
            this.WorkType = workType;
        }

        public WorkDone(int id, Client client, WorkType workType, DateTimeOffset startedOn)
        {
            this.Id = id;
            //this.ClientName = client.Name;
            //this.WorkTypeName = workType.Name;
            this.StartedOn = startedOn;
            this.ClientId = client.Id;
            this.WorkTypeId = workType.Id;
            //this.Rate = workType.Rate;
            this.Client = client;
            this.WorkType = WorkType;
        }

        public WorkDone(int id, Client client, WorkType workType, DateTimeOffset startedOn, DateTimeOffset? endedOn)
        {
            this.Id = id;
            //this.ClientName = client.Name;
            //this.WorkTypeName = workType.Name;
            this.StartedOn = startedOn;
            this.EndedOn = endedOn;
            this.ClientId = client.Id;
            this.WorkTypeId = workType.Id;
            //this.Rate = workType.Rate;
            this.Client = client;
            this.WorkType = WorkType;
        }

        public void Finished()
        {
            if (!(EndedOn.HasValue))
            {
                this.EndedOn = DateTimeOffset.Now;
            }

        }

        public decimal GetTotal()
        {
            if ((EndedOn.HasValue))
            {
                decimal timeWorked = (decimal)(EndedOn.Value-StartedOn).TotalHours;
                return this.WorkType.Rate * (timeWorked);
            }
            return -1;
        }
    }
}