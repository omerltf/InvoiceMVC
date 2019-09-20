using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels
{
    public class CreateInvoice
    {
        public int ClientId { get; set; }
        public InvoiceStatus Status { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        //public List<ILineItem> LineItems { get; private set; }
    }

    public class CreateInvoiceView : CreateInvoice
    {
        public List<Client> Clients { get; set; }

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