using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebbshopSport.GraphWindow;
using WebbshopSport.Models;

namespace WebbshopSport
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //while (true)
            //{
            //    Functions.Admin.AddProduct();
            //    Console.Clear();
            //    Console.WriteLine("Klar, starta om");
            //    Console.ReadLine();
            //}
            GraphWindow.Frontpage.LogIn();
            int loggedIn = int.Parse(Console.ReadLine());
            while (true)
            {
                using (var db = new MyDbContext())
                {
                    if (db.Customers.Where(x => x.Id == loggedIn).Select(x => x.Id).SingleOrDefault() == loggedIn)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        GraphWindow.Frontpage.LogIn();
                        Console.WriteLine("Wrong Id, try again: ");
                        loggedIn = int.Parse(Console.ReadLine());
                    }
                    
                }
            }
            while (true)
            {
                
                Console.Clear();
                GraphWindow.Frontpage.LoggedIn(loggedIn);
                GraphWindow.Frontpage.ChoosenProducts(loggedIn);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Functions.Menus.ShowCategory(1, loggedIn);
                        break;
                    case '2':
                        Console.Clear();
                        Functions.Menus.ShowCategory(2, loggedIn);
                        break;
                    case '3':
                        Console.Clear();
                        Functions.Menus.ShowCategory(3, loggedIn);
                        break;
                    case 'a':
                        Console.Clear();
                        GraphWindow.Frontpage.AdminView(loggedIn);
                        break;
                    case 's':
                        var searchList = new List<Models.Product>();
                        Console.Write("Search for: ");
                        string search = Console.ReadLine();
                        searchList = await Functions.Dapper.GetWantedProduct(search);

                        if (searchList.Count > 0)
                        {
                            foreach (Models.Product product in searchList)
                            {
                                Console.WriteLine(product.Name + " | Can be found under categoryId: " + product.CategoryId);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Could not find what you were looking for");
                        }
                        break;
                    case 'y':
                        Functions.Menus.RemoveFromCart(loggedIn);
                        break;
                    case 'c':
                        Console.Clear();
                        Functions.Menus.ShippmentView(loggedIn);
                        break;
                }
                Console.ReadKey();
            }
            //Functions.Admin.AddCategory();
            //Functions.Admin.AddSupplier();
            //while(true)
            //{
            //    Functions.Admin.AddProduct(); 
            //}
            //Functions.Admin.AddSize();
            //Functions.Admin.ChangeCategory();
            //Functions.Admin.ChangeSupplier();
            //Functions.Admin.DeleteSupplier();
            //Functions.Admin.DeleteProduct();
            //Functions.Admin.ChangeProduct();
            //Functions.Admin.CreateShoppingCostumer();
        }
    }
}
