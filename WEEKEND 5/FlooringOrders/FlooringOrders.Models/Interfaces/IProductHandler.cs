using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models.Interfaces
{
    public interface IProductHandler
    {
        Product RetrieveOne(string type);
        List<Product> RetrieveAll();
    }
}
