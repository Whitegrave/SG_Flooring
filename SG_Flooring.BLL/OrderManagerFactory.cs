using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TEST":
                    return new OrderManager(new FreeAccountTestRepository());
                case "LIVE":
                    return new OrderManager(new BasicAccountTestRepository());
                default:
                    throw new Exception("Mode value in app config was invalid");
            }
        }
    }
}
