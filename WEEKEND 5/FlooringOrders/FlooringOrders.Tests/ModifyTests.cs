using FlooringOrders.BLL;
using FlooringOrders.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Tests
{
    [TestFixture]
    public class ModifyTests
    {
        [TestCase("Cheese", false)]
        [TestCase("1/1/1998", false)]
        [TestCase("1/1/21", true)]
        public void CanUseNewOrderDate(string input, bool expectedResult)
        {
            ModifyOrderRule rule = new ModifyOrderRule();
            Response response = rule.Date(input);
            Assert.AreEqual(response.Success, expectedResult);
        }

        [TestCase("", false)]
        [TestCase("Fred", true)]
        [TestCase("Jeeves, Inc.", true)]
        public void CanUseNewCustomerName(string input, bool expectedResult)
        {
            ModifyOrderRule rule = new ModifyOrderRule();
            Response response = rule.CustomerName(input);
            Assert.AreEqual(response.Success, expectedResult);
        }

        [TestCase("Jonathan", false)]
        [TestCase("Ohio", false)]
        [TestCase("OH", true)]
        public void CanGetExistingStateByAbbreviation(string input, bool expectedResult)
        {
            ModifyOrderRule rule = new ModifyOrderRule();
            Response response = rule.ExistingState(input);
            Assert.AreEqual(response.Success, expectedResult);
        }

        [TestCase("Ohio", false)]
        [TestCase("Wood", true)]
        public void CanGetExistingProduct(string input, bool expectedResult)
        {
            ModifyOrderRule rule = new ModifyOrderRule();
            Response response = rule.ExistingProduct(input);
            Assert.AreEqual(response.Success, expectedResult);
        }

        [TestCase("Iron Man", false)]
        [TestCase("3", false)]
        [TestCase("100", true)]
        public void CanUseNewArea(string input, bool expectedResult)
        {
            ModifyOrderRule rule = new ModifyOrderRule();
            Response response = rule.Area(input);
            Assert.AreEqual(response.Success, expectedResult);
        }

        [Test]
        public void CanPerformOrderAssembly()
        {
            ModifyOrderRule rule = new ModifyOrderRule();
            Order order = new Order()
            {
                Area = 200,
                CostPerSquareFoot = 10,
                CustomerName = "Fred",
                LaborCostPerSquareFoot = 2,
                OrderDate = DateTime.Parse("1/1/2021"),
                OrderNumber = 1,
                ProductType = "Wood",
                State = "Ohio",
                TaxRate = 5,

                LaborCost = 400,
                MaterialCost = 2000,
                Tax = 120,
                Total = 2520
            };

            Order testMeOrder = rule.AssembleOrder(order.OrderDate.ToString(), order.CustomerName,
                                rule.RetrieveOneByFull(order.State),
                                order.ProductType, order.Area.ToString(), order.OrderNumber);

            Assert.AreEqual(order.Area, testMeOrder.Area);
            Assert.AreEqual(order.CostPerSquareFoot, testMeOrder.CostPerSquareFoot);
            Assert.AreEqual(order.CustomerName, testMeOrder.CustomerName);
            Assert.AreEqual(order.LaborCost, testMeOrder.LaborCost);
            Assert.AreEqual(order.LaborCostPerSquareFoot, testMeOrder.LaborCostPerSquareFoot);
            Assert.AreEqual(order.MaterialCost, testMeOrder.MaterialCost);
            Assert.AreEqual(order.OrderDate, testMeOrder.OrderDate);
            Assert.AreEqual(order.OrderNumber, testMeOrder.OrderNumber);
            Assert.AreEqual(order.ProductType, testMeOrder.ProductType);
            Assert.AreEqual(order.State, testMeOrder.State);
            Assert.AreEqual(order.Tax, testMeOrder.Tax);
            Assert.AreEqual(order.TaxRate, testMeOrder.TaxRate);
            Assert.AreEqual(order.Total, testMeOrder.Total);
        }

        [TestCase("", "Guy", "CA", "Iron", "100")]
        [TestCase("1/1/31", "", "CA", "Iron", "100")]
        [TestCase("1/1/31", "Guy", "", "Iron", "100")]
        [TestCase("1/1/31", "Guy", "CA", "", "100")]
        [TestCase("1/1/31", "Guy", "CA", "Iron", "")]
        [TestCase("", "Guy", "", "", "")]
        public void CanPerformUpdatedOrderAssembly(string parseDate, string customerName, string stateAbbreviation, string type, string parseArea)
        {
            ModifyOrderRule rule = new ModifyOrderRule();
            Order order = new Order()
            {
                Area = 200,
                CostPerSquareFoot = 10,
                CustomerName = "Fred",
                LaborCostPerSquareFoot = 2,
                OrderDate = DateTime.Parse("1/1/2021"),
                OrderNumber = 1,
                ProductType = "Wood",
                State = "Ohio",
                TaxRate = 5,

                LaborCost = 400,
                MaterialCost = 2000,
                Tax = 120,
                Total = 2520
            };

            Order testMeOrder = rule.AssembleUpdatedOrder(parseDate, customerName, stateAbbreviation, type, parseArea, 1, order);
            if (string.IsNullOrEmpty(parseDate))
            {
                Assert.AreEqual(testMeOrder.OrderDate, order.OrderDate);
            }
            else
            {
                Assert.AreNotEqual(testMeOrder.OrderDate, order.OrderDate);
            }
            if (string.IsNullOrEmpty(customerName))
            {
                Assert.AreEqual(testMeOrder.CustomerName, order.CustomerName);
            }
            else
            {
                if (string.IsNullOrEmpty(parseDate) && string.IsNullOrEmpty(stateAbbreviation) && string.IsNullOrEmpty(type) && string.IsNullOrEmpty(parseArea))
                {
                    Assert.AreEqual(testMeOrder.LaborCost, order.LaborCost);
                    Assert.AreEqual(testMeOrder.MaterialCost, order.MaterialCost);
                    Assert.AreEqual(testMeOrder.Tax, order.Tax);
                    Assert.AreEqual(testMeOrder.Total, order.Total);
                }
                Assert.AreNotEqual(testMeOrder.CustomerName, order.CustomerName);
            }
            if (string.IsNullOrEmpty(stateAbbreviation))
            {
                Assert.AreEqual(testMeOrder.State, order.State);
            }
            else
            {
                Assert.AreNotEqual(testMeOrder.State, order.State);
            }
            if (string.IsNullOrEmpty(type))
            {
                Assert.AreEqual(testMeOrder.ProductType, order.ProductType);
            }
            else
            {
                Assert.AreNotEqual(testMeOrder.ProductType, order.ProductType);
            }
            if (string.IsNullOrEmpty(parseArea))
            {
                Assert.AreEqual(testMeOrder.Area, order.Area);
            }
            else
            {
                Assert.AreNotEqual(testMeOrder.Area, order.Area);
            }
        }
    }
}
