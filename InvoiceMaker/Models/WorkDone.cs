using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkDone
    {
        public int Id { get; private set; }
        public DateTimeOffset StartedOn { get; private set; }
        public DateTimeOffset? EndedOn { get; private set; }
        public string ClientName { get; private set; }
        public string WorkTypeName { get; private set; }
        public int ClientId { get; private set; }
        public int WorkTypeId { get; private set; }
        public decimal Rate { get; private set; }

        private Client Client { get; set; }
        private WorkType WorkType { get; set; }

        public WorkDone(int id, Client client, WorkType workType)
        {
            this.Id = id;
            this.ClientName = client.Name;
            this.WorkTypeName = workType.Name;
            this.ClientId = client.Id;
            this.WorkTypeId = workType.Id;
            this.Rate = workType.Rate;
            this.StartedOn = DateTime.Now;

            this.Client = client;
            this.WorkType = WorkType;
        }

        public WorkDone(int id, Client client, WorkType workType, DateTimeOffset startedOn)
        {
            this.Id = id;
            this.ClientName = client.Name;
            this.WorkTypeName = workType.Name;
            this.StartedOn = startedOn;
            this.ClientId = client.Id;
            this.WorkTypeId = workType.Id;
            this.Rate = workType.Rate;
            this.Client = client;
            this.WorkType = WorkType;
        }

        public WorkDone(int id, Client client, WorkType workType, DateTimeOffset startedOn, DateTimeOffset? endedOn)
        {
            this.Id = id;
            this.ClientName = client.Name;
            this.WorkTypeName = workType.Name;
            this.StartedOn = startedOn;
            this.EndedOn = endedOn;
            this.ClientId = client.Id;
            this.WorkTypeId = workType.Id;
            this.Rate = workType.Rate;
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
                return Rate * (timeWorked);
            }
            return -1;
        }
    }
}