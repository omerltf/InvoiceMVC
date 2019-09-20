using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class FeeLineItem : ILineItem
    {
        public decimal Amount { get; private set; }
            
        public string Description { get; private set; }

        public DateTimeOffset When { get; private set; }

        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public FeeLineItem(string description, decimal amount, DateTimeOffset when)
        {
            this.Amount = amount;
            this.Description = description;
            this.When = when;
        }
    }
}