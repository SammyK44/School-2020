using FlooringOrders.BLL;
using FlooringOrders.BLL.Rules;
using FlooringOrders.Models;
using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Objects;
using FlooringOrders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.UI.Workflows
{
    public class EditWorkflow : IExecute
    {
        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("Edit");

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

            Console.WriteLine("\nWhat is the order number?");

            OrderResponse orderResponse = new OrderResponse();

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

            string nameInput;
            string stateInput;
            string typeInput;
            string areaInput;

            //Customer name
            Console.WriteLine("What is the customer's name?");
            while (true)
            {
                Console.Write($"Name ({orderResponse.Order.CustomerName}): ");
                nameInput = Console.ReadLine();
                break;
            }

            ModifyOrderRule rule = new ModifyOrderRule();

            //State
            Console.WriteLine("What is the (abbreviation of the) state where the order is from?");
            while (true)
            {
                Console.Write($"({rule.RetrieveOneByFull(orderResponse.Order.State)}): ");
                stateInput = Console.ReadLine();
                if (string.IsNullOrEmpty(stateInput))
                {
                    break;
                }
                Response response = rule.ExistingState(stateInput);
                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    Console.Write(" Try again.");
                }
                else
                {
                    break;
                }
            }

            //Product type
            Console.WriteLine("What is the type of product to be used on the floor?");
            while (true)
            {
                Console.Write($"Type ({orderResponse.Order.ProductType}): ");
                typeInput = Console.ReadLine();
                if (string.IsNullOrEmpty(typeInput))
                {
                    break;
                }
                Response response = rule.ExistingProduct(typeInput);
                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    Console.Write(" Try again.");
                }
                else
                {
                    break;
                }
            }

            //Area
            Console.WriteLine("What is the area (in square feet) of the floor?");
            while (true)
            {
                Console.Write($"Number ({orderResponse.Order.Area}): ");
                areaInput = Console.ReadLine();
                if (string.IsNullOrEmpty(areaInput))
                {
                    break;
                }
                Response response = rule.Area(areaInput);
                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    Console.Write(" Try again.");
                }
                else
                {
                    break;
                }
            }

            //Assemble & prompt to save
            Console.Clear();
            Order order = rule.AssembleUpdatedOrder(dateTimeResponse.Date.ToString(), nameInput, stateInput, typeInput, areaInput, orderResponse.Order.OrderNumber, orderResponse.Order);
            Console.WriteLine("Order summary:\n");
            ConsoleIO.WriteOrder(order);
            Console.WriteLine("Would you like to save your changes to this order?");

            Console.WriteLine("\nY: Save changes");
            Console.WriteLine("N: Cancel changes");

            while (true)
            {
                Console.Write("(Y/N): ");
                bool? valid = DisplayRule.YesOrNo(Console.ReadLine());
                if (valid == true)
                {
                    rule.Update(orderResponse.Order, order);
                    Console.WriteLine("Changes saved.");
                    break;
                }
                else if (valid == false)
                {
                    Console.WriteLine("Changes cancelled.");
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
