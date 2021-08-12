using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Data
{
    public class ProductRepo : IProductRepo
    {
        public List<Product> _products = new List<Product>();
        private string _path = @".\Products.txt";

        // Constructor to populate product list from file
        public ProductRepo()
        {
            try
            {
                // Get order data
                string[] rows = File.ReadAllLines(_path);
                // Remove header row
                rows = rows.Skip(1).ToArray();

                for (int i = 0; i < rows.Length; i++)
                {
                    // Parse delimited lines into array elements
                    string[] fields = rows[i].Split(',');
                    Product x = new Product();

                    // Populate Product fields from array elements
                    x.Name = fields[0];
                    x.CostSqf = decimal.Parse(fields[1]);
                    x.CostLaborSqf = decimal.Parse(fields[2]);

                    // Add Product to list
                    _products.Add(x);
                }                        
            }
            catch (Exception e)
            {
                Console.WriteLine("An error was encountered parsing product file data: \n");
                Console.WriteLine(String.Concat(e.Message, e.StackTrace));
                Console.ReadKey();
            }
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
    }
}
