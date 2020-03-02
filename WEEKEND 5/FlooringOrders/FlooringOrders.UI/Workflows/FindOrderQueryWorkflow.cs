using FlooringOrders.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.UI.Workflows
{
    public class FindOrderQueryWorkflow
    {
        public DateTime DateQuery()
        {
            string input = "";

            Console.WriteLine("\nWhat is the date of the order?");

            Console.Write("( Month/Day/Year ): ");
            input = Console.ReadLine();
            bool valid = Validator.ExistingFile(input);

            if (valid == false)
            {
                Console.WriteLine("There have been no orders set for that date.");
            }
            return DateTime.Parse(input);
        }
        public int OrderNumberQuery(DateTime date)
        {
            string input = "";

            Console.WriteLine("\nWhat is the order number?");

            Console.Write("Digit: ");
            input = Console.ReadLine();
            bool valid = Validator.ExistingOrderNumber(date, input);
            if (valid == false)
            {
                Console.WriteLine("There are no orders with that number.");
            }

            return int.Parse(input);
        }
    }
}
