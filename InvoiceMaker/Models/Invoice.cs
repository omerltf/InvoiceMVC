using InvoiceMaker.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class Invoice
    {
        //client id
        //list <WorkLineItems>
        //invoice state

        public int Id { get; set; }
        public int ClientId { get; set; }
        public InvoiceStatus Status { get; private set; }
        [Required]
        public string InvoiceNumber { get; private set; }

        //uncomment this later
        public List<ILineItem> LineItems { get; private set; }
        public Client client { get; set; }

        public List<WorkDone> Works { get; set; }
        public List<FeeLineItem> Fees { get; set; }


        public Invoice () {}

        public Invoice(string invoiceNumber)
        {
            InvoiceNumber = invoiceNumber;
            //LineItems = new List<ILineItem>();
            Status = InvoiceStatus.Open;
        }
        public Invoice(string invoiceNumber, int clientId, Client client)
        {
            this.ClientId = clientId;
            InvoiceNumber = invoiceNumber;
            //LineItems = new List<ILineItem>();
            Status = InvoiceStatus.Open;
            this.client = client;
        }

        public Invoice(string invoiceNumber, InvoiceStatus status)
            : this(invoiceNumber)
        {
            Status = status;
        }

        public void FinalizeInvoice()
        {
            if (Status == InvoiceStatus.Open)
            {
                Status = InvoiceStatus.Finalized;
            }
        }

        public void CloseInvoice()
        {
            if (Status == InvoiceStatus.Finalized)
            {
                Status = InvoiceStatus.Closed;
            }
        }

        //public void AddWorkLineItem(WorkDone workDone)
        //{
        //    LineItems.Add(new WorkLineItem(workDone));
        //}

        //public void AddFeeLineItem(string description, decimal amount, DateTimeOffset when)
        //{
        //    LineItems.Add(new FeeLineItem(description, amount, when));
        //}

       
    }
}