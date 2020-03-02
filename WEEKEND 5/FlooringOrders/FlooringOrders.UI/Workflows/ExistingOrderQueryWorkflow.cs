using FlooringOrders.BLL.Rules;
using FlooringOrders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.UI
{
    public class ExistingOrderQueryWorkflow
    {
        public DateTimeResponse GetDate()
        {
            string dateInput = "";

            Console.Write("( Month/Day/Year ): ");
            dateInput = Console.ReadLine();

            return ExistingFileRule.ExistingFile(dateInput);
        }
        public OrderResponse GetOrder(DateTime date)
        {
            string orderNumberInput = "";

            Console.Write("Digit: ");
            orderNumberInput = Console.ReadLine();

            return ExistingFileRule.ExistingOrder(date, orderNumberInput);
        }
    }
}
