using FlooringOrders.BLL;
using FlooringOrders.BLL.Rules;
using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.UI.Workflows
{
    public class RemoveWorkflow : IExecute
    {
        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("Remove");

            Console.WriteLine("\nWhat is the date of the order?");
            ExistingOrderQueryWorkflow workflow = new ExistingOrderQueryWorkflow();
            DateTimeResponse dateTimeResponse = new DateTimeResponse();

            while (true)
            {
                dateTimeResponse = workflow.GetDate();
                if (dateTimeResponse.Success == false)
                {
                    Console.WriteLine(dateTimeResponse.Message);
                    Console.WriteLine(" Try again.");
                }
                else
                {
                    break;
                }
            }

            OrderResponse orderResponse = new OrderResponse();
            Console.WriteLine("\nWhat is the order number?");
            while (true)
            {
                orderResponse = workflow.GetOrder(dateTimeResponse.Date);
                if (orderResponse.Success == false)
                {
                    Console.WriteLine(orderResponse.Message);
                    Console.WriteLine(" Try again.");
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            ConsoleIO.WriteOrder(orderResponse.Order);
            Console.WriteLine("Are you sure you want to remove this order?");

            Console.WriteLine("\nY: Remove & save");
            Console.WriteLine("N: Cancel removal");

            ModifyOrderRule rule = new ModifyOrderRule();

            while (true)
            {
                Console.Write("(Y/N): ");
                bool? valid = DisplayRule.YesOrNo(Console.ReadLine());
                if (valid == true)
                {
                    rule.Delete(orderResponse.Order);
                    Console.WriteLine("Order removed.");
                    break;
                }
                else if (valid == false)
                {
                    Console.WriteLine("Order removal cancelled.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
    }
}
