using FlooringOrders.Data;
using FlooringOrders.Models.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Tests
{
    [TestFixture]
    public class LesserCRUDTests
    {
        //DONE

        //Tax
        [TestCase("Ohio",  true)]
        [TestCase("Fred", false)]
        [TestCase("OH", false)]
        public void CanGetOneState(string input, bool expectedResult)
        {
            bool result = true;
            TaxTestRepository repo = new TaxTestRepository();
            if (repo.RetrieveOneByFull(input) == null)
            {
                result = false;
            }
            Assert.AreEqual(result, expectedResult);
        }

        [TestCase("Ohio", false)]
        [TestCase("Fred", false)]
        [TestCase("OH", true)]
        public void CanGetOneStateAbbreviated(string input, bool expectedResult)
        {
            bool result = true;
            TaxTestRepository repo = new TaxTestRepository();
            if (repo.RetrieveOne(input) == null)
            {
                result = false;
            }
            Assert.AreEqual(result, expectedResult);
        }

        //Product
        [TestCase("Charlie", false)]
        [TestCase("Wood", true)]
        public void CanGetOneType(string typeInput, bool expectedResult)
        {
            bool result = true;
            ProductTestRepository repo = new ProductTestRepository();

            if (repo.RetrieveOne(typeInput) == null)
            {
                result = false;
            }
            Assert.AreEqual(result, expectedResult);
        }
    }
}
