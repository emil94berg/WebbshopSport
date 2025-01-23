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
                Console.ReadKey(true);
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
