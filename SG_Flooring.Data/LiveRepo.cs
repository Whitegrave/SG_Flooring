using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Data
{
    public class LiveRepo : IOrderRepo
    {
        public void DeleteOrder(Order removeMe)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(string OrderDate, int OrderNumber)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order saveMe)
        {
            throw new NotImplementedException();
        }
    }
}
