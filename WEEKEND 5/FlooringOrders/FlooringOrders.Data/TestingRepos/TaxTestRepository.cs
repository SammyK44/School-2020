using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class TaxTestRepository : ITaxHandler
    {
        private static List<Tax> list = new List<Tax>()
        {
            new Tax
            {
                StateAbbreviation = "OH",
                StateName = "Ohio",
                TaxRate = 5
            },
            new Tax
            {
                StateAbbreviation = "CA",
                StateName = "California",
                TaxRate = 5
            }
        };

        public List<Tax> RetrieveAll()
        {
            return list;
        }

        public Tax RetrieveOne(string stateAbbreviation)
        {
            foreach (Tax item in list)
            {
                if (stateAbbreviation == item.StateAbbreviation)
                {
                    return item;
                }
            }
            return null;
        }
        public Tax RetrieveOneByFull(string state)
        {
            foreach (Tax item in list)
            {
                if (state == item.StateName)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
