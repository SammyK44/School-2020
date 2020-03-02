using FlooringOrders.Data;
using FlooringOrders.Data.Managers;
using FlooringOrders.Models;
using FlooringOrders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Rules
{
    public static class DisplayRule
    {
        public static List<Order> RetrieveAll(DateTimeResponse response)
        {
            OrderManager repository = new RepoFactory().OrderFactory();

            response = ExistingFileRule.ExistingFile(response.Date.ToString("MM/dd/yyyy"));
            if (response.Success == false)
            {
                throw new Exception(response.Message);
            }
            return repository.RetrieveAll(response.Date);
        }
        public static bool? YesOrNo(string input)
        {
            input = input.ToUpper();
            if (input == "Y")
            {
                return true;
            }
            else if (input == "N")
            {
                return false;
            }
            else
            {
                return null;
            }
        }
    }
}
