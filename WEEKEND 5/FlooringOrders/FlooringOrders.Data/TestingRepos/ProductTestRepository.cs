using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class ProductTestRepository : IProductHandler
    {
        private static List<Product> list = new List<Product>()
        {
            new Product
            {
                ProductType = "Wood",
                CostPerSquareFoot = 10,
                LaborCostPerSquareFoot = 2
            },
            new Product
            {
                ProductType = "Iron",
                CostPerSquareFoot = 10,
                LaborCostPerSquareFoot = 2
            }
        };
        public Product RetrieveOne(string type)
        {
            foreach (Product item in list)
            {
                if (type == item.ProductType)
                {
                    return item;
                }
            }
            return null;
        }
        public List<Product> RetrieveAll()
        {
            return list;
        }
    }
}
