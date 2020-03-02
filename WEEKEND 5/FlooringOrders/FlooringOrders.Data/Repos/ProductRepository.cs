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
    public class ProductRepository : IProductHandler
    {
        private static string path = "Products.txt";
        public static List<Product> ReadProductFromFile()
        {
            List<Product> list = new List<Product>();
            list.Clear();
            foreach (string item in File.ReadAllLines(path))
            {
                string[] set = item.Split(',');
                if (set[1] != "CostPerSquareFoot")
                {
                    Product product = new Product
                    {
                        ProductType = set[0],
                        CostPerSquareFoot = decimal.Parse(set[1]),
                        LaborCostPerSquareFoot = decimal.Parse(set[2])
                    };
                    list.Add(product);
                }
            }
            return list;
        }
        public Product RetrieveOne(string type)
        {
            List<Product> list = ReadProductFromFile();

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
            List<Product> list = ReadProductFromFile();

            return list;
        }
    }
}
