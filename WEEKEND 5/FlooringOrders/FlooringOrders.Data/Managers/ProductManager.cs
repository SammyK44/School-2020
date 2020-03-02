using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data.Managers
{
    public class ProductManager : IProductHandler
    {
        private IProductHandler Handler;

        public ProductManager(IProductHandler handler)
        {
            Handler = handler;
        }

        public List<Product> RetrieveAll()
        {
            return Handler.RetrieveAll();
        }

        public Product RetrieveOne(string type)
        {
            return Handler.RetrieveOne(type);
        }
    }
}
