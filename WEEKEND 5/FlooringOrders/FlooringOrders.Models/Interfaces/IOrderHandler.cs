using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models.Interfaces
{
    public interface IOrderHandler
    {
        void Create(Order order);
        Order RetrieveOne(DateTime date, int orderNumber);
        List<Order> RetrieveAll(DateTime date);
        void Update(Order order, Order oldOrder);
        void Delete(Order order);
        string OrderFile(DateTime date);
        bool Exists(string orderFile);
    }
}
