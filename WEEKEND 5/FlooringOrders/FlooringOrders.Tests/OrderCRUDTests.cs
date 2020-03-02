using FlooringOrders.Data;
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
    public class OrderCRUDTests
    {
        //DONE

        [Test]
        public void CanCreate()
        {
            Order order = new Order();

            bool valid = false;

            OrderTestRepository repo = new OrderTestRepository();
            int initialTotal = repo.RetrieveAll(DateTime.Today).Count;
            repo.Create(order);
            int newTotal = repo.RetrieveAll(DateTime.Today).Count;

            if (initialTotal < newTotal && (initialTotal + 1) == newTotal)
            {
                valid = true;
            }
            Assert.AreEqual(valid, true);
        }

        [TestCase(false, 1, false)]
        [TestCase(true, 3, false)]
        [TestCase(true, 1, true)]
        public void CanGetOne(bool orderDateSwitch, int orderNumber, bool expectedResult)
        {
            DateTime date = DateTime.Today;
            if (orderDateSwitch)
            {
                date = DateTime.Parse("1/1/2000");
            }

            bool result = true;
            OrderTestRepository repo = new OrderTestRepository();

            if (repo.RetrieveOne(date, orderNumber) == null)
            {
                result = false;
            }
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void CanUpdate()
        {
            OrderTestRepository repo = new OrderTestRepository();
            Order initialOrder = repo.RetrieveOne(DateTime.Parse("1/1/2000"), 1);
            repo.Update(new Order { CustomerName = "Henrietta", OrderNumber = 1 }, initialOrder);
            Order newOrder = repo.RetrieveOne(DateTime.Parse("1/1/2000"), 1);

            Assert.AreNotEqual(initialOrder.CustomerName, newOrder.CustomerName);
        }

        [Test]
        public void CanDelete()
        {
            OrderTestRepository repo = new OrderTestRepository();
            int initialTotal = repo.RetrieveAll(DateTime.Today).Count;
            repo.Delete(new Order { OrderNumber = 1 });
            int newTotal = repo.RetrieveAll(DateTime.Today).Count;

            Assert.AreNotEqual(initialTotal, newTotal);
        }
    }
}
