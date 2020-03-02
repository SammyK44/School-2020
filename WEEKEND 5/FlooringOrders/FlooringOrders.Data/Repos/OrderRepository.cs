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
    public class OrderRepository : IOrderHandler
    {
        public string OrderFile(DateTime date)
        {
            string path = "Orders_" + date.ToString("MMddyyyy") + ".txt";
            return path;
        }
        private string ToString(Order order)
        {
            return $"{order.OrderNumber}::" +
                   $"{order.CustomerName}::" +
                   $"{order.State}::" +
                   $"{order.TaxRate}::" +
                   $"{order.ProductType}::" +
                   $"{order.Area}::" +
                   $"{order.CostPerSquareFoot}::" +
                   $"{order.LaborCostPerSquareFoot}::" +
                   $"{order.MaterialCost}::" +
                   $"{order.LaborCost}::" +
                   $"{order.Tax}::" +
                   $"{order.Total}";
        }
        private void WriteToFile(DateTime date, List<Order> list)
        {
            if (!File.Exists(OrderFile(date)))
            {
                File.Create(OrderFile(date));
            }
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(OrderFile(date));
                writer.WriteLine("OrderNumber::CustomerName::State::TaxRate::ProductType::Area::CostPerSquareFoot::LaborCostPerSquareFoot::MaterialCost::LaborCost::Tax::Total");
                foreach (Order order in list)
                {
                    writer.WriteLine(ToString(order));
                }
            }
            finally
            {
                writer.Close();
            }
        }
        private List<Order> ReadFromFile(DateTime date)
        {
            string[] set;
            List<Order> list = new List<Order>();
            if (File.Exists(OrderFile(date)))
            {
                set = File.ReadAllLines(OrderFile(date));
            }
            else
            {
                return list;
            }
            foreach (string line in set)
            {
                string[] split = line.Split(new string[] { "::" }, StringSplitOptions.None);
                List<string> temp = new List<string>();
                if (split[0] != "OrderNumber")
                {
                    foreach (string item in split)
                    {
                        temp.Add(item);
                    }
                    
                    Order order = new Order
                    {
                        OrderDate = date,
                        OrderNumber = int.Parse(temp[0]),
                        CustomerName = temp[1],
                        State = temp[2],
                        TaxRate = decimal.Parse(temp[3]),
                        ProductType = temp[4],
                        Area = decimal.Parse(temp[5]),
                        CostPerSquareFoot = decimal.Parse(temp[6]),
                        LaborCostPerSquareFoot = decimal.Parse(temp[7]),
                        MaterialCost = decimal.Parse(temp[8]),
                        LaborCost = decimal.Parse(temp[9]),
                        Tax = decimal.Parse(temp[10]),
                        Total = decimal.Parse(temp[11])
                    };
                    list.Add(order);
                }
            }
            return list;
        }
        public void Create(Order order)
        {
            List<Order> list = ReadFromFile(order.OrderDate);
            list.Add(order);
            WriteToFile(order.OrderDate, list);
        }
        public Order RetrieveOne(DateTime date, int orderNumber)
        {
            List<Order> list = ReadFromFile(date);
            foreach (Order item in list)
            {
                ReadFromFile(item.OrderDate);
                if (item.OrderNumber == orderNumber)
                {
                    return item;
                }
            }
            return null;
        }
        public List<Order> RetrieveAll(DateTime date)
        {
            List<Order> list = ReadFromFile(date);
            return list;
        }
        public void Update(Order order, Order oldOrder)
        {
            List<Order> list = ReadFromFile(order.OrderDate);
            foreach (Order item in list)
            {
                if (item.OrderNumber == oldOrder.OrderNumber)
                {
                    item.Area = order.Area;
                    item.CostPerSquareFoot = order.CostPerSquareFoot;
                    item.CustomerName = order.CustomerName;
                    item.LaborCost = order.LaborCost;
                    item.LaborCostPerSquareFoot = order.LaborCostPerSquareFoot;
                    item.MaterialCost = order.MaterialCost;
                    item.OrderDate = order.OrderDate;
                    item.OrderNumber = order.OrderNumber;
                    item.ProductType = order.ProductType;
                    item.State = order.State;
                    item.Tax = order.Tax;
                    item.TaxRate = order.TaxRate;
                    item.Total = order.Total;
                    break;
                }
            }
            WriteToFile(order.OrderDate, list);
        }
        public void Delete(Order order)
        {
            List<Order> list = ReadFromFile(order.OrderDate);
            foreach (Order item in list)
            {
                if (item.OrderNumber == order.OrderNumber)
                {
                    list.Remove(item);
                    break;
                }
            }
            WriteToFile(order.OrderDate, list);
        }

        public bool Exists(string orderFile)
        {
            if (File.Exists(orderFile))
            {
                return true;
            }
            return false;
        }
    }
}
