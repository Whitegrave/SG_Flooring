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
        Order GetOrderFromFile(string orderDate, int orderNumber);
        Response SaveOrderToFile(Order saveMe);
        void DeleteOrderFromFile(Order removeMe);
        CheckDateResponse CheckDate(string date);
        CheckDateResponse CheckFileForDate(string date);
    }
}
