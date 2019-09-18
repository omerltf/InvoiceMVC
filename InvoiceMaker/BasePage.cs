using InvoiceMaker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker
{
    public class BasePage : Controller
    {
        protected Context context;
        
        public BasePage()
        {
            context = new Context();
        }

        private bool disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (disposed == true)
            {
                return;
            }

            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;

            base.Dispose(disposing);
        }
    }
}