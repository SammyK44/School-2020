using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrders.Models;
using FlooringOrders.UI.Workflows;

namespace FlooringOrders.UI
{
    public class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Flooring Program");

                Console.WriteLine("\n1. Display orders");
                Console.WriteLine("2. Add an order");
                Console.WriteLine("3. Edit an order");
                Console.WriteLine("4. Remove an order");
                Console.WriteLine("5. Quit");
                Console.Write("\nInput selection: ");


                switch (Console.ReadLine())
                {
                    case "1":
                        new DisplayWorkflow().Execute();
                        break;
                    case "2":
                        new AddWorkflow().Execute();
                        break;
                    case "3":
                        new EditWorkflow().Execute();
                        break;
                    case "4":
                        new RemoveWorkflow().Execute();
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}
