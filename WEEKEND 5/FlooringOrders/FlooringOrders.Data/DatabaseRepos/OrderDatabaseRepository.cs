using FlooringOrders.Models;
using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class OrderDatabaseRepository : IOrderHandler
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

        }
        private List<Order> ReadFromFile(DateTime date)
        {
            List<Order> list = new List<Order>();

            string queryString = "SELECT Date, Number, Name, StateName, Type, Area " +
                                 "FROM dbo.Orders;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderDate = date,
                            OrderNumber = int.Parse(reader[0].ToString()),
                            CustomerName = reader[1].ToString(),
                            State = reader[2].ToString(),
                            TaxRate = decimal.Parse(reader[3].ToString()),
                            ProductType = reader[4].ToString(),
                            Area = decimal.Parse(reader[5].ToString()),
                        };

                        //COMPLETE TAX REPOSITORY
                        foreach (Tax item in TaxDatabaseRepository.ReadTaxFromFile())
                        {
                            if (item.StateName == order.State)
                            {
                                item.TaxRate = order.TaxRate;
                                break;
                            }
                        }

                        //COMPLETE PRODUCT REPOSITORY
                        foreach (Product item in ProductDatabaseRepository.ReadProductFromFile())
                        {
                            if (item.ProductType == order.ProductType)
                            {
                                item.CostPerSquareFoot = order.CostPerSquareFoot;
                                item.LaborCostPerSquareFoot = order.LaborCostPerSquareFoot;
                                break;
                            }
                        }
                        
                        //Calculate & save the other info (MaterialCost, LaborCost, Tax, Total)

                        list.Add(order);
                    }
                }
                return list;
            }  
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
