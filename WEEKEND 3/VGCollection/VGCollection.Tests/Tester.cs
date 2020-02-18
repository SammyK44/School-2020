using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGCollection.BLL;
using NUnit.Framework;

namespace VGCollection.Tests
{
    [TestFixture]
    public class Tester
    {
        [TestCase("1", "1", "1", "1", 1)]
        public void CreateListTest(string name, string system, string genre, string publisher, int year)
        {
            int initialList = Handler.RetrieveAll().Count;
            Handler.Create(name, system, genre, publisher, year);
            Assert.AreNotEqual(initialList, Handler.RetrieveAll().Count);
        }
    }
}
