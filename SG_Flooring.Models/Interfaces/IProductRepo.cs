using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Models.Interfaces
{
    public interface IProductRepo
    {
        List<Product> GetProducts();
    }
}
