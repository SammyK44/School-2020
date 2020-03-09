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
    public class TaxDatabaseRepository : ITaxHandler
    {
        private static string path = "Taxes.txt";
        public static List<Tax> ReadTaxFromFile()
        {
            List<Tax> list = new List<Tax>();
            list.Clear();
            foreach (string item in File.ReadAllLines(path))
            {
                string[] set = item.Split(',');
                if (set[2] != "TaxRate")
                {
                    Tax tax = new Tax
                    {
                        StateAbbreviation = set[0],
                        StateName = set[1],
                        TaxRate = decimal.Parse(set[2])
                    };
                    list.Add(tax);
                }
            }
            return list;
        }
        public List<Tax> RetrieveAll()
        {
            List<Tax> list = ReadTaxFromFile();
            return list;
        }

        public Tax RetrieveOne(string stateAbbreviation)
        {
            List<Tax> list = ReadTaxFromFile();
            
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
            List<Tax> list = ReadTaxFromFile();
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
