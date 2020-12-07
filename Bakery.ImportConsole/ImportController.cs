using Bakery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Bakery.ImportConsole
{
    public class ImportController
    {
        public static IEnumerable<Product> ReadFromCsv()
        {
            var productMatrix = "Products.csv".ReadStringMatrixFromCsv(true);
            var orderItemsMatrix = "OrderItems.csv".ReadStringMatrixFromCsv(true);

            var products = productMatrix
                .Select(line => new Product
                {
                    ProductNr = line[0],
                    Name = line[1],
                    Price = Convert.ToDouble(line[2])
                })
                .ToList();

            var customers = orderItemsMatrix
                .GroupBy(line => line[2])
                .Select(customerGroup => new Customer
                {
                    CustomerNr = customerGroup.Key,
                    LastName = customerGroup.First()[4],
                    FirstName = customerGroup.First()[3]
                })
                .ToList();

            var orders = orderItemsMatrix
                .GroupBy(line => line[0])
                .Select(orderGroup => new Order
                {
                    OrderNr = orderGroup.Key,
                    Date = Convert.ToDateTime(orderGroup.First()[1]),
                    Customer = customers.SingleOrDefault(customer => customer.CustomerNr == orderGroup.First()[2])
                })
                .ToList();

            var orderItems = orderItemsMatrix
                .Select(orderItem => new OrderItem
                {
                    Amount = Convert.ToInt32(orderItem[6]),
                    Order = orders.SingleOrDefault(order => order.OrderNr == orderItem[0]),
                    Product = products.SingleOrDefault(product => product.ProductNr == orderItem[5])

                })
                .ToList();

            products.ForEach(product => product.OrderItems = orderItems.FindAll(item => item.Product == product));
            return products;
        }
    }
}
