using FlooringOrders.Models;
using FlooringOrders.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class OrderTestRepository : IOrderHandler
    {
        private static List<Order> list = new List<Order>()
        {
            new Order
            {
                Area = 200,
                CostPerSquareFoot = 10,
                CustomerName = "Fred",
                LaborCostPerSquareFoot = 2,
                OrderDate = DateTime.Parse("1/1/2000"),
                OrderNumber = 1,
                ProductType = "Wood",
                State = "Ohio",
                TaxRate = 5,

                LaborCost = 400,
                MaterialCost = 2000,
                Tax = 120,
                Total = 2520
            }
        };
        public void Create(Order order)
        {
            list.Add(order);
        }
        public Order RetrieveOne(DateTime date, int orderNumber)
        {
            foreach (Order item in list)
            {
                if (item.OrderNumber == orderNumber)
                {
                    return item;
                }
            }
            return null;
        }
        public List<Order> RetrieveAll(DateTime date)
        {
            return list;
        }
        public void Update(Order order, Order oldOrder)
        {
            Delete(oldOrder);
            Create(order);
        }
        public void Delete(Order order)
        {
            list.Remove(RetrieveOne(order.OrderDate, order.OrderNumber));
        }

        public string OrderFile(DateTime date)
        {
            string path = "Orders_" + date.ToString("MMddyyyy") + ".txt";
            return path;
        }
        public bool Exists(string fileName)
        {
            if (fileName == "Orders_01012000.txt")
            {
                return true;
            }
            return false;
        }
    }
}
