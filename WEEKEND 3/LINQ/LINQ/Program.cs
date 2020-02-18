using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();
            Exercise20();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {
            List<Product> list = DataLoader.LoadProducts().Where(x => x.UnitsInStock == 0).ToList();

            foreach (Product product in list)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            List<Product> list = DataLoader.LoadProducts().Where(x => x.UnitsInStock > 0 && x.UnitPrice > 3).ToList();

            foreach (Product product in list)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        /// <summary>
        /// Print all customers and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            List<Customer> list = DataLoader.LoadCustomers().Where(x => x.Region == "WA").ToList();
            foreach (Customer customer in list)
            {
                Console.WriteLine(customer.CompanyName);
                foreach (Order order in customer.Orders)
                {
                    Console.WriteLine(order.OrderID);
                    Console.WriteLine(order.OrderDate);
                    Console.WriteLine(order.Total);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4(Product product)
        {
            var item = new { product.ProductName };
            Console.WriteLine(item);
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5(Product product)
        {
            var item = new { product.Category, product.ProductID, product.ProductName, UnitPrice = (product.UnitPrice * .25m), product.UnitsInStock };
            Console.WriteLine(item.Category);
            Console.WriteLine(item.ProductID);
            Console.WriteLine(item.ProductName);
            Console.WriteLine(item.UnitPrice);
            Console.WriteLine(item.UnitsInStock);
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6(Product product)
        {
            var item = new { ProductName = product.ProductName.ToUpper(), Category = product.Category.ToUpper() };
            Console.WriteLine(item.ProductName);
            Console.WriteLine(item.Category);
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        static void Exercise7(Product product)
        {
            var item = new { product.Category, product.ProductID, product.ProductName, product.UnitPrice, product.UnitsInStock, ReOrder = product.UnitsInStock < 3 ? true : false };
            Console.WriteLine(item.Category);
            Console.WriteLine(item.ProductID);
            Console.WriteLine(item.ProductName);
            Console.WriteLine(item.UnitPrice);
            Console.WriteLine(item.UnitsInStock);
            Console.WriteLine(item.ReOrder);
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8(Product product)
        {
            var item = new { product.Category, product.ProductID, product.ProductName, product.UnitPrice, product.UnitsInStock, StockValue = (product.UnitsInStock * product.UnitPrice) };
            Console.WriteLine(item.Category);
            Console.WriteLine(item.ProductID);
            Console.WriteLine(item.ProductName);
            Console.WriteLine(item.UnitPrice);
            Console.WriteLine(item.UnitsInStock);
            Console.WriteLine(item.StockValue);
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            List<int> list = DataLoader.NumbersA.Where(x => x % 2 == 0).ToList();
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void Exercise10()
        {
            List<Customer> list = (from Customer in DataLoader.LoadCustomers()
                                   where Customer.Orders.Sum(o => o.Total) < 500.00M
                                   select Customer).ToList();
            foreach (Customer customer in list)
            {
                Console.WriteLine(customer.CompanyName);
            }
        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11()
        {
            List<int> list = DataLoader.NumbersC.Where(x => x % 2 == 1).Take(3).ToList();

            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            List<int> list = DataLoader.NumbersB.Skip(3).ToList();
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void Exercise13()
        {
            List<Customer> list = DataLoader.LoadCustomers().Where(x => x.Region == "WA").ToList();
            List<Customer> list2 = (from Customers in list
                                    orderby Customers.Orders.Max(o => o.OrderDate)
                                    select Customers).ToList();

            foreach (Customer customer in list2)
            {
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Orders[0].OrderDate);
                Console.WriteLine(customer.Orders[0].OrderID);
                Console.WriteLine(customer.Orders[0].Total);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            List<int> numbers = DataLoader.NumbersC.TakeWhile(x => x < 6).ToList();
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            List<int> list = DataLoader.NumbersC.SkipWhile(x => x % 3 != 0).ToList();
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            List<Product> list = (from Products in DataLoader.LoadProducts()
                                  orderby Products.ProductName
                                  select Products).ToList();
            foreach (Product item in list)
            {
                Console.WriteLine(item.ProductName);
            }
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            List<Product> list = (from Product in DataLoader.LoadProducts()
                                  orderby Product.UnitsInStock descending
                                  select Product).ToList();
            foreach (Product item in list)
            {
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.UnitsInStock);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            List<Product> list = (from Product in DataLoader.LoadProducts()
                                  orderby Product.Category
                                  orderby Product.UnitPrice descending
                                  select Product).ToList();

            foreach (Product item in list)
            {
                Console.WriteLine(item.Category);
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.UnitPrice);
            }
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            List<int> list = DataLoader.NumbersB.Reverse().ToList();
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void Exercise20()
        {
            var list = (from Product in DataLoader.LoadProducts()
                        group Product by Product.Category into x
                        select new { x.Key, Grouping = x }).ToList();

            foreach (var item in list)
            {
                Console.WriteLine(item.Key);

                foreach (var thing in item.Grouping)
                {
                    Console.WriteLine(thing.ProductName);
                }
            }

        }

        //YOU'RE DONE

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        static void Exercise21()
        {

        }

        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void Exercise22()
        {

        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {

        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {

        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {

        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {

        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void Exercise27()
        {

        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {

        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {

        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {

        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {

        }
    }
}
