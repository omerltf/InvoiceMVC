using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels
{
    public class EditInvoice
    {
        public int Id { get; set; }
        public List<ILineItem> LineItems { get; private set; }
        public int ClientId { get; set; }
        public InvoiceStatus Status { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }

    }

    public class EditInvoiceView : EditInvoice
    {
        public EditInvoiceView() { }
        public EditInvoiceView(List<WorkDone> workDones)
        {
            this.WorkDones = workDones;
        }

        public List<Client> Clients { get; set; }
        public List<WorkDone> WorkDones { get; set; }
        public List<WorkLineItem> selectedWorkLineItems = new List<WorkLineItem>();
        public List<FeeLineItem> feeLineItems = new List<FeeLineItem>();

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

    }
}