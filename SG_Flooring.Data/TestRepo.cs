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
        private List<Order> orders = new List<Order>();

        // Constructor to populate test data
        public TestRepo()
            {
            Order orderOne = new Order
            {
                Number = 1,
                Date = "01/01/2001",
                Customer = "AllOnes",
                State = "CA",
                Product = "Wood",
                CostPSqf = 1.00M,
                LaborPSqf = 1.00M,
                MaterialCostTotal = 1.00M,
                Area = 100.00M,
                LaborTotal = 100.00M,
                TaxRate = 0.10M,
                TaxTotal = 10.00M,
                Total = 100.00M
            };
            Order orderTwo = new Order
            {
                Number = 2,
                Date = "01/01/2001",
                Customer = "AllTwos",
                State = "CA",
                Product = "Wood",
                CostPSqf = 2.00M,
                LaborPSqf = 2.00M,
                MaterialCostTotal = 2.00M,
                Area = 200.00M,
                LaborTotal = 200.00M,
                TaxRate = 0.20M,
                TaxTotal = 20.00M,
                Total = 100.00M
            };
            Order orderThree = new Order
            {
                Number = 3,
                Date = "01/01/2001",
                Customer = "AllThrees",
                State = "CA",
                Product = "Wood",
                CostPSqf = 3.00M,
                LaborPSqf = 3.00M,
                MaterialCostTotal = 3.00M,
                Area = 300.00M,
                LaborTotal = 300.00M,
                TaxRate = 0.30M,
                TaxTotal = 30.00M,
                Total = 300.00M
            };
            orders.Add(orderOne);
            orders.Add(orderTwo);
            orders.Add(orderThree);

        }

        public void DeleteOrderFromFile(Order removeMe)
        {
            foreach (Order item in orders)
            {
                if (item.Number == removeMe.Number)
                {
                    orders.Remove(item);
                    return;
                }
            }
        }

        public Order GetOrderFromFile(string OrderDate, int OrderNumber)
        {
            // Test repo uses only 01/01/2001
            if (OrderDate != "01/01/2001")
                return null;

            foreach (Order item in orders)
            {
                if (item.Number == OrderNumber)
                    return item;
            }
            return null;
        }

        public Response SaveOrderToFile(Order saveMe)
        {
            Response savedResponse = new Response();
            foreach (Order item in orders)
            {
                // Replace existing item if exists
                if (item.Number == saveMe.Number)
                {
                    savedResponse.Success = true;
                    orders[orders.IndexOf(item)] = saveMe;
                    return savedResponse;
                }
            }
            // Add new item if it wasn't found
            orders.Add(saveMe);
            savedResponse.Success = true;
            return savedResponse;
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
            // Only one date used in test repo
            CheckDateResponse response = new CheckDateResponse();
            response.Message = "Date found in test repo";
            response.Success = true;

            if (date != "01/01/2001")
            {
                response.Message = "Test Repo uses only 01/01/2001";
                response.Success = false;
            }
            return response;
        }

    }
}
