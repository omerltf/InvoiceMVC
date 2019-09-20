using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker
{
    public class Md5HashModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += HandleBeginRequest;
        }

        private void HandleBeginRequest(object sender, EventArgs e)
        {
            InvoiceMakerApplication myInvoice = (InvoiceMakerApplication)sender;
            string[] splitString = new string[10];
            string path = myInvoice.Context.Request.Path;
            splitString = path.Split('/');

            if (splitString[1].Equals("api") && splitString[2].Equals("hash"))
            {
                myInvoice.Context.RewritePath(splitString[3] += ".hash");
            }
            else if (splitString[1].Equals("api") && splitString[2].Equals("binhash"))
            {
                myInvoice.Context.RewritePath(splitString[3] += ".binhash");
            }
            //api/hash/hello%world
        }
    }
}