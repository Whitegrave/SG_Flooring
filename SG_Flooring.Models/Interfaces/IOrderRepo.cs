using SG_Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Models.Interfaces
{
    public interface IOrderRepo
    {
        Order GetOrder(string orderDate, int orderNumber);
        void SaveOrder(Order saveMe);
        void DeleteOrder(Order removeMe);
        CheckDateResponse CheckDate(string date);
        CheckDateResponse CheckFileForDate(string date);
    }
}
