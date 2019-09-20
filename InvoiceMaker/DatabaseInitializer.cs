using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InvoiceMaker
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            var omerLatif = new Client(0, "Omer Latif", true);

            var jamesChurchill = new Client(0, "James Churchill", true);

            var sarthakThakur = new Client(0, "Sarthak Thakur", true);

            var batman = new Client(0, "Batman", true);

            var vigilante = new WorkType(0, "Vigilante", 30);

            var programming = new WorkType(0, "Programming", 20);

            context.Clients.Add(omerLatif);
            context.Clients.Add(jamesChurchill);
            context.Clients.Add(sarthakThakur);
            context.Clients.Add(batman);
            context.WorkTypes.Add(vigilante);
            context.WorkTypes.Add(programming);
            context.SaveChanges();
        }
        

    }
}