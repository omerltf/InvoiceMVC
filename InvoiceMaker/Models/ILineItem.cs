using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Models
{
    public interface ILineItem
    {
        decimal Amount { get;}
        string Description { get;}
        DateTimeOffset When { get;}
    }
}
