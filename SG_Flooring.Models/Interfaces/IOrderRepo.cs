using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Models.Interfaces
{
    public interface IOrderRepo
    {
        Order GetOrder(string OrderDate, int OrderNumber);
        void SaveOrder(Order saveMe);
        void DeleteOrder(Order removeMe);
    }
}
