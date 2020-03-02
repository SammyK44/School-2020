using FlooringOrders.Models;
using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.UI
{
    public static class ConsoleIO
    {
        public static void WriteOrder(Order order)
        {
            Console.WriteLine($"\n{order.OrderNumber} | {order.OrderDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine($"Materials: {order.MaterialCost.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"    Labor: {order.LaborCost.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"      Tax: {order.Tax.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"    Total: {order.Total.ToString("C", CultureInfo.CurrentCulture)}\n");
        }
        public static void WriteProduct(Product product)
        {
            Console.WriteLine($" Type: {product.ProductType}");
            Console.WriteLine($" Cost: {product.CostPerSquareFoot.ToString("C", CultureInfo.CurrentCulture)} per ft.²");
            Console.WriteLine($"Labor: {product.LaborCostPerSquareFoot.ToString("C", CultureInfo.CurrentCulture)} per ft.²\n");
        }
        public static void WriteTax(Tax tax)
        {
            Console.WriteLine($"        State Name: {tax.StateName}");
            Console.WriteLine($"State Abbreviation: {tax.StateAbbreviation}");
            Console.WriteLine($"          Tax Rate: {tax.TaxRate.ToString("C", CultureInfo.CurrentCulture)}\n");
        }
    }
}
