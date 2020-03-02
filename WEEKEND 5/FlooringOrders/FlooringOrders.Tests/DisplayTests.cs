using FlooringOrders.BLL.Rules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Tests
{
    //DONE

    [TestFixture]
    public class DisplayTests
    {
        [TestCase("Y", true)]
        [TestCase("N", false)]
        [TestCase("X", null)]
        public void YesOrNoTest(string input, bool? expectedResult)
        {
            bool? result = DisplayRule.YesOrNo(input);
            Assert.AreEqual(result, expectedResult);
        }
    }
}
