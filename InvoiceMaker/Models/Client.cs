using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class Client
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public int Id { get; private set; }

        public Client(int id, string name, bool isActive)
        {
            this.Name = name;
            this.IsActive = isActive;
            this.Id = id;
        }

        public void Activate()
        {
            this.IsActive = true;
        }

        public void Deactivate()
        {
            this.IsActive = false;
        }
    }
}