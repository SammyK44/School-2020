using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrders.BLL;
using FlooringOrders.BLL.Rules;
using FlooringOrders.Models;
using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Objects;

namespace FlooringOrders.UI.Workflows
{
    public class AddWorkflow : IExecute
    {
        public void Execute()
        {
            string dateInput;
            string nameInput;
            string stateInput;
            string typeInput;
            string areaInput;

            Console.Clear();
            Console.WriteLine("Add");

            ModifyOrderRule rule = new ModifyOrderRule();

            //Order date
            Console.WriteLine("What is the date of the order?");
            while (true)
            {
                Console.Write("(Month/Day/Year): ");
                dateInput = Console.ReadLine();

                Response response = rule.Date(dateInput);
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

            //Customer name
            Console.WriteLine("What is the customer's name?");
            while (true)
            {
                Console.Write("Name: ");
                nameInput = Console.ReadLine();
                Response response = rule.CustomerName(nameInput);
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

            //State
            foreach (Tax tax in rule.RetrieveAllTaxes())
            {
                ConsoleIO.WriteTax(tax);
            }
            Console.WriteLine("What is the (abbreviation of the) state where the order is from?");
            while (true)
            {
                Console.Write("(XX): ");
                stateInput = Console.ReadLine();
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
            foreach (Product product in rule.RetrieveAllProducts())
            {
                ConsoleIO.WriteProduct(product);
            }

            Console.WriteLine("What is the type of product to be used on the floor?");
            while (true)
            {
                Console.Write("Type: ");
                typeInput = Console.ReadLine();
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
                Console.Write("Number: ");
                areaInput = Console.ReadLine();
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

            ///Calculate & return order, then ask if they want to place the order (Y/N).
            Console.Clear();
            Order order = rule.AssembleOrder(dateInput, nameInput, stateInput, typeInput, areaInput, null);
            Console.WriteLine("Order summary:\n");
            ConsoleIO.WriteOrder(order);
            Console.WriteLine("Would you like to place this order?");

            Console.WriteLine("\nY: Place the order");
            Console.WriteLine("N: Cancel order");

            while (true)
            {
                Console.Write("(Y/N): ");
                bool? valid = DisplayRule.YesOrNo(Console.ReadLine());
                if (valid == true)
                {
                    rule.Create(order);
                    Console.WriteLine("Order placed.");
                    break;
                }
                else if (valid == false)
                {
                    Console.WriteLine("Order cancelled.");
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
