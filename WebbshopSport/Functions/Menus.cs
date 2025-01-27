using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbshopSport.GraphWindow;
using WebbshopSport.Models;

namespace WebbshopSport.Functions
{
    internal class Menus
    {
        public static void ShowCategory(int answear, int loggedIn)
        {
            using (var db = new MyDbContext())
            {
                var nameOfCategory = db.Categories.Where(x => x.Id == answear).Select(x => $"{x.Name}").SingleOrDefault();
                var showCategory = db.Products.Where(x => x.CategoryId == answear).Select(x => $"{x.Id} - {x.Name} - {x.Price}").ToList();
                var categoryWindow = new Window(nameOfCategory,0,0,showCategory);
                categoryWindow.Draw();
                //foreach (var item in showCategory)
                //{
                //    Console.WriteLine(item);
                    
                //}
                int ammount = showCategory.Count();
                Console.WriteLine("Choose product: ");
                int productAnswear = int.Parse(Console.ReadLine());
                var nameOfProduct = db.Products.Where(x => x.Id == productAnswear).Select(x => $"{x.Name}").SingleOrDefault(); // Används till window string
                var showProduct = db.Products.Where(x => x.Id == productAnswear).Select(x => $"Description : {x.Description}").ToList(); // Används till window list
                var itemToCart = db.Products.Where(x => x.Id == productAnswear).Select(x => x).SingleOrDefault(); // Plocka ut hela objektet för att lägga till i varukorg
                var choosenProductWindow = new Window(nameOfProduct, 0, ammount+2, showProduct);
                choosenProductWindow.Draw();
                Console.WriteLine("Press x to add this product into your cart or any key to get back to menu");
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                if (key.KeyChar == 'x')
                {
                    Console.WriteLine("How many do you wish to order: ");
                    int howMany = int.Parse(Console.ReadLine());
                    Console.WriteLine(itemToCart.Name + " " + itemToCart.Price + " sek. Quantity: " + howMany);
                    Console.WriteLine("Added to cart");

                    var addToCart = new OrderItem()
                    {
                        ShoppingCartId = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.ShoppingCartId).SingleOrDefault(),
                        ProductId = itemToCart.Id,
                        Price = itemToCart.Price,
                        Quantity = howMany
                    };
                    db.OrderItems.Add(addToCart);
                    db.SaveChanges();
                }
                else
                {
                    
                }
            }
        }
        public static void RemoveFromCart(int loggedIn)
        {
            using (var db = new MyDbContext())
            {
                int shoppingNumber = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.ShoppingCartId).SingleOrDefault();
                Console.WriteLine("Type productId: of product you want to remove: ");
                int removeId = int.Parse(Console.ReadLine());
                //var myOrderItems = db.OrderItems.Where(x => x.ShoppingCartId == shoppingNumber).Select(x => x.ProductId == removeId).SingleOrDefault();
                db.Remove(db.OrderItems.Single(x => x.ProductId == removeId && x.ShoppingCartId == shoppingNumber));
                db.SaveChanges();

                //db.Remove(db.Suppliers.Single(x => x.Id == deleteId));
                //foreach (var item in myOrderItems)
                //{
                //    Console.WriteLine(item);
                //}
            }
        }
        

    }
}
