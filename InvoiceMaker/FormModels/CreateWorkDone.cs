using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels
{
    public class CreateWorkDone
    {
        public int ClientId { get; set; }
        public int WorkTypeId { get; set; }
        public DateTimeOffset StartedOn { get; set; }
    }

    public class CreateWorkDoneView : CreateWorkDone
    {
        public List<Client> Clients { get; set; }
        public List<WorkType> WorkTypes { get; set; }

        public List<SelectListItem> ClientSelectListItems
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var client in Clients)
                {
                    items.Add(new SelectListItem { Text = client.Name, Value = client.Id.ToString() });
                }
                return items;
            }
        }

        public List<SelectListItem> WorkTypeSelectListItems
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var workType in WorkTypes)
                {
                    items.Add(new SelectListItem { Text = workType.Name, Value = workType.Id.ToString() });
                }
                return items;
            }
        }
    }
}