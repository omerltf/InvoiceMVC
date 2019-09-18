using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.FormModels
{
    public class CreateClient
    {
        public string Name { get; set; }

        public bool IsActivated { get; set; }
    }
}