using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class Client
    {
        [Required, Column("ClientName"), MaxLength(255)]
        public string Name { get; private set; }
        [Column("IsActivated")]
        public bool IsActive { get; private set; }
        public int Id { get; set; }

        public Client() { }

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