using FlooringOrders.Data.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class RepoFactory
    {
        private string mode = ConfigurationManager.AppSettings["Mode"].ToString();
        public OrderManager OrderFactory()
        {
            switch (mode)
            {
                case "Test":
                    return new OrderManager(new OrderTestRepository());
                case "File":
                    return new OrderManager(new OrderRepository());
                case "DataBase":
                    throw new NotImplementedException("Components for DataBase not complete. Replace this exception code line when done.");
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
        public ProductManager ProductFactory()
        {
            switch (mode)
            {
                case "Test":
                    return new ProductManager(new ProductTestRepository());
                case "File":
                    return new ProductManager(new ProductRepository());
                case "DataBase":
                    throw new NotImplementedException("Components for DataBase not complete. Replace this exception code line when done.");
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
        public TaxManager TaxFactory()
        {
            switch (mode)
            {
                case "Test":
                    return new TaxManager(new TaxTestRepository());
                case "File":
                    return new TaxManager(new TaxRepository());
                case "DataBase":
                    throw new NotImplementedException("Components for DataBase not complete. Replace this exception code line when done.");
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
