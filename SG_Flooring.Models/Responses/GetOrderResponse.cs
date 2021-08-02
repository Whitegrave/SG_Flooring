using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Models.Responses
{
    public class GetOrderResponse : Response
    {
        public Order order { get; set; }
    }
}
