using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.FormModels
{
    public class EditWorkType
    {
        public int Id { get; set; }

        public string WorkTypeName { get; set; }

        public decimal Rate { get; set; }
    }
}