using SG_Flooring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Data
{
    public class ProductRepo
    {
        public List<Product> _products;
        private string _path = @".\Products.txt";

        // Populate product list from file
        private void LoadProducts()
        {
            // Get order data
            string[] rows = File.ReadAllLines(_path);
            // Remove header row
            rows = rows.Skip(0).ToArray();

            for (int i = 1; i < rows.Length; i++)
            {
                // Parse delimited lines into array elements
                string[] fields = rows[i].Split(',');
                Product x = new Product();

                // Populate Product fields from array elements
                try
                {
                    x.Name = fields[0];
                    x.CostSqf = decimal.Parse(fields[1]);
                    x.CostLabor = decimal.Parse(fields[2]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error was encountered parsing product file data: \n");
                    Console.WriteLine(String.Concat(e.Message, e.StackTrace));
                    Console.WriteLine("\n\n The repository was reset to default values.");
                    ResetProductRepo();
                }

                // Add Product to list
                _products.Add(x);
            }
        }
        // Method to create a new product repo if desired or in the event of file not able to parse
        private void ResetProductRepo()
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                writer.WriteLine("ProductType,CostPerSquareFoot,LaborCostPerSquareFoot");
                writer.WriteLine("Carpet 3mm,2.25,2.10");
                writer.WriteLine("Carpet 6mm,3.15,2.10");
                writer.WriteLine("Laminate,1.75,2.10");
                writer.WriteLine("Stone Tile,3.50,4.15");
                writer.WriteLine("Paved Tile,4.80,4.15");
                writer.WriteLine("Marble,11.70,4.45");
                writer.WriteLine("Concrete,1.45,1.50");
                writer.WriteLine("Wood,5.15,4.75");
                writer.WriteLine("Cherry,8.00,4.75");
                writer.WriteLine("Walnut,6.00,4.75");
                writer.WriteLine("Oak,7.00,4.75");
                writer.WriteLine("Gold,10000.00,10.00");
            }
        }

        // Initialize product repo if it doesn't currently exist, or if the user requested to
        public void InitializeProductRepo(bool ResetRepo = false)
        {
            if (!File.Exists(_path) || ResetRepo == true)
            {
                ResetProductRepo();
            }
        }
    }
}
