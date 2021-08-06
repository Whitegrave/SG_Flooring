using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using SG_Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            // Populate order list for given date
            List<Order> orders = GetOrdersFromFile(OrderDate);

            // Scan orders for matching number
            foreach (Order x in orders)
            {
                if (x.Number == OrderNumber)
                    return x;
            }

            // No match found
            return null;

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
            response.Date = dateOut.ToString("MM/dd/yyyy");
            response.Success = true;
            response.Message = "Date provided was valid";

            return response;
        }

        public CheckDateResponse CheckFileForDate(string date)
        {
            CheckDateResponse response = new CheckDateResponse();
            CheckDateResponse validDateResponse = new CheckDateResponse();

            validDateResponse = CheckDate(date);      

            // Return if invalid date
            if (!validDateResponse.Success)
                return validDateResponse;

            // Check for file with matching date parameter
            if (!File.Exists(@".\" + validDateResponse.Date.ToString()))
            {
                response.Date = "invalid";
                response.Success = false;
                response.Message = $"No orders found for date {validDateResponse.Date}.";
                return response;
            }

            // Date provided was valid and a file matching its date exists
            response.Date = validDateResponse.Date.ToString();
            response.Success = true;
            response.Message = "Date provided was valid and file found";

            return response;
        }

        private List<Order> GetOrdersFromFile(string date)
        {
            // Get account data
            string[] rows = File.ReadAllLines(@".\Orders_" + date.Replace("/", "") + ".txt");
            // Remove header row
            rows = rows.Skip(0).ToArray();

            // Create list for Accounts
            List<Order> orders = new List<Order>();

            for (int i = 1; i < rows.Length; i++)
            {
                // Parse delimited lines into items
                string[] fields = rows[i].Split(',');
                Order x = new Order();

                // Populate Order fields
                try
                {
                    x.Number = int.Parse(fields[0]);
                    x.Customer = fields[1];
                    x.State = fields[2];
                    x.TaxRate = decimal.Parse(fields[3]);
                    x.Product = fields[4];
                    x.Area = decimal.Parse(fields[5]);
                    x.CostPSqf = decimal.Parse(fields[6]);
                    x.LaborPSqf = decimal.Parse(fields[7]);
                    x.MaterialCostPSqf = decimal.Parse(fields[8]);
                    x.LaborTotal = decimal.Parse(fields[9]);
                    x.TaxTotal = decimal.Parse(fields[10]);
                    x.Total = decimal.Parse(fields[11]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error was encountered parsing order file data: \n");
                    Console.WriteLine(String.Concat(e.Message, e.StackTrace));
                    Console.WriteLine("\n\n The repository was reset to default values.");
                    //CreateLiveRepo();
                }

                // Add Order to list
                orders.Add(x);
            }
            return orders.ToList();
        }
    }
}
