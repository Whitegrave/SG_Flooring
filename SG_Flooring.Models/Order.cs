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
        public string Date { get; set; }
        public string Customer { get; set; }
        public string State { get; set; }
        public string Product { get; set; }
        public decimal CostPSqf { get; set; }
        public decimal LaborPSqf { get; set; }
        public decimal MaterialCostPSqf { get; set; }
        public decimal Area { get; set; }
        public decimal LaborTotal { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
    }
}
