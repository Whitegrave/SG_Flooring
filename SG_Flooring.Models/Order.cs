using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Models
{
    public class Order
    {
        public int Number { get; set; }
        public string  Date { get; set; }
        public string Customer { get; set; }
        public string State { get; set; }
        public string Prodcut { get; set; }
        public string Materials { get; set; }
        public decimal Labor { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
