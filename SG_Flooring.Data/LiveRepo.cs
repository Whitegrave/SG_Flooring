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
        public void DeleteOrderFromFile(Order removeMe)
        {
            // Find and remove matching Order
            List<Order> orders = GetOrdersFromFile(removeMe.Date);
            foreach (Order item in orders)
            {
                if (item.Number == removeMe.Number)
                    orders.Remove(item);
            }
            // Rebuild file
            WriteOrdersToFile(removeMe.Date, orders);
        }

        public Order GetOrderFromFile(string OrderDate, int OrderNumber)
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

        public Response SaveOrderToFile(Order saveMe)
        {
            Response savedOrderSuccess = new Response();
            // Check if a file exists for this date
            Response fileExists = CheckFileForDate(saveMe.Date);
            List<Order> ordersToSave = new List<Order>();
            // If not found, create one
            if (!fileExists.Success)
            {
                File.Create(@".\" + saveMe.Date.Replace("/", "") + ".txt");
            }
            else
            // If file was found, populate list from file
            {
                ordersToSave = GetOrdersFromFile(saveMe.Date);
            }

            // If order number is 0, it's a new order, so find next open order index
            int orderNumberToSave = saveMe.Number;
            if (orderNumberToSave == 0)
            {
                int nextOrderNumber = 0;
                int listCount = ordersToSave.Count();

                while (nextOrderNumber < ordersToSave.Count)
                {
                    nextOrderNumber++;
                    bool indexAvailable = true;
                    // Loop through each order in list until an open number is found
                    foreach (Order item in ordersToSave)
                    {
                        if (item.Number == nextOrderNumber)
                            indexAvailable = false;
                    }
                    if (indexAvailable)
                    {
                        // Add order to list as new with updated number
                        saveMe.Number = nextOrderNumber;
                        ordersToSave.Add(saveMe);
                        break;
                    }
                    // On Error
                    else
                    {
                        savedOrderSuccess.Success = false;
                        savedOrderSuccess.Message = $"An index could not be generated for date {saveMe.Date} and number {saveMe.Number}";
                        return savedOrderSuccess;
                    }
                }
            }
            // If Order Number was not 0, it's an existing order (update)
            else
            {
                // Loop through and find matching order, update it
                foreach (Order item in ordersToSave)
                {
                    if (item.Number == saveMe.Number)
                    {
                        ordersToSave[ordersToSave.IndexOf(item)] = saveMe;
                        break;
                    }
                }
            }

            // Re-write file
            WriteOrdersToFile(saveMe.Date, ordersToSave);
            savedOrderSuccess.Success = true;
            savedOrderSuccess.Message = $"Orders saved to date {saveMe.Date}";
            return savedOrderSuccess;
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
                response.Message = "Date provided was not recognized. Use DD/MM/YYYY format. Press any key to continue...";
                return response;
            }

            // Check if file exists for this date
            if (!File.Exists(@".\Orders_" + date.Replace("/", "") + ".txt"))
            {
                response.Date = "invalid";
                response.Success = false;
                response.Message = "No orders exist for this date. Press any key to continue...";
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
            if (!File.Exists(@".\" + validDateResponse.Date.ToString().Replace("/", "") + ".txt"))
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
            // Get order data
            string[] rows = File.ReadAllLines(@".\Orders_" + date.Replace("/", "") + ".txt");
            // Remove header row
            rows = rows.Skip(0).ToArray();

            // Create list for Orders
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

        private void WriteOrdersToFile(string date, List<Order> list)
        {
            using (StreamWriter writer = File.CreateText(@".\" + date.Replace("/", "") + ".txt"))
            {
                // Header
                writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                // Write each order as a concatenated, delimited line
                foreach (Order item in list)
                {
                    writer.WriteLine(string.Join(",", item.Number, item.Customer, item.State, item.TaxRate, item.Product, item.Area, item.CostPSqf, item.LaborPSqf, item.MaterialCostPSqf, item.LaborTotal, item.TaxTotal, item.Total));
                }
            }
        }
    }
}
