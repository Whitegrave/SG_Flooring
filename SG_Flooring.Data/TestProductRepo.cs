using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Data
{
    public class TestProductRepo : IProductRepo
    {
        List<Product> _prodRepo;

        // Constructor to populate products
        public TestProductRepo()
        {
            _prodRepo.Add(new Product { Name = "Carpet 3mm", CostSqf = 2.25M, CostLaborSqf = 2.10M });
            _prodRepo.Add(new Product { Name = "Carpet 5mm", CostSqf = 3.15M, CostLaborSqf = 2.10M });
            _prodRepo.Add(new Product { Name = "Laminate", CostSqf = 1.75M, CostLaborSqf = 2.10M });
            _prodRepo.Add(new Product { Name = "Stone Tile", CostSqf = 3.50M, CostLaborSqf = 4.15M });
            _prodRepo.Add(new Product { Name = "Paved Tile", CostSqf = 4.80M, CostLaborSqf = 4.15M });
            _prodRepo.Add(new Product { Name = "Marble", CostSqf = 11.70M, CostLaborSqf = 4.45M });
            _prodRepo.Add(new Product { Name = "Concrete", CostSqf = 1.45M, CostLaborSqf = 1.50M });
            _prodRepo.Add(new Product { Name = "Wood", CostSqf = 5.15M, CostLaborSqf = 4.75M });
            _prodRepo.Add(new Product { Name = "Cherry", CostSqf = 8.00M, CostLaborSqf = 4.75M });
            _prodRepo.Add(new Product { Name = "Walnut", CostSqf = 6.00M, CostLaborSqf = 4.75M });
            _prodRepo.Add(new Product { Name = "Oak", CostSqf = 7.00M, CostLaborSqf = 4.75M });
        }

        public List<Product> GetProducts()
        {
            return _prodRepo;
        }
    }
}
