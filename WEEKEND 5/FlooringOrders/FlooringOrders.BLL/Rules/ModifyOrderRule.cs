using FlooringOrders.Data;
using FlooringOrders.Data.Managers;
using FlooringOrders.Models;
using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL
{
    public class ModifyOrderRule
    {
        public Response Date(string parseMe)
        {
            Response response = new Response()
            {
                Success = false
            };
            if (!DateTime.TryParse(parseMe, out DateTime date))
            {
                response.Message = "Failed; Input is not a date.";
                return response;
            }
            if (date <= DateTime.Today)
            {
                response.Message = "Failed; Date isn't in the future.";
                return response;
            }
            response.Success = true;
            return response;
        }
        public Response CustomerName(string nameInput)
        {
            Response response = new Response()
            {
                Success = false
            };
            if (string.IsNullOrEmpty(nameInput))
            {
                response.Message = "Failed; Name cannot be empty.";
                return response;
            }
            if (string.IsNullOrEmpty(nameInput))
            {
                response.Message = "Failed; Name cannot contain ''::''.";
                return response;
            }
            response.Success = true;
            return response;
        }
        public Response ExistingState(string stateAbbreviation)
        {
            Response response = new Response()
            {
                Success = false
            };
            TaxManager manager = new RepoFactory().TaxFactory();
            if (manager.RetrieveOne(stateAbbreviation) == null)
            {
                response.Message = "Failed; State not on file.";
                return response;
            }
            response.Success = true;
            return response;
        }
        public Response ExistingProduct(string type)
        {
            Response response = new Response()
            {
                Success = false
            };
            ProductManager manager = new RepoFactory().ProductFactory();
            if (manager.RetrieveOne(type) == null)
            {
                response.Message = "Failed; Product not on file.";
                return response;
            }
            response.Success = true;
            return response;
        }
        public Response Area(string input)
        {
            decimal x;
            Response response = new Response()
            {
                Success = false
            };
            if (!decimal.TryParse(input, out x))
            {
                response.Message = "Failed; Input is not a number.";
                return response;
            }
            if (x < 100)
            {
                response.Message = "Failed; Area is smaller than minimum order size.";
                return response;
            }
            response.Success = true;
            return response;
        }

        public Order AssembleOrder(string parseDate, string customerName, string stateAbbreviation, string type, string parseArea, int? newOrderNumber)
        {
            OrderManager repository = new RepoFactory().OrderFactory();

            //Check date
            Response response = Date(parseDate);
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }
            DateTime date = DateTime.Parse(parseDate);

            //Check customer name
            response = CustomerName(customerName);
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }

            //Check existing state
            response = ExistingState(stateAbbreviation);
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }
            TaxManager manager = new RepoFactory().TaxFactory();
            Tax tax = manager.RetrieveOne(stateAbbreviation);

            //Check existing product
            response = ExistingProduct(type);
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }
            ProductManager Pmanager = new RepoFactory().ProductFactory();
            Product product = Pmanager.RetrieveOne(type);

            //Check area
            response = Area(parseArea);
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }
            decimal area = decimal.Parse(parseArea);

            List<Order> list = repository.RetrieveAll(date);

            //Generate order based on all the data
            Order order = new Order
            {
                Area = area,
                CostPerSquareFoot = product.CostPerSquareFoot,
                CustomerName = customerName,
                LaborCostPerSquareFoot = product.LaborCostPerSquareFoot,
                OrderDate = date,
                ProductType = product.ProductType,
                State = tax.StateName,
                TaxRate = tax.TaxRate,
            };
            if (newOrderNumber == null && list.Count > 0)
            {
                order.OrderNumber = (list.Max(x => x.OrderNumber) + 1);
            }
            else
            {
                order.OrderNumber = 1;
            }

            order.MaterialCost = (order.Area * order.CostPerSquareFoot);
            order.LaborCost = (order.Area * order.LaborCostPerSquareFoot);
            order.Tax = ((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100));
            order.Total = (order.MaterialCost + order.LaborCost + order.Tax);

            return order;
        }

        public Order AssembleUpdatedOrder(string parseDate, string customerName, string stateAbbreviation, string type, string parseArea, int? newOrderNumber, Order oldOrder)
        {
            OrderManager repository = new RepoFactory().OrderFactory();
            Response response = new Response();

            //Check date
            DateTime date = new DateTime();
            if (!string.IsNullOrEmpty(parseDate))
            {
                response = Date(parseDate);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                date = DateTime.Parse(parseDate);
            }
            else
            {
                date = oldOrder.OrderDate;
            }

            //Check customer name
            if (!string.IsNullOrEmpty(customerName))
            {
                response = CustomerName(customerName);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
            }
            else
            {
                customerName = oldOrder.CustomerName;
            }

            //Check existing state
            Tax tax = new Tax();
            if (!string.IsNullOrEmpty(stateAbbreviation))
            {
                response = ExistingState(stateAbbreviation);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                TaxManager manager = new RepoFactory().TaxFactory();
                tax = manager.RetrieveOne(stateAbbreviation);
            }
            else
            {
                tax.StateName = oldOrder.State;
                tax.TaxRate = oldOrder.TaxRate;
            }

            //Check existing product
            Product product = new Product();
            if (!string.IsNullOrEmpty(type))
            {
                response = ExistingProduct(type);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                ProductManager Pmanager = new RepoFactory().ProductFactory();
                product = Pmanager.RetrieveOne(type);
            }
            else
            {
                product.CostPerSquareFoot = oldOrder.CostPerSquareFoot;
                product.LaborCostPerSquareFoot = oldOrder.LaborCostPerSquareFoot;
                product.ProductType = oldOrder.ProductType;
            }

            //Check area
            decimal area;
            if (!string.IsNullOrEmpty(parseArea))
            {
                response = Area(parseArea);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                area = decimal.Parse(parseArea);

                
            }
            else
            {
                area = oldOrder.Area;
            }

            List<Order> list = repository.RetrieveAll(date);

            //Generate order based on all the data
            Order order = new Order
            {
                Area = area,
                CostPerSquareFoot = product.CostPerSquareFoot,
                CustomerName = customerName,
                LaborCostPerSquareFoot = product.LaborCostPerSquareFoot,
                OrderDate = date,
                ProductType = product.ProductType,
                State = tax.StateName,
                TaxRate = tax.TaxRate,
            };
            order.OrderNumber = oldOrder.OrderNumber;

            if (string.IsNullOrEmpty(parseDate) && string.IsNullOrEmpty(type) && string.IsNullOrEmpty(parseArea) && string.IsNullOrEmpty(stateAbbreviation))
            {
                order.MaterialCost = oldOrder.MaterialCost;
                order.LaborCost = oldOrder.LaborCost;
                order.Tax = oldOrder.Tax;
                order.Total = oldOrder.Total;
            }
            else
            {
                order.MaterialCost = (order.Area * order.CostPerSquareFoot);
                order.LaborCost = (order.Area * order.LaborCostPerSquareFoot);
                order.Tax = ((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100));
                order.Total = (order.MaterialCost + order.LaborCost + order.Tax);
            }

            return order;
        }

        ///CRUD Functions
        public void Create(Order order)
        {
            OrderManager repository = new RepoFactory().OrderFactory();

            AssembleOrder(order.OrderDate.ToString(), order.CustomerName,
                          RetrieveOneByFull(order.State),
                          order.ProductType, order.Area.ToString(), order.OrderNumber);
            repository.Create(order);
        }
        public void Update(Order oldOrder, Order newOrder)
        {
            OrderManager repository = new RepoFactory().OrderFactory();

            AssembleOrder(oldOrder.OrderDate.ToString(), oldOrder.CustomerName,
                          RetrieveOneByFull(oldOrder.State),
                          oldOrder.ProductType, oldOrder.Area.ToString(), oldOrder.OrderNumber);

            AssembleUpdatedOrder(newOrder.OrderDate.ToString(), newOrder.CustomerName,
                          RetrieveOneByFull(newOrder.State),
                          newOrder.ProductType, newOrder.Area.ToString(), newOrder.OrderNumber, oldOrder);

            repository.Update(newOrder, oldOrder);
        }
        public void Delete(Order order)
        {
            OrderManager repository = new RepoFactory().OrderFactory();

            AssembleOrder(order.OrderDate.ToString(), order.CustomerName,
                          RetrieveOneByFull(order.State),
                          order.ProductType, order.Area.ToString(), order.OrderNumber);
            repository.Delete(order);
        }
        public List<Product> RetrieveAllProducts()
        {
            ProductManager manager = new RepoFactory().ProductFactory();
            return manager.RetrieveAll();
        }
        public List<Tax> RetrieveAllTaxes()
        {
            TaxManager manager = new RepoFactory().TaxFactory();
            return manager.RetrieveAll();
        }

        //Just for tax
        public string RetrieveOneByFull(string state)
        {
            TaxManager manager = new RepoFactory().TaxFactory();
            return manager.RetrieveOneByFull(state).StateAbbreviation;
        }
    }
}
