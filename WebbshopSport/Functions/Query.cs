using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbshopSport.Models;

namespace WebbshopSport.Functions
{
    internal class Query
    {
        public static void Shoppoholic()
        {
            using (var db = new MyDbContext())
            {
                var mostSpent = (from Customer in db.Customers
                                 join ShoppingCart in db.ShoppingCarts
                                 on Customer.ShoppingCartId equals ShoppingCart.Id
                                 join OrderItem in db.OrderItems
                                 on ShoppingCart.Id equals OrderItem.ShoppingCartId
                                 where OrderItem.Payed == true
                                 group OrderItem by Customer.Id into grouped
                                 select new
                                 {
                                     CustomerId = grouped.Key,
                                     TotalSpent = grouped.Sum(o => o.Price)
                                 })
                                .OrderByDescending(o => o.TotalSpent).FirstOrDefault();
                var nameOfCostumer = db.Customers.Where(x => x.Id == mostSpent.CustomerId).Select(x => x.Name).FirstOrDefault();
                Console.WriteLine("------------------");
                Console.WriteLine($"Most spent: {nameOfCostumer}. Spent: {mostSpent.TotalSpent}");
                Console.WriteLine("------------------");
                var mostSold = (from OrderItem in db.OrderItems
                                where OrderItem.Payed == true
                                group OrderItem by OrderItem.ProductId into grouped
                                select new
                                {
                                    ProductId = grouped.Key,
                                    TotalSold = grouped.Count()
                                })
                                .OrderByDescending(o => o.TotalSold).FirstOrDefault();
                var nameOfProduct = db.Products.Where(x => x.Id == mostSold.ProductId).Select(x => x.Name).FirstOrDefault();
                Console.WriteLine($"Most sold product: {nameOfProduct}. Ammount sold: {mostSold.TotalSold}");
                Console.ReadKey();
            }
        }
    }
}
