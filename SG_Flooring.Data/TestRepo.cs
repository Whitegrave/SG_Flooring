using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using SG_Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Data
{
    public class TestRepo : IOrderRepo
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

        public CheckDateResponse CheckDate(string date)
        {
            CheckDateResponse response = new CheckDateResponse();
            DateTime dateOut;

            // Attempt to parse string into date
            bool dateSuccess = DateTime.TryParse(date, out dateOut);

            // If conversion failed, return false/msg
            if (!dateSuccess)
            {
                response.Date = "invalid";
                response.Success = false;
                response.Message = "Date provided was not recognized. Use DD/MM/YYYY format.";
                return response;
            }

            // Date provided was valid. Convert string to consistent format, store
            response.Date = dateOut.ToString("dd/MM/yyyy");
            response.Success = true;
            response.Message = "Date provided was valid";

            return response;
        }

        public CheckDateResponse CheckFileForDate(string date)
        {
            throw new NotImplementedException();
        }
    }
}
