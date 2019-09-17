using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.FormModels
{
    public class CreateWorkType
    {
        public string WorkTypeName { get; set; }

        public int Rate { get; set; }
    }
}