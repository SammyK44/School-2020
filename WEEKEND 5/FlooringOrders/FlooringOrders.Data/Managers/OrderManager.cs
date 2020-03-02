using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrders.Models;
using FlooringOrders.Models.Interfaces;

namespace FlooringOrders.Data.Managers
{
    public class OrderManager : IOrderHandler
    {
        private IOrderHandler Handler;
        public OrderManager(IOrderHandler handler)
        {
            Handler = handler;
        }

        public void Create(Order order)
        {
            Handler.Create(order);
        }

        public void Delete(Order order)
        {
            Handler.Delete(order);
        }

        public List<Order> RetrieveAll(DateTime date)
        {
            return Handler.RetrieveAll(date);
        }

        public Order RetrieveOne(DateTime date, int orderNumber)
        {
            return Handler.RetrieveOne(date, orderNumber);
        }

        public void Update(Order order, Order oldOrder)
        {
            Handler.Update(order, oldOrder);
        }
        public string OrderFile(DateTime date)
        {
            return Handler.OrderFile(date);
        }

        public bool Exists(string orderFile)
        {
            return Handler.Exists(orderFile);
        }
    }
}
