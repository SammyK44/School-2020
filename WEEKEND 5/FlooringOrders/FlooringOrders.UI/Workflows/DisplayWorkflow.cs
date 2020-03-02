using FlooringOrders.BLL.Rules;
using FlooringOrders.Models;
using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.UI.Workflows
{
    public class DisplayWorkflow : IExecute
    {
        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("Display");

            Console.WriteLine("\nWhat is the date of the order set?");
            ExistingOrderQueryWorkflow workflow = new ExistingOrderQueryWorkflow();
            DateTimeResponse dateTimeResponse = new DateTimeResponse();

            while (true)
            {
                dateTimeResponse = workflow.GetDate();
                if (dateTimeResponse.Success == false)
                {
                    Console.WriteLine(dateTimeResponse.Message);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            foreach (Order item in DisplayRule.RetrieveAll(dateTimeResponse))
            {
                ConsoleIO.WriteOrder(item);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
