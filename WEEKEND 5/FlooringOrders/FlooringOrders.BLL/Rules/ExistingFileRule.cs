using FlooringOrders.Data;
using FlooringOrders.Data.Managers;
using FlooringOrders.Models;
using FlooringOrders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Rules
{
    public static class ExistingFileRule
    {
        public static DateTimeResponse ExistingFile(string parseMe)
        {
            DateTime date;
            OrderManager repository = new RepoFactory().OrderFactory();
            DateTimeResponse response = new DateTimeResponse

            {
                Success = false
            };
            if (!DateTime.TryParse(parseMe, out date))
            {
                response.Message = "Failed; Invalid date.";
                return response;
            }

            if (!repository.Exists(repository.OrderFile(date)))
            {
                response.Message = "Failed; There have been no orders set for that date.";
                return response;
            }

            response.Success = true;
            response.Date = date;
            return response;
        }
        public static OrderResponse ExistingOrder(DateTime date, string parseMe)
        {
            int x;
            OrderManager repository = new RepoFactory().OrderFactory();
            OrderResponse response = new OrderResponse()
            {
                Success = false
            };
            if (ConfigurationManager.AppSettings["Mode"].ToString() != "Test")
            {
                if (!File.Exists(repository.OrderFile(date)))
                {
                    throw new Exception("Error: Invalid date entered Existing Order Number Validator. Contact IT");
                }
            }
            if (!int.TryParse(parseMe, out x))
            {
                response.Message = "Failed; Must be a number.";
                return response;
            }
            Order order = repository.RetrieveOne(date, x);
            if (order == null)
            {
                response.Message = "Failed; That order number doesn't corrospond to that date.";
                return response;
            }
            response.Success = true;
            response.Order = order;
            return response;
        }
    }
}
