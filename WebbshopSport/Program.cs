using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebbshopSport.GraphWindow;
using WebbshopSport.Models;

namespace WebbshopSport
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                GraphWindow.Frontpage.ChoosenProducts();
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Functions.Menus.ShowCategory(1);
                        break;
                    case '2':
                        Console.Clear();
                        Functions.Menus.ShowCategory(2);
                        break;
                    case '3':
                        Console.Clear();
                        Functions.Menus.ShowCategory(3);
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
